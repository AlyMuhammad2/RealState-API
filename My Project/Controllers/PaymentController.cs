using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors.Infrastructure;
using My_Project.Common;
using Microsoft.EntityFrameworkCore;
namespace My_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StripeSettings _stripeSettings;

        public PaymentController(IUnitOfWork unitOfWork, IOptions<StripeSettings> stripeOptions)
        {
            _unitOfWork = unitOfWork;
            _stripeSettings = stripeOptions.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }

        [HttpPost("validate-card")]
        public IActionResult ValidateCard([FromBody] CardValidationRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid card details.");
            }

            try
            {
                // قم بإنشاء Token للبطاقة
                var tokenOptions = new TokenCreateOptions
                {
                    //Card = new CardOptions
                    //{
                    //    Number = request.CardNumber,
                    //    ExpMonth = request.ExpMonth,
                    //    ExpYear = request.ExpYear,
                    //    Cvc = request.Cvc,
                    //},
                };

                var tokenService = new TokenService();
                var token = tokenService.CreateAsync(tokenOptions); // استخدام Create بدلاً من 

                // هنا يمكنك إضافة أي عمليات إضافية بناءً على نجاح التحقق
                return Ok(new { TokenId = token.Id, Message = "Card is valid." });
            }
            catch (StripeException ex)
            {
                return BadRequest(new { Message = "Card validation failed.", Error = ex.Message });
            }
        }

        [HttpPost("subscribe/{subid}")]
        public IActionResult Subscribe(int subid)
        {
            var sub=_unitOfWork.SubscriptionRepository.Get(subid);
            if (sub==null)
            {
                return BadRequest("Invalid subscription details.");
            }

            try
            {
                var userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                if (sub.UserType == "Agency")
                {
                    Agency subagency = _unitOfWork.AgencyRepository.FilterIncluded("Owner", a => a.Owner.UserName == userName);

                    subagency.Subscription = sub;
                }
                else if (sub.UserType =="Agent")
                {
                    Agent subagent = _unitOfWork.AgentRepository.FilterIncluded("User", a => a.User.UserName == userName);

                    subagent.Subscription = sub;
                }
                _unitOfWork.Save();
                return Ok(new { Message = "Subscription created successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Subscription creation failed.", Error = ex.Message });
            }
        }
    }

        public class CardValidationRequest
        {
            public string CardNumber { get; set; }
            public int ExpMonth { get; set; }
            public int ExpYear { get; set; }
            public string Cvc { get; set; }
        }

        public class SubscriptionRequest
        {
            public string TokenId { get; set; }
            public string SubscriptionType { get; set; } // فري، شهري، سنوي
        }
}