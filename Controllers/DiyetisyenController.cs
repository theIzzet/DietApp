using DietApp.Data;
using DietApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DietApp.Controllers
{
    [Authorize(Roles = "Diyetisyen")]
    public class DiyetisyenController : Controller
    {
        private readonly UserManager<DietUser> _userManager;
        private readonly IdentityContext _context;

        public DiyetisyenController(UserManager<DietUser> userManager, IdentityContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Profile()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var profile = await _context.DiyetisyenProfiles
                .Include(p => p.Certificates)
                .Include(p => p.Experiences)
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
            {
                profile = new DiyetisyenProfile { UserId = userId };
                _context.DiyetisyenProfiles.Add(profile);
                await _context.SaveChangesAsync();
            }

            return View(profile);
        }


        public async Task<IActionResult> EditProfile()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var profile = await _context.DiyetisyenProfiles
                .Include(p => p.Certificates)
                .Include(p => p.Experiences)
                .Include(p => p.DietTypes) 
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null) return NotFound();
            



            ViewBag.DietTypes = await _context.DietTypes.ToListAsync();

            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(string? about, IFormFile? profilePicture, int[] selectedDietTypes)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var profile = await _context.DiyetisyenProfiles.Include(p => p.DietTypes)
                .FirstOrDefaultAsync(p => p.UserId == userId)
                ;
            if (profile == null) return NotFound();

            if (!string.IsNullOrEmpty(about)) profile.About = about;

            if (profilePicture != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/profile_pics");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePicture.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePicture.CopyToAsync(stream);
                }
                profile.ProfilePicturePath = "/uploads/profile_pics/" + uniqueFileName;
            }




            profile.DietTypes.Clear();
            if (selectedDietTypes != null && selectedDietTypes.Length > 0)
            {
                var selectedDiets = await _context.DietTypes.Where(d => selectedDietTypes.Contains(d.Id)).ToListAsync();
                foreach (var diet in selectedDiets)
                {
                    profile.DietTypes.Add(diet);
                }
            }

            profile.DietTypes.Clear();
            if (selectedDietTypes != null && selectedDietTypes.Length > 0)
            {
                var selectedDiets = await _context.DietTypes.Where(d => selectedDietTypes.Contains(d.Id)).ToListAsync();
                foreach (var diet in selectedDiets)
                {
                    profile.DietTypes.Add(diet);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> AddExperience(string description)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(description)) return BadRequest();

            var profile = await _context.DiyetisyenProfiles.Include(p => p.Experiences).FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null) return NotFound();

            profile.Experiences.Add(new Experience { Description = description, UserId = userId });
            await _context.SaveChangesAsync();
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            if (experience == null) return NotFound();

            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> UploadCertificates(List<IFormFile> certificates)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var profile = await _context.DiyetisyenProfiles.Include(p => p.Certificates).FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null) return NotFound();

            var certificatesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/certificates");
            if (!Directory.Exists(certificatesFolder)) Directory.CreateDirectory(certificatesFolder);

            foreach (var cert in certificates)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(cert.FileName);
                var certPath = Path.Combine(certificatesFolder, uniqueFileName);
                using (var stream = new FileStream(certPath, FileMode.Create))
                {
                    await cert.CopyToAsync(stream);
                }
                profile.Certificates.Add(new Certificate { FilePath = "/uploads/certificates/" + uniqueFileName, UserId = userId });
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            var certificate = await _context.Certificates.FindAsync(id);
            if (certificate == null) return NotFound();

            _context.Certificates.Remove(certificate);
            await _context.SaveChangesAsync();
            return RedirectToAction("Profile");
        }


        public async Task<IActionResult> Patients()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var profile = await _context.DiyetisyenProfiles
                .Include(p => p.Hastalar)
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null) return NotFound();

            return View(profile.Hastalar);
        }

        public async Task<IActionResult> CreateDietList(int patientId)
        {
            var patient = await _context.PersonalInfos.FindAsync(patientId);
            if (patient == null) return NotFound();

            return View(new DietList { PersonalInfoId = patientId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateDietList(DietList dietList)
        {
            if (dietList == null || string.IsNullOrEmpty(dietList.Description))
            {
                return BadRequest("Diyet listesi boş olamaz.");
            }

            var patient = await _context.PersonalInfos.FindAsync(dietList.PersonalInfoId);
            if (patient == null) return NotFound("Hasta bulunamadı.");

            dietList.CreatedAt = DateTime.Now; // Tarih bilgisi eklendi
            _context.DietLists.Add(dietList);
            await _context.SaveChangesAsync();

            return RedirectToAction("Patients");

        }


        public async Task<IActionResult> ViewDietList(int patientId)
        {
            var patient = await _context.PersonalInfos
                .Include(p => p.Diyetisyen)
                .FirstOrDefaultAsync(p => p.Id == patientId);

            if (patient == null) return NotFound("Hasta bulunamadı.");

            var dietLists = await _context.DietLists
                .Where(d => d.PersonalInfoId == patientId)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();

            ViewBag.PatientName = patient.Name + " " + patient.SurName; // Hasta adını ViewBag ile taşıyoruz

            return View(dietLists);
        }



        [HttpPost]
        public async Task<IActionResult> AcceptPatient(int patientId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            await using var transaction = await _context.Database.BeginTransactionAsync(); // 🔹 Transaction başlat

            try
            {
                var diyetisyen = await _context.DiyetisyenProfiles
                    .FirstOrDefaultAsync(d => d.UserId == userId);

                if (diyetisyen == null) return NotFound("Diyetisyen bulunamadı.");

                var patient = await _context.PersonalInfos.FirstOrDefaultAsync(p => p.Id == patientId);
                if (patient == null) return NotFound("Hasta bulunamadı.");

                patient.DiyetisyenId = diyetisyen.Id;
                await _context.SaveChangesAsync();

                await transaction.CommitAsync(); // 🔹 İşlemi tamamla
                return RedirectToAction("Patients");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // 🔹 Hata olursa işlemi geri al
                Console.WriteLine("Hata: " + ex.Message);
                return BadRequest("Veritabanı hatası.");
            }
        }




    }
}
