using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.Entities
{
    public class Utente
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Ruolo Ruolo { get; set; }
    }

    public enum Ruolo
    {
        Ristoratore = 1,
        Cliente = 2
    }
}
