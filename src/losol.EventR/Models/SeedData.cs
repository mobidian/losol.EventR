using losol.EventR.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace losol.EventR.Models
{
    public class SeedData
    {
        private const string adminUser = "Admin@admin.com";
        private const string adminPassword = "Th1sPa$$word";
        private static readonly string[] Roles = new string[] { "Administrator", "Editor", "Member" };

        public static async Task EnsureRolesPopulated(IApplicationBuilder app)
        {
            RoleManager<IdentityRole> roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();

            foreach (var role in Roles)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                   await roleManager.CreateAsync(new IdentityRole { Name = role });
                }

            }

        }
        public static async Task EnsureAdminUserPopulated(IApplicationBuilder app)
        {
            //ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            RoleManager<IdentityRole> roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();
            UserManager<ApplicationUser> userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();

            if (!(await userManager.GetUsersInRoleAsync("Administrator")).Any())
            {
                var user = new ApplicationUser { UserName = adminUser, Email = adminUser };
                var result = await userManager.CreateAsync(user, adminPassword);
                if (result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }


     
        }
    }
}

