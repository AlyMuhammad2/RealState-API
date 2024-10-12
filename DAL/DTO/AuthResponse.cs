using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public record AuthResponse
    (
        int Id ,
        int userid,
        string Email, 
        string Name, 
        string Token , 
        int ExpireTime, 
        string RefreshToken , 
        DateTime RefreshTokenExpiration
        
    ); 
}
