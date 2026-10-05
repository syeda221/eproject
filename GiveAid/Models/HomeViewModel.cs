using System.Collections.Generic;

namespace GiveAid.Models
{
    public class HomeViewModel
    {
        public List<Cause> Causes { get; set; } = new List<Cause>();
        public List<Programme> Programmes { get; set; } = new List<Programme>();
        public List<Ngo> NGOs { get; set; } = new List<Ngo>();
        public List<Gallery> Galleries { get; set; } = new List<Gallery>();
    }

    public class DonationFormViewModel
    {
        public int CauseId { get; set; }
        public decimal Amount { get; set; }
        public string CardHolderName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string ExpiryDate { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public List<Cause> AvailableCauses { get; set; } = new List<Cause>();
    }

    public class QueryFormViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
