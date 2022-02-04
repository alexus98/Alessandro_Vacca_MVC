using Esercitazione.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.InterfaceRepositories
{
    public interface IPiattoRepository : IRepository<Piatto>
    {
        public Piatto AssociaPiatto(int idPiatto, int idMenu);
        public Piatto DisassociaPiatto(int idPiatto, int idMenu);
    }
}
