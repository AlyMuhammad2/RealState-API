using BLL.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Contracts.Users;

namespace My_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserProfile userProfile;
        public AccountController(IUserProfile _userProfile)
        {
            userProfile = _userProfile;
        }

        [HttpGet("info")]
        public async Task<IActionResult> Info(string id)
        {
            var result = await userProfile.GetProfile(id/*User.GetUserId()!*/);

            return Ok(result);
        }
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string id , ChangePasswordRequest  request)
        {
            var result = await userProfile.ChangePassword(id , request/*User.GetUserId()!*/);

            if (result.Success)
            {
                return Ok(result.Message); 
            }
    
            return BadRequest(result.Message);
        }
    }
}
