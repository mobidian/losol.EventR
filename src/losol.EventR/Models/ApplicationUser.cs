using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace losol.EventR.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class

    // TODO Add image tutorial https://social.technet.microsoft.com/wiki/contents/articles/34445.mvc-asp-net-identity-customizing-for-adding-profile-image.aspx
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
