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

           

            return View(dietList); 
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
