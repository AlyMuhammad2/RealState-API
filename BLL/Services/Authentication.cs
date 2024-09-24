using BLL.Authentication;
using BLL.Interfaces;
using DAL.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using YourProjectNamespace.Models;
using DAL.Models;
using DAL.Default;



namespace BLL.Services
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<User> UserManager;
        private readonly ITokenGenerator tokenGenerator;
        private readonly SignInManager<User> signInManager;
        private readonly int ExpireDays = 20;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUnitOfWork unitOfWork; 
        public Authentication( UserManager<User> _userManager , ITokenGenerator _tokenGenerator , 
            SignInManager<User> signInManager , RoleManager<Role> roleManager, IUnitOfWork _unitOfWork) 
        {
            unitOfWork = _unitOfWork;
            UserManager = _userManager;
            tokenGenerator = _tokenGenerator;
            this.signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<AuthResponse?> LoginAuth(string email, string password, CancellationToken cancellationToken = default)
        {
            var user =  await UserManager.FindByEmailAsync(email);
            if (user == null) 
            {
                return null; 
            }

            var result = await signInManager.PasswordSignInAsync(user, password, false, false);
            if (result.Succeeded)
            {
                //Generate the Token :) 
                var UserRoles = await UserManager.GetRolesAsync(user);
                var (Token, ExpireDate) = tokenGenerator.GenerateToken(user , UserRoles);
                var refresh_token = GenerateRefreshToken();
                var refresh_token_expiry = DateTime.Now.AddDays(ExpireDays);
                user.RefreshTokens.Add(new RefreshToken
                {
                    Token = refresh_token,
                    ExpireOn = refresh_token_expiry
                });
                await UserManager.UpdateAsync(user);
                var response = new AuthResponse(user.Id, user.Email, user.UserName, Token, ExpireDate, refresh_token, refresh_token_expiry);

                return response;
            }
            else
            {
                return null;
            }
        }

        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        public async Task<AuthResponse?> GetRefreshToken(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = tokenGenerator.ValidateToken(token);

            if (userId is null)
                return null;

            var user = await UserManager.FindByIdAsync(userId);

            if (user is null)
                return null;

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (userRefreshToken is null)
                return null;

            userRefreshToken.RevokeOn = DateTime.UtcNow;
            var UserRoles = await UserManager.GetRolesAsync(user);

            var (newToken, expiresIn) = tokenGenerator.GenerateToken(user, UserRoles);
            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(ExpireDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpireOn = refreshTokenExpiration
            });

            await UserManager.UpdateAsync(user);

            var response = new AuthResponse(user.Id, user.Email, user.UserName, newToken, expiresIn, newRefreshToken, refreshTokenExpiration);
            return response; 
        }
        public async Task<AuthResponse?> Register(RegisterReq registerRequest, CancellationToken cancellationToken = default)
        {
            //var EmailisExist = await UserManager.Users.AnyAsync(x => x.Email == registerRequest.Email, cancellationToken);
            var EmailisExist = await UserManager.FindByEmailAsync(registerRequest.Email);

            if (EmailisExist != null)
            {
                return null;
            }
            var user = new User
            {  
                Email = registerRequest.Email,
                UserName = registerRequest.Name,


               
            };
            var result = await UserManager.CreateAsync(user, registerRequest.Password);

            if (result.Succeeded)
            {
                IEnumerable<string> Roles = new List<string> { "Agency" };
                var (Token, ExpireDate) = tokenGenerator.GenerateToken(user , Roles);
                var refresh_token = GenerateRefreshToken();
                var refresh_token_expiry = DateTime.Now.AddDays(ExpireDays);
                user.RefreshTokens.Add(new RefreshToken
                {
                    Token = refresh_token,
                    ExpireOn = refresh_token_expiry
                });

                await UserManager.UpdateAsync(user);
                var role = await _roleManager.FindByNameAsync(DefaultRoles.AgencyRoleName);
                if (role != null)
                {
                    await UserManager.AddToRoleAsync(user, role.Name);
                }
                var agency = new Agency
                {
                    Name = registerRequest.Name,
                    OwnerId = user.Id,
                    CreatedDate = DateTime.Now,
                    SubscriptionId = 1
                }; 
                unitOfWork.AgencyRepository.Add(agency);
                unitOfWork.Save();
                var response = new AuthResponse(user.Id, user.Email, user.UserName, Token, ExpireDate, refresh_token, refresh_token_expiry);
               // await UserManager.AddToRoleAsync(user, /*DefaultRoles.AgencyRoleName*/"Agency");


                return response;
            }
            // var error= result.Errors.FirstOrDefault();
            return null; 
        }

    }
}
