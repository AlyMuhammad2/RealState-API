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
    public class ApartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var Apartments = _unitOfWork.ApartmentRepository.GetAllWithInclude(
                 query => query.Include(p => p.Agency).ThenInclude(ay => ay.Owner),
                 query => query.Include(p => p.Agent).ThenInclude(ag => ag.User)
                );
            if (Apartments == null)
            {
                return NotFound();
            }

            var ApartmentDto = (Apartments).Adapt<IEnumerable<ApartmentResponseDTO>>();


            return Ok(ApartmentDto);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var Apartment = _unitOfWork.ApartmentRepository.GetWithInclude(id, e => e.Id == id,
                 query => query.Include(p => p.Agency).ThenInclude(ay => ay.Owner),
                 query => query.Include(p => p.Agent).ThenInclude(ag => ag.User)
                );
            if (Apartment == null)
            {
                return NotFound();
            }

            var ApartmentDto = (Apartment).Adapt<ApartmentResponseDTO>();


            return Ok(ApartmentDto);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ApartmentRequestDto ApartmentDTO)
        {
            if (ApartmentDTO == null)
            {
                return BadRequest();
            }
            var Apartment = _unitOfWork.ApartmentRepository.Add(ApartmentDTO.Adapt<Apartment>());
            _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new { id = Apartment.Id }, Apartment);

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ApartmentRequestDto Apartment)
        {
            if (Apartment == null)
            {
                return BadRequest();
            }

            var existingApartment = _unitOfWork.ApartmentRepository.Get(id);
            if (existingApartment == null)
            {
                return NotFound();
            }
            Apartment.Id = existingApartment.Id;

            _unitOfWork.ApartmentRepository.Update(Apartment.Adapt(existingApartment));
            _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingApartment = _unitOfWork.ApartmentRepository.Get(id);
            if (existingApartment is null)
            {
                return NotFound();
            }
            _unitOfWork.ApartmentRepository.Delete(existingApartment.Id);
            _unitOfWork.Save();
            return NoContent();


        }
    }
}
