using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class tasks
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Agent")]
        public int AgentId { get; set; } // Foreign Key to Agent

        [Required]
        public string TaskName { get; set; }

        public string Description { get; set; }
        public string Status { get; set; } // e.g., New, In Progress, Completed
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletionDate { get; set; }

        // Navigation Property
        [InverseProperty("Tasks")]
        public Agent Agent { get; set; }
    }
}
