using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiveAid.Models
{
    public class Programme
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string ProgrammeName { get; set; }

        // Link to NGO
        public int NgoId { get; set; }
        [ForeignKey("NgoId")]
        public Ngo Ngo { get; set; }
    }
}
