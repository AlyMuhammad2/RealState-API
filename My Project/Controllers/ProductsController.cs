using BLL.Abstractions;
using BLL.Interfaces;
using DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Project.Common;
using My_Project.DTO;
using System.Collections.Immutable;
using System.Linq.Dynamic.Core;

namespace My_Project.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("Allproducts")]
        public IActionResult GetALL([FromQuery] RequestFilters filters)
        {
            var productsQuery = _unitOfWork.ProductRepository.GetAllWithInclude(
                  query => query.Where(x =>  string.IsNullOrEmpty(filters.SearchValue) ||
                                       x.Description.Contains(filters.SearchValue) ||
                                       x.Title.Contains(filters.SearchValue) ||
                                       x.Location.Contains(filters.SearchValue)||
                                       x.Agency.Name.Contains(filters.SearchValue)||
                                       x.Agent.User.UserName.Contains(filters.SearchValue)),
               query => query.Include(p => p.Agency),
               query => query.Include(p => p.Agent).ThenInclude(ag => ag.User)).AsQueryable();
      
            if (!string.IsNullOrEmpty(filters.SortColumn))
            {
                productsQuery = productsQuery.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
            }
            if (productsQuery == null)
            {
                return NotFound();
            }
            var paginatedProducts = PaginatedList<Product>.Create(productsQuery, filters.pageNumber, filters.pageSize);
            var productCardDtos = paginatedProducts.Items.Select(p => p.Adapt<ProductCardDTO>()).ToList();
            return Ok(productCardDtos);
        }
        [HttpGet("agents/{AgentId}/products")]
        [Authorize(Roles = "Agent")]
        public IActionResult GetProductsByAgentId(int AgentId, [FromQuery] RequestFilters filters)
        {
            var productsQuery = _unitOfWork.ProductRepository.GetAllWithInclude(
                  query => query.Where(x => string.IsNullOrEmpty(filters.SearchValue) ||
                                       x.Description.Contains(filters.SearchValue) ||
                                       x.Title.Contains(filters.SearchValue) ||
                                       x.Location.Contains(filters.SearchValue)),
               query => query.Include(p => p.Agency),
               query => query.Include(p => p.Agent).ThenInclude(ag => ag.User),
               query => query.Where(p => p.AgentId == AgentId)
               ).AsQueryable();

            if (!string.IsNullOrEmpty(filters.SortColumn))
            {
                productsQuery = productsQuery.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
            }
            if (productsQuery == null)
            {
                return NotFound();
            }
            var paginatedProducts = PaginatedList<Product>.Create(productsQuery, filters.pageNumber, filters.pageSize);
            var productCardDtos = paginatedProducts.Items.Select(p => p.Adapt<ProductCardDTO>()).ToList();
            return Ok(productCardDtos);
        }
        [HttpGet("agency/{AgencyId}/products")]
        [Authorize(Roles = "Agency")]
        public IActionResult GetProductsByAgencyId(int AgencyId, [FromQuery] RequestFilters filters)
        {
            var productsQuery = _unitOfWork.ProductRepository.GetAllWithInclude(
                  query => query.Where(x => string.IsNullOrEmpty(filters.SearchValue) ||
                                       x.Description.Contains(filters.SearchValue) ||
                                       x.Title.Contains(filters.SearchValue) ||
                                       x.Location.Contains(filters.SearchValue) ||
                                       x.Agent.User.UserName.Contains(filters.SearchValue)),
               query => query.Include(p => p.Agency),
               query => query.Include(p => p.Agent).ThenInclude(ag => ag.User),
               query => query.Where(p => p.AgencyId == AgencyId)
               ).AsQueryable();

            if (!string.IsNullOrEmpty(filters.SortColumn))
            {
                productsQuery = productsQuery.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
            }
            if (productsQuery == null)
            {
                return NotFound();
            }
            var paginatedProducts = PaginatedList<Product>.Create(productsQuery, filters.pageNumber, filters.pageSize);
            var productCardDtos = paginatedProducts.Items.Select(p => p.Adapt<ProductCardDTO>()).ToList();
            return Ok(productCardDtos);
        }

    }
}
