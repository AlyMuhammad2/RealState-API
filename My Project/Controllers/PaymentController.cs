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
using Stripe.Checkout;
using Microsoft.AspNetCore.Authorization;
namespace My_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StripeSettings _stripeSettings;
        private readonly IAuthentication _Authentication;

        public PaymentController(IUnitOfWork unitOfWork, IOptions<StripeSettings> stripeOptions,IAuthentication authentication)
        {
            _unitOfWork = unitOfWork;
            _stripeSettings = stripeOptions.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
            _Authentication = authentication;
        }

        //[HttpPost("validate-card")]
        //public IActionResult ValidateCard([FromBody] CardValidationRequest request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest("Invalid card details.");
        //    }

        //    try
        //    {
        //        // قم بإنشاء Token للبطاقة
        //        var tokenOptions = new TokenCreateOptions
        //        {
        //            //Card = new CardOptions
        //            //{
        //            //    Number = request.CardNumber,
        //            //    ExpMonth = request.ExpMonth,
        //            //    ExpYear = request.ExpYear,
        //            //    Cvc = request.Cvc,
        //            //},
        //        };

        //        var tokenService = new TokenService();
        //        var token = tokenService.CreateAsync(tokenOptions); // استخدام Create بدلاً من 

        //        // هنا يمكنك إضافة أي عمليات إضافية بناءً على نجاح التحقق
        //        return Ok(new { TokenId = token.Id, Message = "Card is valid." });
        //    }
        //    catch (StripeException ex)
        //    {
        //        return BadRequest(new { Message = "Card validation failed.", Error = ex.Message });
        //    }
        //}
     //   [Authorize]
        [HttpPost("subscribe/{subid}")]
        public IActionResult Subscribe(int subid)
        {
            var sub = _unitOfWork.SubscriptionRepository.Get(subid);
            if (sub == null)
            {
                return BadRequest("Invalid subscription details.");
            }

            string priceId = string.Empty;

            // Map the subscription ID to the corresponding Stripe Price ID
            switch (subid)
            {
                case 3:
                    priceId = "price_1Q6E3D2LJTCao3ZtvoD9TWKf"; // Price ID for the Basic Plan
                    break;
                case 4:
                    priceId = "price_1Q6JzD2LJTCao3ZtV03vREk6"; // Price ID for the Premium Plan
                    break;
                case 6:
                    priceId = "price_1Q6K192LJTCao3ZtzrR0qNqQ"; // Price ID for the Basic Plan
                    break;
                case 7:
                    priceId = "price_1Q6K1p2LJTCao3ZtkJMq28a9"; // Price ID for the Premium Plan
                    break;
                default:
                    return BadRequest("Invalid subscription details.");
            }

            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                var name= User.FindFirst( ClaimTypes.Name)?.Value;
                StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    SuccessUrl = "http://localhost:4200/dashboard",
                    CancelUrl = "https://example.com/cancel",
                    LineItems = new List<Stripe.Checkout.SessionLineItemOptions>
                    {
                        new Stripe.Checkout.SessionLineItemOptions
                        {
                            Price = priceId, // Use the mapped Price ID
                            Quantity = 1,
                            
                            
                        },
                       
                    },
                    Mode = "subscription",
      
                };

                var service = new Stripe.Checkout.SessionService();

                // Create the session and log it
                var session = service.Create(options);
                if (session == null)
                {
                    // Log error if session creation fails
                    return BadRequest("Failed to create Stripe session.");
                }


                // Log the session ID
                Console.WriteLine($"Stripe session created successfully: {session.Id}");
              
                    if (sub.UserType == "Agency")
                    {
                        Agency subagency = _unitOfWork.AgencyRepository.FilterIncluded("Owner", a => a.Owner.Email == userEmail);

                        subagency.Subscription = sub;
                    }
                    else if (sub.UserType == "Agent")
                    {
                        Agent subagent = _unitOfWork.AgentRepository.FilterIncluded("User", a => a.User.Email == userEmail);

                        subagent.Subscription = sub;
                    }

                _unitOfWork.Save();

                // Return the session ID
                return Ok(session);
            }
            catch (Exception ex)
            {
                // Log the error message for debugging
                Console.WriteLine($"Error during Stripe session creation: {ex.Message}");
                return BadRequest(new { Message = "Subscription creation failed.", Error = ex.Message });
            }
        }
        [HttpPost("process-payment")]
        public IActionResult ProcessPayment([FromBody] CardDetails cardDetails)
        {
            try
            {
                // Set Stripe secret API key
                StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

                // Create a payment method using the card details provided
                var paymentMethodOptions = new PaymentMethodCreateOptions
                {
                    Type = "card",
                    Card = new PaymentMethodCardOptions
                    {
                        Number = cardDetails.CardNumber,
                        ExpMonth = cardDetails.ExpMonth,
                        ExpYear = cardDetails.ExpYear,
                        Cvc = cardDetails.Cvc,
                    },
                };

                var paymentMethodService = new PaymentMethodService();
                var paymentMethod = paymentMethodService.Create(paymentMethodOptions);

                // Create a payment intent
                var paymentIntentOptions = new PaymentIntentCreateOptions
                {
                    Amount = 2000, // Payment amount in cents (e.g., $20.00)
                    Currency = "usd",
                    PaymentMethod = paymentMethod.Id,
                    ConfirmationMethod = "manual", // Or "automatic" if you prefer
                    Confirm = true, // Confirm the payment immediately
                };

                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = paymentIntentService.Create(paymentIntentOptions);

                // Return payment intent details to the front-end
                return Ok(new { PaymentIntentId = paymentIntent.Id, Status = paymentIntent.Status });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("pay")]
        public IActionResult Pay([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

                var paymentIntentOptions = new PaymentIntentCreateOptions
                {
                    Amount = 5000, // Amount in cents (e.g., $50.00)
                    Currency = "usd",
                    PaymentMethod = paymentRequest.PaymentMethodId,
                    ConfirmationMethod = "manual",
                    Confirm = true,
                };

                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = paymentIntentService.Create(paymentIntentOptions);

                if (paymentIntent.Status == "requires_action")
                {
                    // Handle additional authentication if required
                    return Ok(new { requiresAction = true, clientSecret = paymentIntent.ClientSecret });
                }

                return Ok(new { Message = "Payment successful!", PaymentIntentId = paymentIntent.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Payment failed.", details = ex.Message });
            }
        }

        // Class to hold the PaymentMethodId sent from the front-end
        public class PaymentRequest
        {
            public string PaymentMethodId { get; set; }
        }
        // Define a class to hold the card details
        public class CardDetails
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
}