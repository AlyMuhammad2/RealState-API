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
        public string SubscriptionType { get; set; } // e.g. Free , Monthly, Yearly
        public string Description { get; set; }
        public int? NumOfAgents { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationMonths { get; set; }
        public bool IsActive { get; set; }
        // Navigation Property
        public ICollection<Agency> Agencies { get; set; }
        public ICollection<Agent> Agents { get; set; }
    }
}