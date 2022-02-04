using Esercitazione.Core.BusinessLayer;
using Esercitazione.Core.Entities;
using Esercitazione.MVC.Helper;
using Esercitazione.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Esercitazione.MVC.Controllers
{
    [Authorize]
    public class PiattiController : Controller
    {
        private readonly IBusinessLayer BL;

        public PiattiController(IBusinessLayer bl)
        {
            BL = bl;
        }

        public IActionResult Index()
        {
            List<Piatto> piatto = BL.GetAllPiatti();
            List<PiattoViewModel> piattoViewModel = new List<PiattoViewModel>();

            foreach (var item in piatto)
            {
                piattoViewModel.Add(item.ToPiattoViewModel());
            }
            return View(piattoViewModel);
        }

        [Authorize(Policy = ("Ristoratore"))]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var piatto = BL.GetAllPiatti().FirstOrDefault(p => p.Id == id);
            var piattoViewModel = piatto.ToPiattoViewModel();

            LoadViewBag();
            ViewBag.Create = false;
            return View(piattoViewModel);
        }


        [Authorize(Policy = ("Ristoratore"))]
        [HttpPost]
        public IActionResult Edit(PiattoViewModel piattoViewModel)
        {
            if (ModelState.IsValid)
            {
                var piatto = piattoViewModel.ToPiatto();
                var esito = BL.ModificaPiatto(piatto.Id, piatto.Nome, piatto.Descrizione, piatto.TipoPiatto, piatto.Prezzo);
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
            return View(piattoViewModel);
        }

        [Authorize(Policy = ("Ristoratore"))]
        [HttpGet]
        public IActionResult Create()
        {
            LoadViewBag();
            ViewBag.Create = true;
            return View();
        }

        [Authorize(Policy = ("Ristoratore"))]
        [HttpPost]
        public IActionResult Create(PiattoViewModel piattoViewModel)
        {
            if (ModelState.IsValid)
            {
                var piatto = piattoViewModel.ToPiatto();
                var esito = BL.AggiungiPiatto(piatto);
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
            return View(piattoViewModel);
        }

        [Authorize(Policy = ("Ristoratore"))]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var piatto = BL.GetAllPiatti().FirstOrDefault(p => p.Id == id);
            var piattoViewModel = piatto.ToPiattoViewModel();
            return View(piattoViewModel);
        }

        [Authorize(Policy = ("Ristoratore"))]
        [HttpPost]
        public IActionResult Delete(PiattoViewModel piattoViewModel)
        {
            var piatto = piattoViewModel.ToPiatto();
            var esito = BL.EliminaPiatto(piatto.Id);
            if (esito.IsOk == true)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.MessaggioErrore = esito.Messaggio;
                return View("ErroriDiBusiness");
            }
        }

        



        [Authorize(Policy = ("Ristoratore"))]
        public IActionResult Disaccoppia()
        {
            return View();
        }

        private void LoadViewBag()
        {
            ViewBag.TipiPiatto = new SelectList(Enum.GetValues(typeof(Tipologia)).Cast<Tipologia>().Select(t => new SelectListItem
            {
                Text = t.ToString(),
                Value = ((int)t).ToString()
            }).ToList().OrderBy(t => t.Text), "Value", "Text");
        }
    }
}
