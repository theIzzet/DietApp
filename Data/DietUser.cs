using Microsoft.AspNetCore.Identity;

namespace DietApp.Data
{
    public class DietUser:IdentityUser
    {
        public string Name { get; set; } = string.Empty;

        public string SurName {  get; set; } = string.Empty;

        public string GraduationSertificatePath {  get; set; } = string.Empty;
        public string TranskriptPath { get; set;} = string.Empty;
    }
}
