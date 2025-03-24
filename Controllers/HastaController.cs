using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DietApp.Data;
using DietApp.Entities;
using DietApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;

using DietApp.Models;

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

       

        public async Task<IActionResult> Hizmetler(int? id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {

                var roles = await _userManager.GetRolesAsync(user);

                
                if (roles.Contains("Hasta"))
                {
                    ViewBag.Layout = "_HastaLayout";
                }
                else
                {
                    ViewBag.Layout = "_Layout";
                }
            }
            else
            {
                ViewBag.Layout = "_Layout";
            }
            if (id == null)
            {
                
                var allDoktorlar = await _dataContext.DiyetisyenProfiles
                    .Include(d => d.User)
                    .ToListAsync();

                if (allDoktorlar == null || !allDoktorlar.Any())
                {
                    ViewBag.Message = "Henüz listelenecek diyetisyen yok.";
                }

                return View(allDoktorlar);
            }

         
            var dietType = await _dataContext.DietTypes
                .Include(dt => dt.DiyetisyenProfiles)
                .ThenInclude(dp => dp.User)
                .FirstOrDefaultAsync(dt => dt.Id == id);

            if (dietType == null)
            {
                ViewBag.Message = "Belirtilen diyet türü bulunamadı.";
                return View(new List<DiyetisyenProfile>());
            }

           
            ViewBag.DietTypeTitle = dietType.Title;
            ViewBag.DietTypeDescription = dietType.Description;

            return View(dietType.DiyetisyenProfiles);
        }

        public async Task<IActionResult> DoktorlarSayfa(string id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                // Kullanıcının rollerini al
                var roles = await _userManager.GetRolesAsync(user);

                // Eğer kullanıcının rolü "Hasta" ise, özel layout'u kullanmasını sağla
                if (roles.Contains("Hasta"))
                {
                    ViewBag.Layout = "_HastaLayout";
                }
                else
                {
                    ViewBag.Layout = "_Layout";
                }
            }
            else
            {
                ViewBag.Layout = "_Layout";
            }
            if (string.IsNullOrEmpty(id))
            {
                return NotFound("Doktor bulunamadı.");
            }

            var doktor = await _dataContext.DiyetisyenProfiles
                                           .Include(d => d.User)
                                           .Include(d => d.Comments)
                                           .ThenInclude(d =>d.User)
                                           .FirstOrDefaultAsync(d => d.UserId == id);

            if (doktor == null)
            {
                return NotFound("Doktor profili bulunamadı.");
            }

            return View(doktor);
        }

        


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized();
                }

                var diyetisyen = await _dataContext.DiyetisyenProfiles.FindAsync(model.DietisyenProfileId);
                if (diyetisyen == null)
                {
                    return NotFound("Böyle bir diyetisyen profili bulunamadı.");
                }

                var comment = new Comment
                {
                    CommentText = model.CommentText,
                    PublishedOn = DateTime.Now,
                    UserId = user.Id,
                    DPId = model.DietisyenProfileId,
                    Rating = model.Rating
                };

                _dataContext.Comments.Add(comment);
                await _dataContext.SaveChangesAsync();

                // Doğru ID'yi kullanarak yönlendirme yap
                return RedirectToAction("DoktorlarSayfa", new { id = diyetisyen.UserId });
            }

            return BadRequest("Geçersiz veri");
        }
       
        [Authorize(Roles = "Hasta")]
        public async Task<IActionResult> DietList()
        {
            // Şu anki oturum açan kullanıcıyı al
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                // Kullanıcı yoksa ana sayfaya yönlendirilebilir
                return RedirectToAction("Index", "Home");
            }

            // Kişisel bilgi kaydını çek (kişiye ait PersonalInfo)
            // Örnek: eğer PersonalInfo içinde 'DietUserId' gibi bir alan varsa
            var personalInfo = await _dataContext.PersonalInfos
                .FirstOrDefaultAsync(p => p.UserId == currentUser.Id);

            if (personalInfo == null)
            {
                return NotFound("Kullanıcıya ait kişisel bilgi bulunamadı.");
            }

            // Hastaya ait diyet listesini çek (en son ekleneni alıyoruz)
            var dietList = await _dataContext.DietLists
                .Where(d => d.PersonalInfoId == personalInfo.Id)
                .OrderByDescending(d => d.CreatedAt)
                .FirstOrDefaultAsync();

            if (dietList == null)
            {
                return NotFound("Diyet listesi bulunamadı.");
            }

            // dietList.Description içindeki JSON'u çözümlüyoruz
            // Mesela "Pazartesi" -> [{ type: "...", meal: "..." }, ... ]
            // şeklinde bir sözlük (Dictionary) elde edeceğiz
            Dictionary<string, List<MealVM>> model =
                          JsonConvert.DeserializeObject<Dictionary<string, List<MealVM>>>(dietList.Description);
            if (!string.IsNullOrWhiteSpace(dietList.Description))
            {
                model = JsonConvert.DeserializeObject<Dictionary<string, List<MealVM>>>(dietList.Description);
            }

            // View'a JSON'dan elde ettiğimiz Dictionary'i gönderiyoruz
            return View(model);
        }




        // Diğer aksiyonlarınızın altında ekleyebilirsiniz.
        [Authorize(Roles = "Hasta")]
        public IActionResult Measurements()
        {
            return View();
        }


        // Kilo Ölçüm formunu göster
        public IActionResult WeightMeasurement()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> WeightMeasurement(WeightMeasurementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = _userManager.GetUserId(User);
            var personalInfo = await _dataContext.PersonalInfos.FirstOrDefaultAsync(p => p.UserId == userId);
            if (personalInfo == null)
            {
                return NotFound("Hasta bilgileri bulunamadı.");
            }

            var weightMeasurement = new WeightMeasurement
            {
                PersonalInfoId = personalInfo.Id,
                MeasurementDate = DateTime.Now,
                Weight = model.Weight,
                PhotoPath = SaveUploadedFile(model.Photo)
            };

            _dataContext.WeightMeasurements.Add(weightMeasurement);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("MeasurementHistory");
        }
        // Vücut Ölçüm formunu göster
        public IActionResult BodyMeasurement()
        {
            return View();
        }
         
        [HttpPost]
        public async Task<IActionResult> BodyMeasurement(BodyMeasurementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = _userManager.GetUserId(User);
            var personalInfo = await _dataContext.PersonalInfos.FirstOrDefaultAsync(p => p.UserId == userId);
            if (personalInfo == null)
            {
                return NotFound("Hasta bilgileri bulunamadı.");
            }

            var bodyMeasurement = new BodyMeasurement
            {
                PersonalInfoId = personalInfo.Id,
                MeasurementDate = DateTime.Now,
                Waist = model.Waist,
                Hips = model.Hips,
                Chest = model.Chest,
                UpperArm = model.UpperArm,
                Leg = model.Leg,
                Neck = model.Neck,
                PhotoPath = SaveUploadedFile(model.Photo)
            };

            _dataContext.BodyMeasurements.Add(bodyMeasurement);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("MeasurementHistory");
        }
        // Kayıtlı kilo ve vücut ölçümlerini gösteren sayfa
        public async Task<IActionResult> MeasurementHistory()
        {
            var userId = _userManager.GetUserId(User);
            var personalInfo = await _dataContext.PersonalInfos.FirstOrDefaultAsync(p => p.UserId == userId);
            if (personalInfo == null)
            {
                return NotFound("Hasta bilgileri bulunamadı.");
            }

            var weightMeasurements = await _dataContext.WeightMeasurements
                .Where(w => w.PersonalInfoId == personalInfo.Id)
                .OrderBy(w => w.MeasurementDate)
                .ToListAsync();

            var bodyMeasurements = await _dataContext.BodyMeasurements
                .Where(b => b.PersonalInfoId == personalInfo.Id)
                .OrderBy(b => b.MeasurementDate)
                .ToListAsync();

            var model = new MeasurementHistoryViewModel
            {
                WeightMeasurements = weightMeasurements,
                BodyMeasurements = bodyMeasurements
            };

            return View(model);
        }

        // “Ölçüm Yapılamadı” formunu göster (measurementType: "Weight" veya "Body")
        public IActionResult ReportMeasurementIssue(string measurementType)
        {
            var model = new MeasurementIssueViewModel
            {
                MeasurementType = measurementType
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ReportMeasurementIssue(MeasurementIssueViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = _userManager.GetUserId(User);
            var personalInfo = await _dataContext.PersonalInfos.FirstOrDefaultAsync(p => p.UserId == userId);
            if (personalInfo == null)
            {
                return NotFound("Hasta bilgileri bulunamadı.");
            }

            var issueReport = new MeasurementIssueReport
            {
                PersonalInfoId = personalInfo.Id,
                ReportDate = DateTime.Now,
                MeasurementType = model.MeasurementType,
                Reason = model.Reason,
                ApprovedByDietitian = false
            };

            _dataContext.MeasurementIssueReports.Add(issueReport);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("MeasurementHistory");
        }
        // Yüklenen dosyayı wwwroot/uploads klasörüne kaydeder ve dosya yolunu döner.
        private string? SaveUploadedFile(IFormFile? file)
        {
            if (file != null && file.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return "/uploads/" + uniqueFileName;
            }
            return null;
        } 

    }
}
