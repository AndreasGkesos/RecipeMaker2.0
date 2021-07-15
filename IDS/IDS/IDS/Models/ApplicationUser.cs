using Microsoft.AspNetCore.Identity;

namespace IDS.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Extra1 { get; set; }
        public string Extra2 { get; set; }
    }
}
