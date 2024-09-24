using BLL.Interfaces;
using DAL.Default;
using DAL.DTO;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        [HttpGet("GetAgents")]
        public IActionResult GetAgents(int AgencyId)
        {

            var Agents = _unitOfWork.AgentRepository.GetAll(a => a.AgencyId == AgencyId);
            return Ok(Agents);
        }
        [HttpGet("{id}")]
        public IActionResult GetAgent(int id)
        {
            var Agent = _unitOfWork.AgentRepository.Get(id);
            if (Agent == null)
            {
                return NotFound();
            }
            return Ok(Agent);
        }
        [HttpPost("{agencyId}/add-agent")]
        //[Authorize(Roles = "Agency")]
        public async Task<IActionResult> CreateAgent(int agencyId, [FromBody] RegisterReq NewAgent)
        {
            if (NewAgent == null)
            {
                return BadRequest("Invalid agent");
            }
            var Agency = _unitOfWork.AgencyRepository.Get(agencyId);
            var subscription = _unitOfWork.SubscriptionRepository.Get(Agency.SubscriptionId);
            if (Agency.NumOfAvailableAgents >= subscription.NumOfAgents)
            {
                return BadRequest("No available agents");
            }

            var exsistingAgent = await userManager.FindByEmailAsync(NewAgent.Email);
            if (exsistingAgent != null)
            {
                return BadRequest("User with same email already exists");
            }

            var NewUser = new User
            {
                Email = NewAgent.Email,
                UserName = NewAgent.Name


            };
            var result = await userManager.CreateAsync(NewUser, NewAgent.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            var role = await roleManager.FindByNameAsync(DefaultRoles.AgentRoleName);
            if (role != null)
            {
                await userManager.AddToRoleAsync(NewUser, role.Name);
            }
            var Agent = new Agent
            {
                AgencyId = agencyId,
                UserId = NewUser.Id,
                CreatedDate = DateTime.Now
            };
            Agency.NumOfAvailableAgents++;
            _unitOfWork.AgencyRepository.Update(Agency);
            _unitOfWork.AgentRepository.Add(Agent);
            _unitOfWork.Save();

            return Ok("Agent is added.");
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
        [HttpDelete("{agentId}")]
        public async Task<IActionResult> DeleteAgent(int agentId)
        {
            var agent = _unitOfWork.AgentRepository.Get(agentId);
            if (agent == null)
            {
                return NotFound("Agent not found.");
            }

            var agency = _unitOfWork.AgencyRepository.Get(agent.AgencyId);
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
            agency.NumOfAvailableAgents--;

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
        }
}
