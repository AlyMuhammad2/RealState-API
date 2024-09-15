using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace My_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AgencyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        [HttpPost]
        public IActionResult CreateAgent([FromBody] Agency agent)
        {
            if (agent == null)
            {
                return BadRequest();
            }

            _unitOfWork.AgencyRepository.Add(agent);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetAgncy), new { id = agent.Id }, agent);
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
