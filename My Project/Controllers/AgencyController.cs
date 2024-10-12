using BLL.Abstractions;
using BLL.Interfaces;
using DAL.Default;
using DAL.DTO;
using DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using My_Project.Common;
using My_Project.DTO;
using YourProjectNamespace.Models;
namespace My_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public AgencyController(IUnitOfWork unitOfWork, UserManager<User> _userManager, RoleManager<Role> _roleManager)
        {
            _unitOfWork = unitOfWork;
            userManager = _userManager;
            roleManager = _roleManager;
        }
        //Agency Crud Operation
        [HttpGet("{id}")]
        public IActionResult GetAgency(int id)
        {
            var Agency = _unitOfWork.AgencyRepository.GetWithInclude(id, e => e.Id == id,
            query => query.Include(p => p.Agents),
                 query => query.Include(p => p.Owner),
                   query => query.Include(p => p.Products),
                     query => query.Include(p => p.Subscription)
             );
            if (Agency == null)
            {
                return NotFound();
            }

            var AgencyDto = (Agency).Adapt<AgencyResponseDTO>();


            return Ok(AgencyDto);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AgencyRequestDTO agency)
        {
            if (agency == null)
            {
                return BadRequest();
            }

            var existingagency = _unitOfWork.AgencyRepository.Get(id);
            if (existingagency == null)
            {
                return NotFound();
            }
            agency.Id = existingagency.Id;

            _unitOfWork.AgencyRepository.Update(agency.Adapt(existingagency));
            _unitOfWork.Save();

            return NoContent();
        }

        [HttpPost("{agencyId}/add-agency")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAgency(int agencyId, [FromBody] RegisterReq NewAgent)
        {
            if (NewAgent == null)
            {
                return BadRequest("Invalid agent");
            }
            var Agency = _unitOfWork.AgencyRepository.Find(a=>a.OwnerId==agencyId).FirstOrDefault();

            //var subscription = _unitOfWork.SubscriptionRepository.Get((int)Agency.SubscriptionId);
            //if (Agency.NumOfAvailableAgents >= subscription.NumOfAgents)
            //{
            //    return BadRequest("No available agents");
            //}

            var exsistingAgent = await userManager.FindByEmailAsync(NewAgent.Email);
            if (exsistingAgent != null)
            {
                return BadRequest("User with same email already exists");
            }

            var NewUser = new User
            {
                Email = NewAgent.Email,
                UserName = NewAgent.Name,
                PhoneNumber=NewAgent.PhoneNumber


            };
            var result = await userManager.CreateAsync(NewUser, NewAgent.Password);
            if (!result.Succeeded)
            {
                userManager.DeleteAsync(NewUser);
                return BadRequest(result.Errors);
            }
            var role = await roleManager.FindByNameAsync(DefaultRoles.AgentRoleName);
            if (role != null)
            {
                await userManager.AddToRoleAsync(NewUser, role.Name);
            }
            var Agent = new Agent
            {
                AgencyId = Agency.Id,
                UserId = NewUser.Id,
                CreatedDate = DateTime.Now
            };
            Agency.NumOfAvailableAgents++;
            _unitOfWork.AgencyRepository.Update(Agency);
            _unitOfWork.AgentRepository.Add(Agent);
            _unitOfWork.Save();

            return Ok(new { email = NewAgent.Email, pass = NewAgent.Password });
        }
       
        [HttpDelete("{agentId}")]
        public async Task<IActionResult> DeleteAgent(int agentId)
        {
            var agent = _unitOfWork.AgentRepository.Get(agentId);
            if (agent == null)
            {
                return NotFound("Agent not found.");
            }

            var agency = _unitOfWork.AgencyRepository.Get((int)agent.AgencyId);
            if (agency == null)
            {
                return NotFound("Agency not found.");
            }

            var user = await userManager.FindByIdAsync(agent.UserId.ToString());
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
            agency.NumOfAvailableAgents++;

            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("The agent has already been deleted or updated by another process.");
            }

            return Ok("Agent deleted successfully.");
        }
        [HttpGet]
        [Route("{agencyid}/agents")]

        public IActionResult GetAgents(int agencyid,[FromQuery] RequestFilters filters)
        {
            var AgentsQuery = _unitOfWork.AgentRepository.GetAllWithInclude(
                  query => query.Where(x => string.IsNullOrEmpty(filters.SearchValue) ||
                                       x.User.UserName.Contains(filters.SearchValue)),
               query => query.Include(p => p.User),
                 query => query.Include(p => p.Agency),
                   query => query.Include(p => p.Products),
                     query => query.Include(p => p.Tasks),
                       query => query.Where(p => p.Agency.OwnerId==agencyid)
               ).AsQueryable();

            if (!string.IsNullOrEmpty(filters.SortColumn))
            {
                AgentsQuery = AgentsQuery.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
            }
            if (AgentsQuery == null)
            {
                return NotFound();
            }
            var paginatedAgents = PaginatedList<Agent>.Create(AgentsQuery, filters.pageNumber, filters.pageSize);
            var AgentsDto = paginatedAgents.Items.Select(p => p.Adapt<AgentResponseDTO>()).ToList();
            return Ok(AgentsDto);
        }
 
    }
}
