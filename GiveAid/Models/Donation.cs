using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiveAid.Models
{
    public class Donation
    {
        [Key]
        public int Id { get; set; }
        
        public decimal Amount { get; set; }

        // ASP.NET Identity User ID
        public string UserId { get; set; } 

        // Link to Cause
        public int CauseId { get; set; }
        [ForeignKey("CauseId")]
        public Cause Cause { get; set; }
    }
}
