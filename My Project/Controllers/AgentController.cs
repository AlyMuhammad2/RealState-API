using BLL.Abstractions;
using BLL.Interfaces;
using DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Project.Common;
using My_Project.DTO;
using System.Linq.Dynamic.Core;
namespace My_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<Role> roleManager;

        public AgentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
      
        [HttpGet("{id}")]
        public IActionResult GetAgent(int id)
        {
            var agent = _unitOfWork.AgentRepository.GetWithInclude(id, e => e.Id == id,
                 query => query.Include(p => p.Agency).ThenInclude(ay => ay.Owner),
                 query => query.Include(p => p.User),
                 query => query.Include(p => p.Agency),
                 query => query.Include(p => p.Products),
                 query => query.Include(p => p.Subscription),
                 query => query.Include(p => p.Tasks)
                );
            if (agent == null)
            {
                return NotFound();
            }

            var agentDto = (agent).Adapt<AgentResponseDTO>();


            return Ok(agentDto);
        }
        [HttpPost]
        // [Authorize(Roles = "Agency")]
        public IActionResult CreateAgent([FromBody] Agent agent)
        {
            if (agent == null)
            {
                return BadRequest();
            }

            _unitOfWork.AgentRepository.Add(agent);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetAgent), new { id = agent.Id }, agent);
        }
        [HttpPut("{id}")]
        // [Authorize(Roles = "Agency")]
        public IActionResult UpdateAgent(int id, [FromBody] AgentRequestDTO agent)
        {

            if (agent == null)
            {
                return BadRequest();
            }

            var existingagent = _unitOfWork.AgentRepository.Get(id);
            if (existingagent == null)
            {
                return NotFound();
            }
            agent.id = existingagent.Id;

            _unitOfWork.AgentRepository.Update(agent.Adapt(existingagent));
            _unitOfWork.Save();

            return NoContent();
        }
        [HttpDelete("{id}")]
        // [Authorize(Roles = "Agency")]
        public IActionResult DeleteAgent(int id)
        {
            var agent = _unitOfWork.AgentRepository.Get(id);
            if (agent == null)
            {
                return NotFound();
            }

            _unitOfWork.AgentRepository.Delete(agent.Id);
            _unitOfWork.Save();

            return NoContent();
        }
        
       
        
    }
}
