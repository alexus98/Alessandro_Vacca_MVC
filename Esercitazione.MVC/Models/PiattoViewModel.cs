using Esercitazione.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Esercitazione.MVC.Models
{
    public class PiattoViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Required]
        public Tipologia TipoPiatto { get; set; }

        [Required]
        public decimal Prezzo { get; set; }

        public int? IdMenu { get; set; }
        public MenuViewModel? Menu { get; set; }
    }
}
