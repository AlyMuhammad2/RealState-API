﻿using BLL.Interfaces;
using DAL.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using My_Project.DTO;
using YourProjectNamespace.Models;

namespace My_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication Authentication;
        private readonly UserManager<User> userManager;
        public AuthenticationController(IAuthentication _Authentication,UserManager<User> _userManager )
        {
            Authentication = _Authentication;
            userManager = _userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestt loginRequest , CancellationToken cancellationToken)
        {
            var result = await Authentication.LoginAuth(loginRequest.Email, loginRequest.Password, cancellationToken);
            return result == null ? BadRequest("Invalid Email or Password") : Ok(result);

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterReq registerRequest, CancellationToken cancellationToken)
        {

            var result = await Authentication.Register(registerRequest, cancellationToken);
            return result == null ? BadRequest("Invalid Email or Password") : Ok(result);

        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var authResult = await Authentication.GetRefreshToken(request.Token, request.RefreshToken, cancellationToken);

            return authResult is null ?BadRequest("invalid "): Ok (authResult);
        }
        
    }
}

