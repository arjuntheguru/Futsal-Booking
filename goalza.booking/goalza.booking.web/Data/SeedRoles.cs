using goalza.booking.web.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goalza.booking.web.Data
{
    public class SeedRoles
    {
        public static async Task Initialize(RoleManager<IdentityRole> roleManager,
             UserManager<ApplicationUser> userManager)
        {

            string[] Roles = { "SUPER_ADMIN", "ADMIN", "USER" };

            foreach (string roleName in Roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            if (await userManager.FindByNameAsync("superadmin") == null)
            {
                var user = new ApplicationUser()
                {
                    UserName = "superadmin",
                    Email = "developer@goalza.com",
                    PhoneNumber = "4125478",
                    Address = "Kathmandu",
                    CreatedBy = "superadmin"
                };

                var res = await userManager.CreateAsync(user, "Super@dmin1");
                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "SUPER_ADMIN");
                }
            }
        }
    }
}
