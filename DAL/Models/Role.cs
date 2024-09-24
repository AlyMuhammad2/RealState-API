using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourProjectNamespace.Models;

namespace DAL.Models
{
    public class Role : IdentityRole<int>
    {
        public string RoleName { get; set; }

        public ICollection<IdentityUserRole<int>> UserRoles { get; set; }

    }
}
