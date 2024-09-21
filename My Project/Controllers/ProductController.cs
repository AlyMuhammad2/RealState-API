using BLL.Interfaces;
using DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Project.DTO;
using System.Collections.Immutable;

namespace My_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var products = _unitOfWork.ProductRepository.GetAllWithInclude(
     query => query.Include(p => p.Agency),
     query => query.Include(p => p.Agent).ThenInclude(ag => ag.User)
 );

            if (products == null)
            {
                return NotFound(); 
            }

////            var productAgency = _unitOfWork.AgencyRepository.Get((int)product.AgencyId);
//  /          var productAgent = _unitOfWork.AgentRepository.FilterIncluded("User", a => a.Id == product.AgentId).FirstOrDefault();
//            if (productAgent == null || productAgency==null)
//            {
//                return NotFound();
//            }
            var productCardDto = (products).Adapt<IEnumerable<ProductCardDTO>>();

           
            return Ok(productCardDto); // Return the DTO
        }
        [HttpPost]
        public IActionResult Add([FromBody]ProductCardDTO product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            _unitOfWork.ProductRepository.Add(product.Adapt<Product>());
            _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), product);

        }
    }
}
