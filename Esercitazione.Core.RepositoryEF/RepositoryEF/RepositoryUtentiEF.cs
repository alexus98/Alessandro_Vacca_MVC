using Esercitazione.Core.Entities;
using Esercitazione.Core.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.RepositoryEF.RepositoryEF
{
    public class RepositoryUtentiEF : IUtenteRepository
    {

        private readonly MasterContext context;

        public RepositoryUtentiEF(MasterContext ctx)
        {
            context = ctx;
        }

        public Utente Add(Utente item)
        {
            context.Utenti.Add(item);
            context.SaveChanges();
            return item;
        }

        public bool Delete(Utente item)
        {
            context.Utenti.Remove(item);
            context.SaveChanges();
            return true;
        }

        public List<Utente> GetAll()
        {
            return context.Utenti.ToList();
        }

        public Utente? GetById(int id)
        {
            return context.Utenti.FirstOrDefault(u => u.Id == id);
        }

        public Utente GetByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;
            return context.Utenti.FirstOrDefault(u => u.Username == username);
        }

        public Utente Update(Utente item)
        {
            context.Utenti.Update(item);
            context.SaveChanges();
            return item;
        }
    }
}
