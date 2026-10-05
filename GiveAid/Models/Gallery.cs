using System.ComponentModel.DataAnnotations;

namespace GiveAid.Models
{
    public class Gallery
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; } // e.g., "Health Camp 2026"
        
        [Required]
        public string ImagePath { get; set; } // Path to the uploaded image
    }
}
