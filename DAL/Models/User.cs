using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace YourProjectNamespace.Models
{
    public class User : IdentityUser<int>  
    {

        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }

        // Many-to-Many relationship with Role
        public ICollection<IdentityUserRole<int>> UserRoles { get; set; }

        // One-to-Many relationship with Agency
        [InverseProperty("Owner")]
        public ICollection<Agency> OwnedAgencies { get; set; }

        // One-to-One relationship with Agent
        [InverseProperty("User")]
        public Agent AgentProfile { get; set; } // Associated agent profile

        public List<RefreshToken> RefreshTokens { get; set; } = [];

    }
}
