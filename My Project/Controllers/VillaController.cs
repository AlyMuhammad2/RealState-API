using BLL.Interfaces;
using DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace My_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetVillas()
        {
            var villas = _unitOfWork.VillaRepository.GetAll();
            return Ok(villas);
        }
        [HttpGet("{id}")]
        public IActionResult GetVilla(int id) 
        {
            var villa=_unitOfWork.VillaRepository.Get(id);
            return Ok(villa);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Villa villa)
        {
            if (villa == null)
            {
                return BadRequest();
            }
            _unitOfWork.VillaRepository.Add(villa);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetVilla), new { id = villa.Id }, villa);

        }
        [HttpPut("{id}")]
        public IActionResult updateVilla(int id, [FromBody] Villa villa)
        {
            if (villa == null)
            {
                return BadRequest();
            }

            var existingVilla = _unitOfWork.VillaRepository.Get(id);
            if(existingVilla == null)
            {
                return NotFound();
            }
            villa.Id = existingVilla.Id;
            _unitOfWork.VillaRepository.Update(villa.Adapt(existingVilla));
            _unitOfWork.Save();

            return NoContent();
        }

            [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var existingVilla = _unitOfWork.VillaRepository.Get(id);
            if (existingVilla is null)
            { 
            return NotFound();
            }
            _unitOfWork.VillaRepository.Delete(existingVilla.Id);
            _unitOfWork.Save();
            return NoContent();


        }
    }
}
