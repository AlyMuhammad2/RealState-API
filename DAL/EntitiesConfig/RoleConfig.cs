using DAL.Default;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourProjectNamespace.Models;

namespace DAL.EntitiesConfig
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {

            builder.HasData(

                new Role
                {
                    Id = DefaultRoles.AdminRoleId,
                    RoleName=DefaultRoles.AdminRoleName,
                    Name = DefaultRoles.AdminRoleName,
                    NormalizedName = DefaultRoles.AdminRoleName.ToUpper(),                    
                    ConcurrencyStamp = DefaultRoles.AdminRoleConcurrencyStamp     
                } , new Role
               {
                   Id = DefaultRoles.AgencyRoleId,
                    RoleName = DefaultRoles.AgencyRoleName,

                    Name = DefaultRoles.AgencyRoleName,
                    NormalizedName = DefaultRoles.AgencyRoleName.ToUpper(),                    
                    ConcurrencyStamp = DefaultRoles.AgencyRoleConcurrencyStamp
                } , new Role
                {
                    Id = DefaultRoles.AgentRoleId,
                    RoleName = DefaultRoles.AgentRoleName,

                    Name = DefaultRoles.AgentRoleName,
                    NormalizedName = DefaultRoles.AgentRoleName.ToUpper(),
                    ConcurrencyStamp = DefaultRoles.AgentRoleConcurrencyStamp
                }
            );
        }
    }
}
