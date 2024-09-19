using BLL.Interfaces;
using DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var Apartments = _unitOfWork.ApartmentRepository.GetAll();
            return Ok(Apartments);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var Apartment = _unitOfWork.ApartmentRepository.Get(id);
            return Ok(Apartment);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Apartment Apartment)
        {
            if (Apartment == null)
            {
                return BadRequest();
            }
            _unitOfWork.ApartmentRepository.Add(Apartment);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new { id = Apartment.Id }, Apartment);

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Apartment Apartment)
        {
            if (Apartment == null ||Apartment.Id!=id)
            {
                return BadRequest();
            }
            var existingApartment = _unitOfWork.ApartmentRepository.Get(id);
            if(existingApartment==null)
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
