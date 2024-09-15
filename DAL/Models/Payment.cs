using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Agency")]
        public int AgencyId { get; set; } // Foreign Key to Agency

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal
        public bool IsSuccessful { get; set; }

        // Navigation Property
        [InverseProperty("Payments")]
        public Agency Agency { get; set; }
    }
}
