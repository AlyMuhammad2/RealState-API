﻿using BLL.Abstractions;
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
               query => query.Include(p => p.Agents).ThenInclude(ag=>ag.User),
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
            var paginatedAgencies = PaginatedList<Agency>.Create(AgenciesQuery, filters.pageNumber, filters.pageSize);
            var AgenciesDto = paginatedAgencies.Items.Select(p => p.Adapt<AgencyResponseDTO>()).ToList();
            return Ok(AgenciesDto);
        }
      
        [HttpPut("{id}")]
        public IActionResult UpdateAgent(int id, [FromBody] Agency agent)
        {
            if (agent == null || agent.Id != id)
            {
                return BadRequest();
            }

            var existingAgent = _unitOfWork.AgencyRepository.Get(id);
            if (existingAgent == null)
            {
                return NotFound();
            }

            _unitOfWork.AgencyRepository.Update(agent);
            _unitOfWork.Save();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAgent(int id)
        {
            var agent = _unitOfWork.AgentRepository.Get(id);
            if (agent == null)
            {
                return NotFound();
            }

            _unitOfWork.AgencyRepository.Delete(agent.Id);
            _unitOfWork.Save();

            return NoContent();
        }
        [HttpPost("add-agency")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAgency([FromBody] AgencyRegisterReq NewAgency)
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
                NumOfAvailableAgents=0,
                OwnerId = NewUser.Id,
                CreatedDate = DateTime.Now,
            };
   
            _unitOfWork.AgencyRepository.Add(Agency);
            _unitOfWork.Save();

            return Ok("Agent is added.");
        }
    }
}
