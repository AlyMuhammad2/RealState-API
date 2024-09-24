using DAL.DTO;
using SurveyBasket.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserProfile
    {
        public Task<UserProfileResponse> GetProfile(string userId);
        public Task<ChangePasswordResponse> ChangePassword(string userId, ChangePasswordRequest request); 

    }
}
