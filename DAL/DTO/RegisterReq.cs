using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public record RegisterReq
    (
        string Name,
        string? AgencyName,
        string Email , 
        string Password, 
        string ConfirmPassword,
        string phoneNumber,
        string AccountType
    );
   
}
