using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using YourProjectNamespace.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DAL.Default;

namespace DAL.EntitiesConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {


            var passwordHasher = new PasswordHasher<User>();


            builder.HasData(

                new User
                {
                    Id = DefaultUsers.AdminId, 
                    UserName = DefaultUsers.AdminUserName,  
                    NormalizedUserName = DefaultUsers.AdminUserName.ToUpper(),
                    Email = DefaultUsers.AdminEmail,
                    NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
                    SecurityStamp = DefaultUsers.AdminSecurityStamp,
                    ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
                    EmailConfirmed = true,
                    PasswordHash = passwordHasher.HashPassword(null!, DefaultUsers.AdminPassword)
                }
            );
        }
    }
}
