using System.ComponentModel.DataAnnotations;

namespace GiveAid.Models
{
    public class Cause
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string CauseName { get; set; } // e.g., Children, Education
    }
}
