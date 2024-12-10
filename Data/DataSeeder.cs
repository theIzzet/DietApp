using Microsoft.AspNetCore.Identity;

namespace DietApp.Data
{
    public static class DataSeeder
    {
        public static async Task SeedRoles(RoleManager<DietRole> roleManager)
        {
            string[] roles = { "Hasta", "Diyetisyen", "Admin" }; 
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new DietRole { Name = role });
                }
            }
        }
    }
}
