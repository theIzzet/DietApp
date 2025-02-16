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

namespace DietApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<DietUser> _userManager;
        private readonly IdentityContext _dataContext;


        public HomeController(ILogger<HomeController> logger, UserManager<DietUser> userManager, IdentityContext dataContext)
        {
            _logger = logger;
            _userManager = userManager;
            _dataContext = dataContext;
        }

        public async Task<IActionResult>  Index()
        {
            var dietTypes = await _dataContext.DietTypes
                .Include(dt => dt.DiyetisyenProfiles)
                .ThenInclude(dp => dp.User)
                .ToListAsync();
            return View(dietTypes);
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        public async Task<IActionResult> HastaPage()
        {
            // Oturumdaki kullanıcı ID'sini al
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
            var dietTypes = await _dataContext.DietTypes
                .Include(dt => dt.DiyetisyenProfiles)
                .ThenInclude(dp => dp.User)
                .ToListAsync();

            ViewBag.UserName = $"{user.Name} {user.SurName}";
            return View(dietTypes);
        }

        [Authorize(Roles = "Hasta")]
        
        [HttpGet]
        public async Task<IActionResult> HastaBilgilerim()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Kullanıcı oturum açmamış.");
            }

            // Kullanıcı bilgilerini getir (veya boş modeller oluştur)
            var model = new HastaBilgilerim
            {
                KisiselBilgiler = await _dataContext.PersonalInfos.FirstOrDefaultAsync(k => k.UserId == userId) ?? new PersonalInfo(),
                TibbiGecmis = await _dataContext.PastMedicals.FirstOrDefaultAsync(t => t.UserId == userId) ?? new PastMedical(),
                BeslenmeAliskanliklari = await _dataContext.EatingHabits.FirstOrDefaultAsync(b => b.UserId == userId) ?? new EatingHabit(),
                FizikselAktiviteDurumu = await _dataContext.PhysicalActivityStatus.FirstOrDefaultAsync(f => f.UserId == userId) ?? new PhysicalActivityStatus(),
                YasamTarzi = await _dataContext.Lifestyles.FirstOrDefaultAsync(y => y.UserId == userId) ?? new Lifestyle(),
                Hedefler = await _dataContext.Goals.FirstOrDefaultAsync(h => h.UserId == userId) ?? new Goal()
            };

            return View(model);
        }

        // HastaBilgilerim - POST
        [HttpPost]
        public async Task<IActionResult> HastaBilgilerim(HastaBilgilerim model)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Kullanıcı oturum açmamış.");
            }
            // Model doğruluğunu kontrol ediyoruz
            if (!ModelState.IsValid)
            {
                // Model geçerli değilse, aynı sayfayı model ile birlikte döndürüyoruz
                return View(model);
            }

            // Kullanıcı ID'sini ekle
            model.KisiselBilgiler.UserId = userId;
            model.TibbiGecmis.UserId = userId;
            model.BeslenmeAliskanliklari.UserId = userId;
            model.FizikselAktiviteDurumu.UserId = userId;
            model.YasamTarzi.UserId = userId;
            model.Hedefler.UserId = userId;

            // Her tablo için güncelleme veya ekleme işlemi
            await UpsertRecord(_dataContext.PersonalInfos, model.KisiselBilgiler);
            await UpsertRecord(_dataContext.PastMedicals, model.TibbiGecmis);
            await UpsertRecord(_dataContext.EatingHabits, model.BeslenmeAliskanliklari);
            await UpsertRecord(_dataContext.PhysicalActivityStatus, model.FizikselAktiviteDurumu);
            await UpsertRecord(_dataContext.Lifestyles, model.YasamTarzi);
            await UpsertRecord(_dataContext.Goals, model.Hedefler);

            await _dataContext.SaveChangesAsync();

            TempData["Message"] = "Bilgileriniz başarıyla kaydedildi!";
            return RedirectToAction("HastaBilgilerim");
        }

        private async Task UpsertRecord<T>(DbSet<T> dbSet, T record) where T : class
        {
            var entry = _dataContext.Entry(record);
            if (entry.State == EntityState.Detached)
            {
                var primaryKeyProperty = typeof(T).GetProperty("Id");
                if (primaryKeyProperty != null && (int)primaryKeyProperty.GetValue(record) == 0)
                {
                    dbSet.Add(record);
                }
                else
                {
                    dbSet.Update(record);
                }
            }
        }


        [Authorize(Roles = "Diyetisyen")]
        public async Task<IActionResult> DiyetisyenPage()
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

            // Kullanıcı bilgileri başarılı şekilde bulunduysa DiyetisyenController içindeki Profile metoduna yönlendir
            return RedirectToAction("Profile", "Diyetisyen");
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // Kullanıcı oturumunu kapat
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // Giriş sayfasına yönlendir
            return RedirectToAction("Index", "Home");
        }

    }
}
