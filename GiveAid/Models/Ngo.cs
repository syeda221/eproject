using System.ComponentModel.DataAnnotations;

namespace GiveAid.Models
{
    public class Ngo
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string NgoName { get; set; }
        
        public string Description { get; set; }
    }
}
