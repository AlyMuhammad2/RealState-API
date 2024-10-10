using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Location { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsAvailable { get; set; }

        public bool? IsForRent { get; set; }
        //public bool? IsForSale { get; set; }
        public double? Area { get; set; }
        public int? NumOfBedroom {  get; set; }
        public int? NumOfBathrom { get; set; }
        public int? NumOfCars { get; set; }
        
        [ForeignKey("Agent")]
        public int? AgentId { get; set; } // Foreign Key to Agent
        public Agent? Agent { get; set; }

        [ForeignKey("Agency")]
        public int? AgencyId { get; set; } // Foreign Key to Agency
        public Agency? Agency { get; set; }

        // Navigation Properties

        public string? PrimaryImg { get; set; }
        public List<string>? images { get; set; }
    }
}
