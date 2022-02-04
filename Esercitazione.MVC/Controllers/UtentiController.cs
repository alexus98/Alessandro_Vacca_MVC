using Esercitazione.Core.BusinessLayer;
using Esercitazione.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Esercitazione.MVC.Controllers
{
    public class UtentiController : Controller
    {
        private readonly IBusinessLayer BL;

        public UtentiController(IBusinessLayer bl)
        {
            BL = bl;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new UtenteViewModel { ReturnUrl = returnUrl });
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(UtenteViewModel utenteViewModel)
        {
            if (utenteViewModel == null)
            {
                return View();
            }

            var utente = BL.GetAccount(utenteViewModel.Username);
            if (utente != null && ModelState.IsValid)
            {
                if (utente.Password == utenteViewModel.Password)
                {
                    //l'utente è autenticato"
                    var claim = new List<Claim>{

                        new Claim (ClaimTypes.Name, utente.Username),
                        new Claim (ClaimTypes.Role, utente.Ruolo.ToString())
                    };

                    var properties = new AuthenticationProperties
                    {
                        RedirectUri = utenteViewModel.ReturnUrl,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                    };

                    var claimIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimIdentity),
                        properties
                        );
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError(nameof(utenteViewModel.Password), "Password errata");
                    return View(utenteViewModel);
                }
            }
            else
            {
                return View(utenteViewModel);
            }

            return View();
        }

        [AllowAnonymous]
        public IActionResult Forbidden() => View();

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
