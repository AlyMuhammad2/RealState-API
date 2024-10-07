using DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using My_Project.DTO;

namespace My_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]

        public IActionResult CreateSubscription([FromBody] SubscriptionRequestDTO subscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var activeSubscription = _unitOfWork.SubscriptionRepository
            //    .Find(s => s.IsActive && s.StartDate.AddMonths(s.DurationMonths) > DateTime.Now)
            //    .FirstOrDefault();

            //if (activeSubscription != null)
            //{
            //    return BadRequest(new { message = "You already have an active subscription." });
            //}

            subscription.StartDate = DateTime.Now;
            subscription.IsActive = true;

            _unitOfWork.SubscriptionRepository.Add(subscription.Adapt<Subscription>());
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetSubscriptionById), new { id = subscription.SubId }, subscription);
        }

        //[Authorize(Roles = "Admin")]


        [HttpGet("{id}")]
        public IActionResult GetSubscriptionById(int id)
        {
            var subscription = _unitOfWork.SubscriptionRepository.Get(id);
            if (subscription == null)
            {
                return NotFound(new { message = "Subscription not found" });
            }
            if (subscription.StartDate.AddMonths(subscription.DurationMonths) <= DateTime.Now)
            {
                subscription.IsActive = false;
                _unitOfWork.Save();
            }

            return Ok(subscription);
        }
        //[Authorize(Roles = "Admin")]

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SubscriptionRequestDTO subscription)
        {
            if (subscription == null)
            {
                return BadRequest();
            }

            var existingsubscription = _unitOfWork.SubscriptionRepository.Get(id);
            if (existingsubscription == null)
            {
                return NotFound();
            }
            subscription.SubId = existingsubscription.Id;
            _unitOfWork.SubscriptionRepository.Update(subscription.Adapt(existingsubscription));
            _unitOfWork.Save();

            return NoContent();
        }
        //[Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingsubscription = _unitOfWork.SubscriptionRepository.Get(id);
            if (existingsubscription is null)
            {
                return NotFound();
            }
            _unitOfWork.ApartmentRepository.Delete(existingsubscription.Id);
            _unitOfWork.Save();
            return NoContent();


        }
        [HttpGet]

        public IActionResult GetAllSubscriptions()
        {
            var subscriptions = _unitOfWork.SubscriptionRepository.GetAllWithInclude( );

       

            return Ok(subscriptions);
        }
       

    }

}

