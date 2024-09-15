using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace My_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AgentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAgents()
        {
            var Agents = _unitOfWork.AgentRepository.GetAll();
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
        [HttpPost]
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
        public IActionResult UpdateAgent(int id, [FromBody] Agent agent)
        {
            if (agent == null || agent.Id != id)
            {
                return BadRequest();
            }

            var existingAgent = _unitOfWork.AgentRepository.Get(id);
            if (existingAgent == null)
            {
                return NotFound();
            }

            _unitOfWork.AgentRepository.Update(agent);
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

            _unitOfWork.AgentRepository.Delete(agent.Id);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
