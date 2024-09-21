using BLL.Interfaces;
using DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using My_Project.DTO;

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
            var houses = _unitOfWork.HouseRepository.GetAllWithInclude(
                 query => query.Include(p => p.Agency).ThenInclude(ay=>ay.Owner),
                 query => query.Include(p => p.Agent).ThenInclude(ag => ag.User)
                );
            if (houses == null)
            {
                return NotFound();
            }

            var HouseDto = (houses).Adapt<IEnumerable<HouesResponseDTO>>();


            return Ok(HouseDto); 
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var house = _unitOfWork.HouseRepository.GetWithInclude(id,e=>e.Id==id,
                 query => query.Include(p => p.Agency).ThenInclude(ay => ay.Owner),
                 query => query.Include(p => p.Agent).ThenInclude(ag => ag.User)
                );
            if (house == null)
            {
                return NotFound();
            }

            var HouseDto = (house).Adapt<HouesResponseDTO>();


            return Ok(HouseDto);
        }

        [HttpPost]
        public IActionResult Add([FromBody]HouseRequestDTO houseDTO)
        {
            if (houseDTO == null)
            {
                return BadRequest();
            }
            var house =_unitOfWork.HouseRepository.Add(houseDTO.Adapt<House>());
            _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new { id = house.Id }, house);

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] HouseRequestDTO house)
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
