using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.Entities
{
    public class Piatto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public Tipologia TipoPiatto { get; set; }
        public decimal Prezzo { get; set; }

        public int? IdMenu { get; set; }
        public Menu? Menu { get; set; }
    }

    public enum Tipologia
    {
        Primo = 1,
        Secondo = 2,
        Contorno = 3,
        Dolce = 4
    }
}
