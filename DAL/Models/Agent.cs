using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourProjectNamespace.Models;

namespace DAL.Models
{
    public class Agent
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; } // Foreign Key to User

        [ForeignKey("Agency")]
        public int? AgencyId { get; set; } // Foreign Key to Agency

        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        // Navigation Properties
        [InverseProperty("AgentProfile")]
        public User User { get; set; }

        [InverseProperty("Agents")]
        public Agency Agency { get; set; }

        [ForeignKey("Subscription")]
        public int? SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }

        // One-to-Many relationship with Tasks
        public ICollection<tasks> Tasks { get; set; }
        [InverseProperty("Agent")]
        public ICollection<Product> Products { get; set; }
    }
}
