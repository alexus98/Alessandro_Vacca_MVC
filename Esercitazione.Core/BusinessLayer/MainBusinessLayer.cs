using Esercitazione.Core.Entities;
using Esercitazione.Core.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.BusinessLayer
{
    public class MainBusinessLayer : IBusinessLayer
    {

        private readonly IMenuRepository menuRepo;
        private readonly IPiattoRepository piattiRepo;
        private readonly IUtenteRepository utentiRepo;

        public MainBusinessLayer(IMenuRepository menu, IPiattoRepository piatti, IUtenteRepository utenti)
        {
            menuRepo = menu;
            piattiRepo = piatti;
            utentiRepo = utenti;
        }

        #region Piatto

        public Esito AggiungiPiatto(Piatto p)
        {
            Piatto piatto = piattiRepo.GetById(p.Id);
            if (piatto == null)
            {
                piattiRepo.Add(p);
                return new Esito { Messaggio = $"Piatto aggiunto correttamente", IsOk = true };
            }
            return new Esito { Messaggio = $"Impossibile aggiungere il piatto", IsOk = false };
        }

        public Esito ModificaPiatto(int id, string nome, string descrizione, Tipologia tipologia, decimal prezzo)
        {
            var piatto = piattiRepo.GetById(id);
            if (piatto == null)
            {
                return new Esito { Messaggio = "Nessun piatto trovato", IsOk = false };
            }

            piatto.Nome = nome;
            piatto.Descrizione = descrizione;
            piatto.TipoPiatto = tipologia;
            piatto.Prezzo = prezzo;
            piattiRepo.Update(piatto);

            return new Esito { Messaggio = "Piatto aggiornato correttamente", IsOk = true };
        }

        public Esito EliminaPiatto(int id)
        {
            var piatto = piattiRepo.GetById(id);

            if (piatto == null)
            {
                return new Esito { Messaggio = "Nessun piatto trovato", IsOk = false };
            }

            piattiRepo.Delete(piatto);
            return new Esito { Messaggio = "Piatto eliminato correttamente", IsOk = true };
        }

        public List<Piatto> GetAllPiatti()
        {
            return piattiRepo.GetAll();
        }

        #endregion

        #region Menu

        public Esito AggiungiMenu(Menu m)
        {
            Menu menu = menuRepo.GetById(m.Id);
            if (menu == null)
            {
                menuRepo.Add(m);
                return new Esito { Messaggio = $"Menu aggiunto correttamente", IsOk = true };
            }
            return new Esito { Messaggio = $"Impossibile aggiungere il Menu", IsOk = false };
        }

        public List<Menu> GetAllMenu()
        {
            return menuRepo.GetAll();
        }

        #endregion


        public Utente GetAccount(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            return utentiRepo.GetByUsername(username);
        }

        public Esito AssociaPiattoAMenu(int idPiatto, int idMenu)
        {
            throw new NotImplementedException();
        }

        public Esito DisassociaPiattoAMenu(int idPiatto, int idMenu)
        {
            throw new NotImplementedException();
        }
    }
}
