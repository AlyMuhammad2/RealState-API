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

        [HttpPost("subscribe")]
        public IActionResult Subscribe([FromBody] SubscriptionRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.TokenId))
            {
                return BadRequest("Invalid subscription details.");
            }

            try
            {
                // هنا يمكنك إضافة منطق الاشتراك في النظام الخاص بك
                var subscription = new DAL.Models.Subscription
                {
                    // إعداد الاشتراك بناءً على نوع الاشتراك
                    // على سبيل المثال: تحديد مميزات الاشتراك بناءً على الخطة المختارة
                };

                // حفظ الاشتراك في قاعدة البيانات باستخدام _unitOfWork
                _unitOfWork.SubscriptionRepository.Add(subscription);
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