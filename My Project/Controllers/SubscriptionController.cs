using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CreateSubscription([FromBody] Subscription subscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activeSubscription = _unitOfWork.SubscriptionRepository
                .Find(s => s.IsActive && s.StartDate.AddMonths(s.DurationMonths) > DateTime.Now)
                .FirstOrDefault();

            if (activeSubscription != null)
            {
                return BadRequest(new { message = "You already have an active subscription." });
            }

            subscription.StartDate = DateTime.Now;
            subscription.IsActive = true;

            _unitOfWork.SubscriptionRepository.Add(subscription);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetSubscriptionById), new { id = subscription.Id }, subscription);
        }
      


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

        //[HttpGet]
        //public  IActionResult GetAllSubscriptions()
        //{
        //    var subscriptions =  _unitOfWork.SubscriptionRepository.GetAll();
        //    return Ok(subscriptions);
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetSubscriptionById(int id)
        //{
        //    var subscription =  _unitOfWork.SubscriptionRepository.Get(id);
        //    if (subscription == null)
        //    {
        //        return NotFound(new { message = "Subscription not found" });
        //    }

        //    return Ok(subscription);
        //}

        //[HttpPost]
        //public IActionResult CreateSubscription([FromBody] Subscription subscription)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //     _unitOfWork.SubscriptionRepository.Add(subscription);
        //    _unitOfWork.Save();

        //    return CreatedAtAction(nameof(GetSubscriptionById), new { id = subscription.Id }, subscription);
        //}

        //[HttpPut("{id}")]
        //public  IActionResult UpdateSubscription(int id, [FromBody] Subscription subscription)
        //{
        //    var existingSubscription =  _unitOfWork.SubscriptionRepository.Get(id);
        //    if (existingSubscription == null)
        //    {
        //        return NotFound(new { message = "Subscription not found" });
        //    }

        //    existingSubscription.SubscriptionType = subscription.SubscriptionType;
        //    existingSubscription.Price = subscription.Price;
        //    existingSubscription.Description = subscription.Description;
        //    existingSubscription.IsActive = subscription.IsActive;

        //    _unitOfWork.Save();

        //    return Ok(existingSubscription);
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteSubscription(int id)
        //{
        //    var subscription =  _unitOfWork.SubscriptionRepository.Get(id);
        //    if (subscription == null)
        //    {
        //        return NotFound(new { message = "Subscription not found" });
        //    }

        //    _unitOfWork.SubscriptionRepository.Delete(subscription.Id);
        //    _unitOfWork.Save();

        //    return NoContent();
        //}
    }
}
