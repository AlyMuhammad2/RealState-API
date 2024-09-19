using BLL.Interfaces;
using DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace My_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public HouseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var houses = _unitOfWork.HouseRepository.GetAll();
            return Ok(houses);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var house = _unitOfWork.HouseRepository.Get(id);
            return Ok(house);
        }

        [HttpPost]
        public IActionResult Add([FromBody] House house)
        {
            if (house == null)
            {
                return BadRequest();
            }
            _unitOfWork.HouseRepository.Add(house);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new { id = house.Id }, house);

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] House house)
        {
            if (house == null)
            {
                return BadRequest();
            }

            var existingHouse = _unitOfWork.HouseRepository.Get(id);
            if (existingHouse == null)
            {
                return NotFound();
            }
            house.Id=existingHouse.Id;

            _unitOfWork.HouseRepository.Update(house.Adapt(existingHouse));
            _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingHouse = _unitOfWork.HouseRepository.Get(id);
            if (existingHouse is null)
            {
                return NotFound();
            }
            _unitOfWork.HouseRepository.Delete(existingHouse.Id);
            _unitOfWork.Save();
            return NoContent();


        }
    }
}
