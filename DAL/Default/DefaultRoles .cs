using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Default
{
    public static class DefaultRoles
    {
        public const int AdminRoleId = 1 ;
        public const string AdminRoleName = "Admin";
        public const string AdminNormalizedName = "ADMIN";
        //public string AdminDescription = "Admin Role";  
        public  const string AdminRoleConcurrencyStamp = "35d2aakk-bc54-4248-a172-a77de3ae2321";

        public const int AgencyRoleId = 2;
        public const string AgencyRoleName = "Agency";
        public const string AgencyNormalizedName = "AGENCY";
        //public string AdminDescription = "Agency Role";  
        public  const string AgencyRoleConcurrencyStamp = "35d2aakk-bc54-5678-a172-a77de3kk2321";


        public const int AgentRoleId = 3;
        public const string AgentRoleName = "Agent";
        public const string AgentNormalizedName = "AGENT";
        //public string AdminDescription = "Agent Role";  
        public const string AgentRoleConcurrencyStamp = "99d2aakk-bc54-5628-a172-a77de3gk8977";

    }

}
