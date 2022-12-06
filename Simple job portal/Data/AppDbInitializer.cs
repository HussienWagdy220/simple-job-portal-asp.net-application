using Microsoft.AspNetCore.Identity;
using Simple_job_portal.Data.Static;
using Simple_job_portal.Models;

namespace Simple_job_portal.Data
{
    public class AppDbInitializer
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if(!await roleManager.RoleExistsAsync(UserRoles.Employer))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Employer));
                if(!await roleManager.RoleExistsAsync(UserRoles.Employee))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Employee));

                //Users

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string EmployerEmail = "hussienwagdy0@gmail.com";
                var EmployerUser = await userManager.FindByEmailAsync(EmployerEmail);

                if(EmployerUser == null)
                {
                    var newEmployerUser = new ApplicationUser()
                    {
                        FullName = "hussienwagdy0",
                        UserName = "hussienwagdy0-user",
                        Gender = "male",
                        Email = EmployerEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newEmployerUser,"Hu01151530823?");
                    await userManager.AddToRoleAsync(newEmployerUser, UserRoles.Employer);
                }

                string EmployeeEmail = "hussienwagdy100@gmail.com";
                var EmployeeUser = await userManager.FindByEmailAsync(EmployeeEmail);

                if(EmployeeUser == null)
                {
                    var newEmployeeUser = new ApplicationUser()
                    {
                        FullName = "hussienwagdy100",
                        UserName = "hussienwagdy100-user",
                        Gender = "male",
                        Email = EmployeeEmail,
                        EmailConfirmed = true                       
                    };
                    await userManager.CreateAsync(newEmployeeUser, "Hu01151530823?");
                    await userManager.AddToRoleAsync(newEmployeeUser, UserRoles.Employee);
                }
            }
        }
    }
}
