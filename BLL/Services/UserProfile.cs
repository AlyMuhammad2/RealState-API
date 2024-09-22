using BLL.Interfaces;
using DAL.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SurveyBasket.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourProjectNamespace.Models;
using static BLL.Services.UserProfile;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BLL.Services
{
 
        public class UserProfile : IUserProfile
        {
            public UserProfile(UserManager<User> _userManager)
            {
                userManager = _userManager;
            }
            private readonly UserManager<User> userManager;

            public async Task<UserProfileResponse> GetProfile(string userId)
            {
                var user = await userManager.Users
                    .Where(x => x.Id == int.Parse(userId))
                    .FirstAsync();

                return new UserProfileResponse
                (
                    UserName : user.UserName,
                    Email : user.Email,
                    PhoneNumber : user.PhoneNumber
                );
            }


        public async Task<ChangePasswordResponse> ChangePassword(string userId, ChangePasswordRequest request)
        {
            var user = await userManager.FindByIdAsync(userId);

            var result = await userManager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);

            if (result.Succeeded)
                return new ChangePasswordResponse
                {Success = true, Message = "Password changed successfully."
                };

            var error = result.Errors.FirstOrDefault()?.Description ?? "An error occurred Passweord not changed.";

            return new ChangePasswordResponse
            {
                Success = false,
                Message = error
            };
        }
    }
    }

