using BLL.Abstractions;
using BLL.Services;
using DAL.Default;
using DAL.DTO;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Project.Common;
using My_Project.DTO;
using System.Linq;
using System.Linq.Dynamic.Core;
using YourProjectNamespace.Models;

namespace My_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        public AdminController(IUnitOfWork unitOfWork, UserManager<User> _userManager, RoleManager<Role> _roleManager)
        {
            _unitOfWork = unitOfWork;
            userManager = _userManager;
            roleManager = _roleManager;
        }

        [HttpGet("/Admin/Agency")]
        // [Authorize(Roles = "Admin")]
        public IActionResult GetAgencies([FromQuery] RequestFilters filters)
        {
            var AgenciesQuery = _unitOfWork.AgencyRepository.GetAllWithInclude(
                  query => query.Where(x => string.IsNullOrEmpty(filters.SearchValue) ||
                                       x.Name.Contains(filters.SearchValue)),
               query => query.Include(p => p.Agents).ThenInclude(ag => ag.User),
                 query => query.Include(p => p.Owner),
                   query => query.Include(p => p.Products),
                     query => query.Include(p => p.Subscription),
                     query => query.Include(p => p.Tasks)
               ).AsQueryable();

            if (!string.IsNullOrEmpty(filters.SortColumn))
            {
                AgenciesQuery = AgenciesQuery.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
            }
            if (AgenciesQuery == null)
            {
                return NotFound();
            }
            var totalItems = AgenciesQuery.Count(); // Get total items count

            var paginatedAgencies = PaginatedList<Agency>.Create(AgenciesQuery, filters.pageNumber, filters.pageSize);
            var AgenciesDto = paginatedAgencies.Items.Select(p => p.Adapt<AgencyResponseDTO>()).ToList();

            return Ok(new
            {
                TotalItems = totalItems, // Return total items count
                Items = AgenciesDto // Return paginated items
            });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAgency(int id, [FromBody] Agency agency)
        {
            if (agency == null || agency.Id != id)
            {
                return BadRequest();
            }

            var existingAgent = _unitOfWork.AgencyRepository.Get(id);
            if (existingAgent == null)
            {
                return NotFound();
            }

            _unitOfWork.AgencyRepository.Update(agency);
            _unitOfWork.Save();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgency(int id)
        {

            var agency = _unitOfWork.AgencyRepository.Get(id);
            if (agency == null)
            {
                return NotFound("Agency not found.");
            }

            var user = await userManager.FindByIdAsync(agency.OwnerId.ToString());
            if (user != null)
            {
                // 1- Remove the user from all roles
                var roles = await userManager.GetRolesAsync(user);
                if (roles != null)
                {
                    foreach (var role in roles)
                    {
                        await userManager.RemoveFromRoleAsync(user, role);
                    }
                }

                // 2- Delete the user
                var result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest("Failed to delete the user associated with the agent.");
                }
            }
            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("The agent has already been deleted or updated by another process.");
            }

            return Ok();
        }
        [HttpPost("add-agency")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAgency([FromBody] AgencyRegisterReq NewAgency, int? subid)
        {
            if (NewAgency == null)
            {
                return BadRequest("Invalid agent");
            }


            var exsistingAgency = await userManager.FindByEmailAsync(NewAgency.Email);
            if (exsistingAgency != null)
            {
                return BadRequest("User with same email already exists");
            }

            var NewUser = new User
            {

                Email = NewAgency.Email,
                UserName = NewAgency.Name,
                PhoneNumber = NewAgency.PhoneNumber,

            };
            var result = await userManager.CreateAsync(NewUser, NewAgency.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            var role = await roleManager.FindByNameAsync(DefaultRoles.AgencyRoleName);
            if (role != null)
            {
                await userManager.AddToRoleAsync(NewUser, role.Name);
            }
            var Agency = new Agency
            {
                Name = NewAgency.AgencyName,
                NumOfAvailableAgents = 0,
                OwnerId = NewUser.Id,
                SubscriptionId = subid,
                CreatedDate = DateTime.Now,
            };
            var sub = _unitOfWork.SubscriptionRepository.Get((int)subid);
            if (sub != null)
            {
                sub.Id = (int)subid;
                sub.NumOfsubs++;
                _unitOfWork.SubscriptionRepository.Update(sub);
            }

            _unitOfWork.AgencyRepository.Add(Agency);
            _unitOfWork.Save();

            return Ok("Agency is added.");
        }

        //[HttpGet]
        //public  IActionResult GetAllSubscriptions()
        //{
        //    var subscriptions =  _unitOfWork.SubscriptionRepository.GetAll();
        //    return Ok(subscriptions);
        //}
        //[HttpPut("{id}")]
        //public  IActionResult UpdateSubscription(int id, [FromBody] Subscription subscription)
        //{
        //    var existingSubscription =  _unitOfWork.SubscriptionRepository.Get(id);
        //    if (existingSubscription == null)
        //    {
        //        return NotFound(new { message = "Subscription not found" });
        //    }

        //    existingSubscription.SubscriptionType = subscription.SubscriptionType;
        //    existingSubscription.Price = subscription.Price;
        //    existingSubscription.Description = subscription.Description;
        //    existingSubscription.IsActive = subscription.IsActive;

        //    _unitOfWork.Save();

        //    return Ok(existingSubscription);
        //}
        //[HttpDelete("{id}")]
        //public IActionResult DeleteSubscription(int id)
        //{
        //    var subscription =  _unitOfWork.SubscriptionRepository.Get(id);
        //    if (subscription == null)
        //    {
        //        return NotFound(new { message = "Subscription not found" });
        //    }

        //    _unitOfWork.SubscriptionRepository.Delete(subscription.Id);
        //    _unitOfWork.Save();

        //    return NoContent();
        //}
        [HttpGet]
        [Route("agents")]

        public IActionResult GetAgents([FromQuery] RequestFilters filters)
        {
            var AgentsQuery = _unitOfWork.AgentRepository.GetAllWithInclude(
                   
               query => query.Include(p => p.User),
                 query => query.Include(p => p.Agency),
                   query => query.Include(p => p.Products),
                  query => query.Include(p => p.Subscription),
                     query => query.Include(p => p.Tasks)
                              ).AsQueryable();

            if (!string.IsNullOrEmpty(filters.SortColumn))
            {
                AgentsQuery = AgentsQuery.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
            }
            if (filters.SearchValue == "Free")
            {
                AgentsQuery = AgentsQuery.Where(x => x.AgencyId == null);
            }
            else 
            {
                AgentsQuery= _unitOfWork.AgentRepository.GetAllWithInclude(
                   query => query.Where(x => string.IsNullOrEmpty(filters.SearchValue) ||
                                                      x.User.UserName.Contains(filters.SearchValue) ||
                                                      x.Agency.Name.Contains(filters.SearchValue)),
               query => query.Include(p => p.User),
                 query => query.Include(p => p.Agency),
                   query => query.Include(p => p.Products),
                    query => query.Include(p => p.Subscription),
                     query => query.Include(p => p.Tasks)
                              ).AsQueryable();


                                                      
            }

            var totalItems = AgentsQuery.Count();
            if (totalItems == 0)
            {
                return NotFound("No agents found.");
            }
            var paginatedAgents = PaginatedList<Agent>.Create(AgentsQuery, filters.pageNumber, filters.pageSize);
            var AgentsDto = paginatedAgents.Items.Select(p => p.Adapt<AgentResponseDTO>()).ToList();
            return Ok(new
            {
                TotalItems = totalItems, // Return total items count
                Items = AgentsDto
                // Return paginated items);
            });

            }
        [HttpGet("subscriptions")]
        public IActionResult GetAllSubscriptions([FromQuery] RequestFilters filters)
        {
            // Get agency and agent subscriptions
            var agencySubscriptions =  _unitOfWork.AgencyRepository.GetAllWithInclude(
                  query => query.Where(x => string.IsNullOrEmpty(filters.SearchValue) ||
                                       x.Name.Contains(filters.SearchValue)),
                 query => query.Include(p => p.Owner),
                     query => query.Include(p => p.Subscription),
                   query => query.Where(p => p.SubscriptionId!=null)

               ).AsQueryable();
            var agentSubscriptions = _unitOfWork.AgentRepository.GetAllWithInclude(
                   query => query.Where(x => string.IsNullOrEmpty(filters.SearchValue) ||
                                                      x.User.UserName.Contains(filters.SearchValue) ||
                                                      x.Agency.Name.Contains(filters.SearchValue)),
               query => query.Include(p => p.User),      
                   query => query.Include(p => p.Subscription),
                   query => query.Where(p => p.SubscriptionId != null)

                              ).AsQueryable();

            var agencySubscriptionDtos = agencySubscriptions.Adapt<List<subscriptionDto>>();

            // Map agent subscriptions to DTO using Mapster
            var agentSubscriptionDtos = agentSubscriptions.Adapt<List<subscriptionDto>>();

            // Combine both lists into one
            var allSubscriptions = new List<subscriptionDto>();

            // Add your subscriptions to the list...

            // Apply sorting if filters are provided
          
            allSubscriptions.AddRange(agencySubscriptionDtos);
            allSubscriptions.AddRange(agentSubscriptionDtos);

            foreach (var subscription in allSubscriptions)
            {
                subscription.CalculateIsActive();
            }
            var totalItems = allSubscriptions.Count();

            if (totalItems == 0)
            {
                return NotFound("No subscriptions found.");
            }

            // Apply pagination
            var paginatedSubscriptions = PaginatedList<subscriptionDto>.Create(allSubscriptions.AsQueryable(), filters.pageNumber, filters.pageSize);

            // Return paginated result
            return Ok(new
            {
                TotalItems = totalItems,   // Return total count of items
                Items = paginatedSubscriptions.Items // Return paginated items
            });
        }

    }
}
