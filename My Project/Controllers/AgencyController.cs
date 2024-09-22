using BLL.Interfaces;
using DAL.Default;
using DAL.DTO;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public AgencyController(IUnitOfWork unitOfWork , UserManager<User> _userManager , RoleManager<Role> _roleManager)
        {
            _unitOfWork = unitOfWork;
            userManager = _userManager;
            roleManager = _roleManager;
        }
        [HttpGet]
        public IActionResult GetAgencies()
        {
            var Agents = _unitOfWork.AgencyRepository.GetAll();
            return Ok(Agents);
        }
        [HttpGet("{id}")]
        public IActionResult GetAgncy(int id)
        {
            var Agent = _unitOfWork.AgencyRepository.Get(id);
            if (Agent == null)
            {
                return NotFound();
            }
            return Ok(Agent);
        }
        [HttpPost("{agencyId}/add-agent")]
        public async Task<IActionResult> CreateAgent(int agencyId, [FromBody] RegisterReq NewAgent)
        {
            if (NewAgent == null)
            {
                return BadRequest();
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
    }
}
