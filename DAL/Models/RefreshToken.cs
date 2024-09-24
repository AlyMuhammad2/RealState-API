using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourProjectNamespace.Models;


namespace DAL.Models
{
    [Owned]
    public class RefreshToken
    {
        public RefreshToken() { }
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpireOn { get; set; }
        public DateTime createdOn { get; set; }

        public DateTime? RevokeOn { get; set; }
        public bool IsExpired
        {
            get { return DateTime.UtcNow >= ExpireOn; }
        }
        public bool IsActive => RevokeOn is null && !IsExpired;
        [ForeignKey("user")]
        public int UserId { get; set; }
        public User? user { get; set; }


    }
}
