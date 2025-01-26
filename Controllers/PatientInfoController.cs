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
        public async Task< IActionResult> PatientInfos(PatientInfoViewModel model)
        {
            if(ModelState.IsValid)
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
                    UserId=user.Id,
                    RegularPhysicalActivity=model.RegularPhysicalActivity,
                    DailyInactivity=model.DailyInactivity,
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
                    AllergyName=model.AllergyName,
                    DiseaseName=model.DiseaseNameD,
                    FamilyDiseaseName=model.DiseaseNameF,
                    MedicationName=model.MedicationName,
                    

                };
                _context.PastMedicals.Add(pastMedical);

                

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");


            }
            return View(model);
        }


    }
}
