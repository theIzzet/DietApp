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
        private readonly DataContext _dataContext;


        public HomeController(ILogger<HomeController> logger, UserManager<DietUser> userManager, DataContext dataContext)
        {
            _logger = logger;
            _userManager = userManager;
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
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

        // Hasta Page
        [Authorize(Roles = "Hasta")]
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

            ViewBag.UserName = $"{user.Name} {user.SurName}";
            return View();
        }

        [Authorize(Roles = "Hasta")]
        // HastaBilgilerim - GET
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
                KisiselBilgiler = await _dataContext.KisiselBilgiler.FirstOrDefaultAsync(k => k.KullaniciId == userId) ?? new KisiselBilgiler(),
                TibbiGecmis = await _dataContext.TibbiGecmis.FirstOrDefaultAsync(t => t.KullaniciId == userId) ?? new TibbiGecmis(),
                BeslenmeAliskanliklari = await _dataContext.BeslenmeAliskanliklari.FirstOrDefaultAsync(b => b.KullaniciId == userId) ?? new BeslenmeAliskanliklari(),
                FizikselAktiviteDurumu = await _dataContext.FizikselAktiviteDurumu.FirstOrDefaultAsync(f => f.KullaniciId == userId) ?? new FizikselAktiviteDurumu(),
                YasamTarzi = await _dataContext.YasamTarzi.FirstOrDefaultAsync(y => y.KullaniciId == userId) ?? new YasamTarzi(),
                Hedefler = await _dataContext.Hedefler.FirstOrDefaultAsync(h => h.KullaniciId == userId) ?? new Hedefler()
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
            model.KisiselBilgiler.KullaniciId = userId;
            model.TibbiGecmis.KullaniciId = userId;
            model.BeslenmeAliskanliklari.KullaniciId = userId;
            model.FizikselAktiviteDurumu.KullaniciId = userId;
            model.YasamTarzi.KullaniciId = userId;
            model.Hedefler.KullaniciId = userId;

            // Her tablo için güncelleme veya ekleme işlemi
            await UpsertRecord(_dataContext.KisiselBilgiler, model.KisiselBilgiler);
            await UpsertRecord(_dataContext.TibbiGecmis, model.TibbiGecmis);
            await UpsertRecord(_dataContext.BeslenmeAliskanliklari, model.BeslenmeAliskanliklari);
            await UpsertRecord(_dataContext.FizikselAktiviteDurumu, model.FizikselAktiviteDurumu);
            await UpsertRecord(_dataContext.YasamTarzi, model.YasamTarzi);
            await UpsertRecord(_dataContext.Hedefler, model.Hedefler);

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


        // Diyetisyen Page
        [Authorize(Roles = "Diyetisyen")]
        public async Task<IActionResult> DiyetisyenPage(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı ID'si eksik.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            ViewBag.UserName = $"{user.Name} {user.SurName}";
            return View();
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
