using DietApp.Data;
using DietApp.Entities;
using DietApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DietApp.Controllers
{
    public class PatientInfoController : Controller
    {


        private readonly RoleManager<DietRole> _roleManager;
        private readonly UserManager<DietUser> _userManager;

        private readonly SignInManager<DietUser> _signInManager;

        private readonly IdentityContext _context;


        public PatientInfoController(UserManager<DietUser> userManager, RoleManager<DietRole> roleManager, SignInManager<DietUser> signInManager, IdentityContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;

            _context = context;

        }

        [HttpGet]
        public IActionResult PatientInfos()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PatientInfos(PatientInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                var personalInfo = new PersonalInfo
                {
                    UserId = user.Id,
                    Name = model.Name,
                    SurName = model.SurName,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Height = model.Height,
                    Weight = model.Weight,
                    ContactInformation = model.ContactInformation,
                    Occupation = model.Occupation,
                    MaritalStatus = model.MaritalStatus,
                    NumberOfChildren = model.NumberOfChildren
                };
                _context.PersonalInfos.Add(personalInfo);

                // EatingHabit oluştur ve kaydet
                var eatingHabit = new EatingHabit
                {
                    UserId = user.Id,

                    MealTimes = model.MealTimes,
                    ConsumedFoods = model.ConsumedFoods,
                    SnackingHabits = model.SnackingHabits,
                    WaterConsumption = model.WaterConsumption,

                    CookingMethod = model.CookingMethod,
                    EatingDuration = model.EatingDuration,
                    EatingOutHabits = model.EatingOutHabits,
                    DessertConsumption = model.DessertConsumption
                };
                _context.EatingHabits.Add(eatingHabit);


                var physicalActivityStatus = new PhysicalActivityStatus
                {
                    UserId = user.Id,
                    RegularPhysicalActivity = model.RegularPhysicalActivity,
                    DailyInactivity = model.DailyInactivity,
                    SleepPattern = model.SleepPattern,

                };
                _context.PhysicalActivityStatus.Add(physicalActivityStatus);

                var lifeStyle = new Lifestyle
                {
                    UserId = user.Id,
                    StressLevel = model.StressLevel,
                    NumberOfSmokingPackage = model.NumberOfSmokingPackage,
                    SmokingUtilezeYear = model.SmokingUtilezeYear,
                    AlcoholConsumption = model.AlcoholConsumption,
                    CaffeineIntake = model.CaffeineIntake,
                    MotivationLevel = model.MotivationLevel,
                    SocialSupport = model.SocialSupport
                };

                _context.Lifestyles.Add(lifeStyle);


                var goal = new Goal
                {
                    UserId = user.Id,
                    WeightGoal = model.WeightGoal,
                    HealthIssuesManagement = model.HealthIssuesManagement,
                    SportsPerformanceGoals = model.SportsPerformanceGoals,
                    OtherGoals = model.OtherGoals
                };
                _context.Goals.Add(goal);




                var pastMedical = new PastMedical
                {
                    UserId = user.Id,
                    AllergyName = model.AllergyName,
                    DiseaseName = model.DiseaseName,
                    FamilyDiseaseName = model.FamilyDiseaseName,
                    MedicationName = model.MedicationName,


                };
                _context.PastMedicals.Add(pastMedical);



                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");


            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> PatientInfoDetails()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var model = new PatientInfoViewModel();

            // Personal Info
            var personalInfo = await _context.PersonalInfos
                .FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (personalInfo != null)
            {
                model.Name = personalInfo.Name;
                model.SurName = personalInfo.SurName;
                model.DateOfBirth = personalInfo.DateOfBirth;
                model.Gender = personalInfo.Gender;
                model.Height = personalInfo.Height;
                model.Weight = personalInfo.Weight;
                model.ContactInformation = personalInfo.ContactInformation;
                model.Occupation = personalInfo.Occupation;
                model.MaritalStatus = personalInfo.MaritalStatus;
                model.NumberOfChildren = personalInfo.NumberOfChildren;
            }

            // Eating Habits
            var eatingHabit = await _context.EatingHabits
                .FirstOrDefaultAsync(e => e.UserId == user.Id);
            if (eatingHabit != null)
            {
                model.MealTimes = eatingHabit.MealTimes;
                model.ConsumedFoods = eatingHabit.ConsumedFoods;
                model.SnackingHabits = eatingHabit.SnackingHabits;
                model.WaterConsumption = eatingHabit.WaterConsumption;
                model.CookingMethod = eatingHabit.CookingMethod;
                model.EatingDuration = eatingHabit.EatingDuration;
                model.EatingOutHabits = eatingHabit.EatingOutHabits;
                model.DessertConsumption = eatingHabit.DessertConsumption;
            }

            // Physical Activity
            var physicalActivity = await _context.PhysicalActivityStatus
                .FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (physicalActivity != null)
            {
                model.RegularPhysicalActivity = physicalActivity.RegularPhysicalActivity;
                model.DailyInactivity = physicalActivity.DailyInactivity;
                model.SleepPattern = physicalActivity.SleepPattern;
            }

            // Lifestyle
            var lifestyle = await _context.Lifestyles
                .FirstOrDefaultAsync(l => l.UserId == user.Id);
            if (lifestyle != null)
            {
                model.StressLevel = lifestyle.StressLevel;
                model.NumberOfSmokingPackage = lifestyle.NumberOfSmokingPackage;
                model.SmokingUtilezeYear = lifestyle.SmokingUtilezeYear;
                model.AlcoholConsumption = lifestyle.AlcoholConsumption;
                model.CaffeineIntake = lifestyle.CaffeineIntake;
                model.MotivationLevel = lifestyle.MotivationLevel;
                model.SocialSupport = lifestyle.SocialSupport;
            }

            // Goals
            var goal = await _context.Goals
                .FirstOrDefaultAsync(g => g.UserId == user.Id);
            if (goal != null)
            {
                model.WeightGoal = goal.WeightGoal;
                model.HealthIssuesManagement = goal.HealthIssuesManagement;
                model.SportsPerformanceGoals = goal.SportsPerformanceGoals;
                model.OtherGoals = goal.OtherGoals;
            }

            // Medical History
            var pastMedical = await _context.PastMedicals
                .FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (pastMedical != null)
            {
                model.AllergyName = pastMedical.AllergyName;
                model.DiseaseName = pastMedical.DiseaseName;
                model.FamilyDiseaseName = pastMedical.FamilyDiseaseName;
                model.MedicationName = pastMedical.MedicationName;
            }

            return View(model);
        }

        [Route("PatientInfo/[action]")]

        [HttpPost]
        public async Task<IActionResult> UpdatePersonalInfo([FromBody] PersonalInfo model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            var personalInfo = await _context.PersonalInfos.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (personalInfo == null)
            {
                return Json(new { success = false, message = "Personal information not found." });
            }

            // Update fields
            personalInfo.Name = model.Name;
            personalInfo.SurName = model.SurName;
            personalInfo.DateOfBirth = model.DateOfBirth;
            personalInfo.Gender = model.Gender;
            personalInfo.Height = model.Height;
            personalInfo.Weight = model.Weight;
            personalInfo.ContactInformation = model.ContactInformation;
            personalInfo.Occupation = model.Occupation;
            personalInfo.MaritalStatus = model.MaritalStatus;
            personalInfo.NumberOfChildren = model.NumberOfChildren;

            try
            {
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Personal information updated successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error updating personal information: " + ex.Message });
            }
        }



        [Route("PatientInfo/[action]")]
        [HttpPost]
        public async Task<IActionResult> UpdatePhysicalActivity([FromBody] PhysicalActivityStatus model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            var physicalActivity = await _context.PhysicalActivityStatus.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (physicalActivity == null)
            {
                // If no record exists, create a new one
                physicalActivity = new PhysicalActivityStatus
                {
                    UserId = user.Id
                };
                _context.PhysicalActivityStatus.Add(physicalActivity);
            }

            // Update fields
            physicalActivity.RegularPhysicalActivity = model.RegularPhysicalActivity;
            physicalActivity.DailyInactivity = model.DailyInactivity;
            physicalActivity.SleepPattern = model.SleepPattern;

            try
            {
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Fiziksel aktivite bilgileri başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Fiziksel aktivite bilgilerini güncellerken hata oluştu: " + ex.Message });
            }
        }




        [Route("PatientInfo/[action]")]
        [HttpPost]
        public async Task<IActionResult> UpdateLifestyle([FromBody] Lifestyle model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            var lifestyle = await _context.Lifestyles.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (lifestyle == null)
            {
                // If no record exists, create a new one
                lifestyle = new Lifestyle
                {
                    UserId = user.Id
                };
                _context.Lifestyles.Add(lifestyle);
            }

            // Update fields
            lifestyle.StressLevel = model.StressLevel;
            lifestyle.NumberOfSmokingPackage = model.NumberOfSmokingPackage;
            lifestyle.SmokingUtilezeYear = model.SmokingUtilezeYear;
            lifestyle.AlcoholConsumption = model.AlcoholConsumption;
            lifestyle.CaffeineIntake = model.CaffeineIntake;
            lifestyle.MotivationLevel = model.MotivationLevel;
            lifestyle.SocialSupport = model.SocialSupport;

            try
            {
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Yaşam tarzı bilgileri başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Yaşam tarzı bilgilerini güncellerken hata oluştu: " + ex.Message });
            }
        }

        [Route("PatientInfo/[action]")]
        [HttpPost]
        public async Task<IActionResult> UpdateEatingHabits([FromBody] EatingHabit model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            var eatingHabits = await _context.EatingHabits.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (eatingHabits == null)
            {
                // If no record exists, create a new one
                eatingHabits = new EatingHabit
                {
                    UserId = user.Id
                };
                _context.EatingHabits.Add(eatingHabits);
            }

            // Update fields
            eatingHabits.MealTimes = model.MealTimes;
            eatingHabits.ConsumedFoods = model.ConsumedFoods;
            eatingHabits.SnackingHabits = model.SnackingHabits;
            eatingHabits.EatingOutHabits = model.EatingOutHabits;
            eatingHabits.EatingDuration = model.EatingDuration;
            eatingHabits.DessertConsumption = model.DessertConsumption;
            eatingHabits.CookingMethod = model.CookingMethod;
            eatingHabits.WaterConsumption = model.WaterConsumption;

            try
            {
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Yeme alışkanlıkları başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Yeme alışkanlıklarını güncellerken hata oluştu: " + ex.Message });
            }
        }

        [Route("PatientInfo/[action]")]
        [HttpPost]
        public async Task<IActionResult> UpdateGoals([FromBody] Goal model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            var goals = await _context.Goals.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (goals == null)
            {
                // If no record exists, create a new one
                goals = new Goal
                {
                    UserId = user.Id
                };
                _context.Goals.Add(goals);
            }

            // Update fields
            goals.WeightGoal = model.WeightGoal;
            goals.HealthIssuesManagement = model.HealthIssuesManagement;
            goals.SportsPerformanceGoals = model.SportsPerformanceGoals;
            goals.OtherGoals = model.OtherGoals;

            try
            {
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Hedefleriniz başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Hedeflerinizi güncellerken hata oluştu: " + ex.Message });
            }
        }

        [Route("PatientInfo/[action]")]
        [HttpPost]
        public async Task<IActionResult> UpdateMedicalHistory([FromBody] PastMedical model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            var medicalHistory = await _context.PastMedicals.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (medicalHistory == null)
            {
                // If no record exists, create a new one
                medicalHistory = new PastMedical
                {
                    UserId = user.Id
                };
                _context.PastMedicals.Add(medicalHistory);
            }

            // Update fields
            medicalHistory.AllergyName = model.AllergyName;
            medicalHistory.DiseaseName = model.DiseaseName;
            medicalHistory.FamilyDiseaseName = model.FamilyDiseaseName;
            medicalHistory.MedicationName = model.MedicationName;

            try
            {
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Tıbbi geçmiş bilgileri başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Tıbbi geçmiş bilgilerini güncellerken hata oluştu: " + ex.Message });
            }
        }


    }
}
