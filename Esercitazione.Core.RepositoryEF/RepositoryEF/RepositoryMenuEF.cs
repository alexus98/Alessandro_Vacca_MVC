using Esercitazione.Core.Entities;
using Esercitazione.Core.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.RepositoryEF.RepositoryEF
{
    public class RepositoryMenuEF : IMenuRepository
    {

        private readonly MasterContext context;

        public RepositoryMenuEF(MasterContext ctx)
        {
            context = ctx;
        }

        public Menu Add(Menu item)
        {
            context.Menu.Add(item);
            context.SaveChanges();
            return item;
        }

        public bool Delete(Menu item)
        {
            context.Menu.Remove(item);
            context.SaveChanges();
            return true;
        }

        public List<Menu> GetAll()
        {
            return context.Menu.Include(m => m.Piatti).ToList();
        }

        public Menu? GetById(int id)
        {
            return context.Menu.Include(m => m.Piatti).FirstOrDefault(m => m.Id == id);
        }

        public Menu Update(Menu item)
        {
            context.Menu.Update(item);
            context.SaveChanges();
            return item;
        }
    }
}
