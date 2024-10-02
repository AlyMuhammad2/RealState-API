using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string SubscriptionType { get; set; } 
        public string UserType { get; set; }
        public string Description { get; set; }
        public int? NumOfsubs { get; set; }
        public int? NumOfAvailableAgents { get; set; }
        public int? NumOfAvailableproducts { get; set; }
        public int? NumOfimagesperproducts { get; set; }

        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationMonths { get; set; }
        public bool IsActive { get; set; }
        // Navigation Property
        public ICollection<Agency> Agencies { get; set; }
        public ICollection<Agent> Agents { get; set; }
    }
}