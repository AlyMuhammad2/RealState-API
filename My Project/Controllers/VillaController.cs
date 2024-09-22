using BLL.Interfaces;
using DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Project.DTO;

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
        public IActionResult GetAll()
        {
            var Villas = _unitOfWork.VillaRepository.GetAllWithInclude(
                 query => query.Include(p => p.Agency).ThenInclude(ay => ay.Owner),
                 query => query.Include(p => p.Agent).ThenInclude(ag => ag.User)
                );
            if (Villas == null)
            {
                return NotFound();
            }

            var VillaDto = (Villas).Adapt<IEnumerable<VillaResponseDTO>>();


            return Ok(VillaDto);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var Villa = _unitOfWork.VillaRepository.GetWithInclude(id, e => e.Id == id,
                 query => query.Include(p => p.Agency).ThenInclude(ay => ay.Owner),
                 query => query.Include(p => p.Agent).ThenInclude(ag => ag.User)
                );
            if (Villa == null)
            {
                return NotFound();
            }

            var VillaDto = (Villa).Adapt<VillaResponseDTO>();


            return Ok(VillaDto);
        }

        [HttpPost]
        public IActionResult Add([FromBody] VillaRequestDto VillaDTO)
        {
            if (VillaDTO == null)
            {
                return BadRequest();
            }
            var Villa = _unitOfWork.VillaRepository.Add(VillaDTO.Adapt<Villa>());
            _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new { id = Villa.Id }, Villa);

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] VillaRequestDto Villa)
        {
            if (Villa == null)
            {
                return BadRequest();
            }

            var existingVilla = _unitOfWork.VillaRepository.Get(id);
            if (existingVilla == null)
            {
                return NotFound();
            }
            Villa.Id = existingVilla.Id;

            _unitOfWork.VillaRepository.Update(Villa.Adapt(existingVilla));
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
