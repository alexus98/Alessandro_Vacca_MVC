using Esercitazione.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.BusinessLayer
{
    public interface IBusinessLayer
    {
        public List<Menu> GetAllMenu();
        public Esito AggiungiMenu(Menu m);
        
        public List<Piatto> GetAllPiatti();
        public Esito AggiungiPiatto(Piatto m);
        public Esito ModificaPiatto(int id, string nome, string descrizione, Tipologia tipologia, decimal prezzo);
        public Esito EliminaPiatto(int id);

        public Esito AssociaPiattoAMenu(int idPiatto, int idMenu);
        public Esito DisassociaPiattoAMenu(int idPiatto, int idMenu);

        public Utente GetAccount(string username);
    }
}
