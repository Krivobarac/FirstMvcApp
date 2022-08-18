using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace FirstMvcApp.Models
{
    public class CustomUser : IdentityUser
    {
        [DefaultValue("English")]
        public string? PreferredLanguage { get; set; }
    }
}
