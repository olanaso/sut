using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface INormaRepository : IBaseRepository<Norma>
    {
        Norma GetOne(long NormaId);
        List<Norma> GetAll();
        void Save(Norma obj);
    }
}
