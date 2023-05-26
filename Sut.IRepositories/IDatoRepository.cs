using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IDatoRepository : IBaseRepository<Dato>
    {
        Dato GetOne(long MetaDatoId, TipoDato tipo);
        List<Dato> GetByTipo(TipoDato tipo);
        new void Save(Dato obj);
    }
}
