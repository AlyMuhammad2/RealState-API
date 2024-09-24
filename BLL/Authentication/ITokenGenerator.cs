using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourProjectNamespace.Models;

namespace BLL.Authentication
{
    public interface ITokenGenerator
    {
        (string token , int expiration ) GenerateToken(User user , IEnumerable<string> Roles);
        string? ValidateToken(string token);
    }
}
