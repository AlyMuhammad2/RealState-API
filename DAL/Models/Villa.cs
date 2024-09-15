using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Villa :Product
    {
        [Required]
        public int NumberOfFloors { get; set; } // عدد الطوابق

        public bool HasSwimmingPool { get; set; } // هل تحتوي على مسبح؟

        public bool HasGarden { get; set; } // ه

        public override string ToString()
        {
            return "Villa";
        }
    }
}
