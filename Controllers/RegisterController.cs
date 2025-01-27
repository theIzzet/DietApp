using DietApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DietApp.Models;
using Microsoft.AspNetCore.Hosting;
namespace DietApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RoleManager<DietRole> _roleManager;
        private readonly UserManager<DietUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public RegisterController(UserManager<DietUser> userManager, IWebHostEnvironment webHostEnvironment, RoleManager<DietRole> roleManager)
        {
            _userManager=userManager;
            _roleManager=roleManager;
            _webHostEnvironment = webHostEnvironment;
        }



        public IActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user=new DietUser {UserName=model.Username,Name=model.Name, SurName=model.SurName, Email=model.Email, /*PasswordHash=model.Password*/ };


                IdentityResult result= await _userManager.CreateAsync(user,model.Password);

                if ( result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "Hasta");


                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "Rol atama sırasında bir hata oluştu.");
                        return View(model);
                    }
                    return RedirectToAction("Index", "Home");
                }

                foreach (IdentityError err in result.Errors)
                {

                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }

        public IActionResult DiyetisyenRegister()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DiyetisyenRegister(DiyetisyenRegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                string graduationSertificatePath = await SaveFileAsync(model.GraduationCertificate, "GraduationCertificates");
                string transkriptPath = await SaveFileAsync(model.Transkript, "Transkripts");

                if (string.IsNullOrEmpty(graduationSertificatePath) || string.IsNullOrEmpty(transkriptPath))
                {
                    ModelState.AddModelError(string.Empty, "Dosyaların yüklenmesi sırasında bir hata oluştu.");
                    return View(model);
                }


               


                var user = new DietUser { UserName = model.Username, Name = model.Name, SurName = model.SurName, Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    GraduationSertificatePath=graduationSertificatePath,
                  TranskriptPath= transkriptPath
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }

                //
                var roleResult = await _userManager.AddToRoleAsync(user, "Diyetisyen");
                if (!roleResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Rol atama sırasında bir hata oluştu.");
                    return View(model);
                }

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        private async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {


            // Dosya türünü kontrol ediyoruz
            if (file.ContentType != "application/pdf")
            {
                throw new InvalidOperationException("Sadece PDF dosyalarına izin verilmektedir.");
            }



            if (file != null && file.Length > 0)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", folderName);
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }


                //Diyetisyenlerin belge eklediğinde benzersiz olması için bu kodu ekledim
                string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";

                
                string filePath = Path.Combine(uploadDir, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Path.Combine("uploads", folderName, uniqueFileName).Replace("\\", "/"); // Veritabanına kaydedilecek yol
            }

            return string.Empty;
        }



    }
}
