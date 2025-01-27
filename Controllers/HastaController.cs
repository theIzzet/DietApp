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
    }
}
