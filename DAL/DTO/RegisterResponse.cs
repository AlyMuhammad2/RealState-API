using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public record RegisterResponse
    (
        int Id ,
        string Email, 
        string Name, 
        string Roles, 
        string Token , 
        int ExpireTime, 
        string RefreshToken , 
        DateTime RefreshTokenExpiration
        
    ); 
}
