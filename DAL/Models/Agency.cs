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
    public class Agency
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [ForeignKey("Owner")]
        public int OwnerId { get; set; } // Foreign Key to User

        public int NumOfAvailableAgents { get; set; }

        [ForeignKey("Subscription")]
        public int SubscriptionId { get; set; } // Foreign Key to Subscription

        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        // Navigation Properties
        [InverseProperty("OwnedAgencies")]
        public User Owner { get; set; }

        public Subscription Subscription { get; set; }

        // One-to-Many relationship with Agents
        public ICollection<Agent> Agents { get; set; }

        // One-to-Many relationship with Payments
        public ICollection<Payment> Payments { get; set; }

        [InverseProperty("Agency")]
        public ICollection<Product> Products { get; set; } // One-to-Many relationship with Product

    }
}