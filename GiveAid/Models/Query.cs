using System.ComponentModel.DataAnnotations;

namespace GiveAid.Models
{
    public class Query
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Message { get; set; }

        // ASP.NET Identity User ID
        public string UserId { get; set; } 
    }
}
