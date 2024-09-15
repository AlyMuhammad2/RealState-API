using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class House :Product
    {
        [Required]
        public int NumberOfRooms { get; set; } // عدد الغرف

        public bool HasGarage { get; set; } // هل يحتوي على جراج؟

        public bool HasBackyard { get; set; }

        public override string ToString()
        {
            return "House";
        }
    }
}
