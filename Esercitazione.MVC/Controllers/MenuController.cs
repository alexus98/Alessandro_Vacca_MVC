using Esercitazione.Core.BusinessLayer;
using Esercitazione.Core.Entities;
using Esercitazione.MVC.Helper;
using Esercitazione.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Esercitazione.MVC.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {

        private readonly IBusinessLayer BL;

        public MenuController(IBusinessLayer bl)
        {
            BL = bl;
        }

        public IActionResult Index()
        {
            List<Menu> menu = BL.GetAllMenu();
            List<MenuViewModel> menuViewModel = new List<MenuViewModel>();

            foreach (var item in menu)
            {
                menuViewModel.Add(item.ToMenuViewModel());
            }
            return View(menuViewModel);
        }

        public IActionResult Details(int id)
        {
            var menu = BL.GetAllMenu().FirstOrDefault(m => m.Id == id);
            decimal total = menu.Piatti.Sum(p => p.Prezzo);
            var menuViewModel = menu.ToMenuViewModel();

            ViewBag.Totale = total;

            return View(menuViewModel);
        }


        [Authorize(Policy = ("Ristoratore"))]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = ("Ristoratore"))]
        [HttpPost]
        public IActionResult Create(MenuViewModel menuViewModel)
        {
            if (ModelState.IsValid)
            {
                var menu = menuViewModel.ToMenu();
                var esito = BL.AggiungiMenu(menu);
                if (esito.IsOk == true)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.MessaggioErrore = esito.Messaggio;
                    return View("ErroriBL");
                }
            }
            return View(menuViewModel);
        }
    }
}