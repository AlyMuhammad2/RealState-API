using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Apartment :Product
    {
        [Required]
        public int FloorNumber { get; set; } // رقم الطابق

        public bool HasElevatorAccess { get; set; } // هل يوجد مصعد؟

        public override string ToString()
        {
            return "Apartment"; 
        }
    }
}
