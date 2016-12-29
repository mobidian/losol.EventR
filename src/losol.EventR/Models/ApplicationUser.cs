using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace losol.EventR.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class

    // TODO Add image tutorial https://social.technet.microsoft.com/wiki/contents/articles/34445.mvc-asp-net-identity-customizing-for-adding-profile-image.aspx
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Navn")]
        public string FullName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Kort om deg selv")]
        public string Bio { get; set; }
        
        [Display(Name = "Yrke")]
        public string Occupation { get; set; }

        [Display(Name = "Arbeidssted")]
        public string Employer { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fødselsdato")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Adresse")]
        public string Address { get; set; }

        [Display(Name = "Adresse 2")]
        public string Address2 { get; set; }

        [Display(Name = "Postnummer")]
        public string PostalCode { get; set; }

        [Display(Name = "Sted")]
        public string City { get; set; }

        [Display(Name = "Land")]
        public string Country { get; set; }

        [Display(Name = "Stat")]
        public string State { get; set; }

        [Display(Name = "Profilbilde")]
        public byte[] UserPhoto { get; set; }
    }
}
