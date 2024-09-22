using DAL.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;


namespace My_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin login;
        public LoginController(ILogin _login )
        {
            login = _login;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestt loginRequest , CancellationToken cancellationToken)
        {
            var result = await login.LoginAuth(loginRequest.Email, loginRequest.Password, cancellationToken);
            return result == null ? BadRequest("Invalid Email or Password") : Ok(result);

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterReq registerRequest, CancellationToken cancellationToken)
        {
            var result = await login.Register(registerRequest, cancellationToken);
            return result == null ? BadRequest("Invalid Email or Password") : Ok(result);

        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var authResult = await login.GetRefreshToken(request.Token, request.RefreshToken, cancellationToken);

            return authResult is null ?BadRequest("invalid "): Ok (authResult);
        }
    }
}
