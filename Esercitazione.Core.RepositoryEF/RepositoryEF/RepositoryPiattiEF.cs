using Esercitazione.Core.Entities;
using Esercitazione.Core.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.RepositoryEF.RepositoryEF
{
    public class RepositoryPiattiEF : IPiattoRepository
    {

        private readonly MasterContext context;

        public RepositoryPiattiEF(MasterContext ctx)
        {
            context = ctx;
        }

        public Piatto Add(Piatto item)
        {
            context.Piatti.Add(item);
            context.SaveChanges();
            return item;
        }

        public bool Delete(Piatto item)
        {
            context.Piatti.Remove(item);
            context.SaveChanges();
            return true;
        }

        public List<Piatto> GetAll()
        {
            return context.Piatti.ToList();
        }

        public Piatto Update(Piatto item)
        {
            context.Piatti.Update(item);
            context.SaveChanges();
            return item;
        }

        public Piatto AssociaPiatto(int idPiatto, int idMenu)
        {
            throw new NotImplementedException();
        }

        public Piatto DisassociaPiatto(int idPiatto, int idMenu)
        {
            throw new NotImplementedException();
        }

        public Piatto? GetById(int id)
        {
            return context.Piatti.FirstOrDefault(p => p.Id == id);

        }
    }
}
