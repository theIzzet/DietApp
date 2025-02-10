using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DietApp.Data;
using DietApp.Entities;
using DietApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace DietApp.Controllers
{
    // Controller'dan miras alıyoruz
    public class HastaController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<DietUser> _userManager;
        private readonly IdentityContext _dataContext;


        public HastaController(ILogger<HomeController> logger, UserManager<DietUser> userManager, IdentityContext dataContext)
        {
            _logger = logger;
            _userManager = userManager;
            _dataContext = dataContext;
        }

        public IActionResult Hizmetler()
        {
            // "Views/Hasta/Hizmetler.cshtml" arar
            return View("Hizmetler");
        }
        public IActionResult DoktorlarSayfa()
        {
            // "Views/Hasta/Hizmetler.cshtml" arar
            return View("DoktorlarSayfa");
        }
        [Authorize(Roles = "Hasta")]
        public async Task<IActionResult> DietList()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Kullanıcı oturum açmamış.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            // Fetching the diet list for the patient
            var patient = await _dataContext.PersonalInfos.FirstOrDefaultAsync(p => p.UserId == userId);
            if (patient == null)
            {
                return NotFound("Hasta bilgileri bulunamadı.");
            }

            var dietList = await _dataContext.DietLists
                                            .FirstOrDefaultAsync(d => d.PersonalInfoId == patient.Id);

           

            return View(dietList); // Pass the diet list to the view
        }

    }
}
