using BLL.Abstractions;
using BLL.Interfaces;
using DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Project.Common;
using My_Project.DTO;
using System.Linq.Dynamic.Core;

namespace My_Project.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<Role> roleManager;


        public TasksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("task/{id}")]

        public IActionResult Get(int id)
        {
            var Task = _unitOfWork.TasksRepository.GetWithInclude(id, e => e.Id == id,
                 query => query.Include(p => p.Agent).ThenInclude(ag => ag.User)
                );
            if (Task == null)
            {
                return NotFound();
            }

            var TaskDto = (Task).Adapt<TaskResponseDTO>();


            return Ok(TaskDto);
        }
        // [Authorize(Roles = "Agency")]

        [HttpPost]
       [Route("agency/{AgencyId}/addTasks")]

        public IActionResult Add(int AgencyId,[FromBody] TaskRequestDTO TaskDTO)
        {
            if (TaskDTO == null)
            {
                return BadRequest();
            }
            TaskDTO.AgencyId = AgencyId;
            var Task = _unitOfWork.TasksRepository.Add(TaskDTO.Adapt<tasks>());
            _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new { id = Task.Id }, Task);

        }
        // [Authorize(Roles = "Agency")]

        [HttpPut]
        [Route("task/update/{id}")]

        public IActionResult Update(int id, [FromBody] TaskRequestDTO Task)
        {
            if (Task == null)
            {
                return BadRequest();
            }

            var existingTask = _unitOfWork.TasksRepository.Get(id);
            if (existingTask == null)
            {
                return NotFound();
            }
            Task.Id = existingTask.Id;

            _unitOfWork.TasksRepository.Update(Task.Adapt(existingTask));
            _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete]
        [Route("task/delete/{id}")]

        public IActionResult Delete(int id)
        {
            var existingTask = _unitOfWork.TasksRepository.Get(id);
            if (existingTask is null)
            {
                return NotFound();
            }
            _unitOfWork.TasksRepository.Delete(existingTask.Id);
            _unitOfWork.Save();
            return NoContent();


        }
        [HttpGet("agent/{AgentId}/tasks")]
        // [Authorize(Roles = "Agent")]
        public IActionResult GetByAgentId(int AgentId, [FromQuery] RequestFilters filters)
        {
            var TasksQuery = _unitOfWork.TasksRepository.GetAllWithInclude(
                  query => query.Where(x => string.IsNullOrEmpty(filters.SearchValue) ||
                                       x.TaskName.Contains(filters.SearchValue) ||
                                       x.Description.Contains(filters.SearchValue) ||
                                       x.Status.Contains(filters.SearchValue)),
                query => query.Include(p => p.Agency),
               query => query.Include(p => p.Agent).ThenInclude(ag => ag.User),
               query => query.Where(p => p.AgentId == AgentId)
               ).AsQueryable();

            if (!string.IsNullOrEmpty(filters.SortColumn))
            {
                TasksQuery = TasksQuery.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
            }
            if (TasksQuery == null)
            {
                return NotFound();
            }
            var paginatedTasks = PaginatedList<tasks>.Create(TasksQuery, filters.pageNumber, filters.pageSize);
            var TasksDto = paginatedTasks.Items.Select(p => p.Adapt<TaskResponseDTO>()).ToList();
            return Ok(TasksDto);
        }
        [HttpGet("agency/{AgencyId}/tasks")]
        // [Authorize(Roles = "Agency")]
        public IActionResult GetByAgencyId(int AgencyId, [FromQuery] RequestFilters filters)
        {
            var TasksQuery = _unitOfWork.TasksRepository.GetAllWithInclude(
                  query => query.Where(x => string.IsNullOrEmpty(filters.SearchValue) ||
                                       x.TaskName.Contains(filters.SearchValue) ||
                                       x.Description.Contains(filters.SearchValue) ||
                                       x.Status.Contains(filters.SearchValue)||
                                       x.Agent.User.UserName.Contains(filters.SearchValue)
                                       ),
                query => query.Include(p => p.Agency),
               query => query.Include(p => p.Agent).ThenInclude(ag => ag.User),
               query => query.Where(p => p.AgencyId == AgencyId)
               ).AsQueryable();

            if (!string.IsNullOrEmpty(filters.SortColumn))
            {
                TasksQuery = TasksQuery.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
            }
            if (TasksQuery == null)
            {
                return NotFound();
            }
            var paginatedTasks = PaginatedList<tasks>.Create(TasksQuery, filters.pageNumber, filters.pageSize);
            var TasksDto = paginatedTasks.Items.Select(p => p.Adapt<TaskResponseDTO>()).ToList();
            return Ok(TasksDto);
        }
    }
}
