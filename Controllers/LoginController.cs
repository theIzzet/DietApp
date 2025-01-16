using DietApp.Data;
using DietApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DietApp.Controllers
{
    public class LoginController : Controller
    {

        private readonly RoleManager<DietRole> _roleManager;
        private readonly UserManager<DietUser> _userManager;
       
        private readonly SignInManager<DietUser> _signInManager;

        public LoginController(UserManager<DietUser> userManager,  RoleManager<DietRole> roleManager,SignInManager<DietUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            
        }

        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user=await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();

                    var result= await _signInManager.PasswordSignInAsync(user,model.Password,model.RememberMe,false);

                    if (result.Succeeded)
                    {
                        // Kullanıcı rollerini al
                        var roles = await _userManager.GetRolesAsync(user);


                        //await _userManager.ResetAccessFailedCountAsync(user);
                        //await _userManager.SetLockoutEndDateAsync(user,null);

                        if (roles.Contains("Hasta"))
                        {
                            // Hasta sayfasına yönlendir
                            return RedirectToAction("HastaPage", "Home", new { userId = user.Id });
                        }
                        else if (roles.Contains("Diyetisyen"))
                        {
                            // Diyetisyen sayfasına yönlendir
                            return RedirectToAction("DiyetisyenPage", "Home", new { userId = user.Id });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Parolanız hatalı. Lütfen tekrar deneyiniz");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "E-mail bilginiz hatalı. Lütfen tekrar deneyiniz");
                }


            }
            return View(model);
        }
    }
}
