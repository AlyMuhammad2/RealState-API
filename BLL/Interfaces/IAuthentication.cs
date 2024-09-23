using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;
using Microsoft.AspNetCore.Identity.Data;

namespace BLL.Interfaces
{
    public interface IAuthentication
    {
        public  Task<AuthResponse?> LoginAuth(string email, string password , CancellationToken cancellationToken=default);
        public  Task<AuthResponse?> GetRefreshToken(string token, string refreshToken , CancellationToken cancellationToken=default);
        public  Task<AuthResponse?> Register(RegisterReq registerRequest, CancellationToken cancellationToken = default); 

    }
}
