using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IDatoService
    {
        List<Dato> GetByTipo(TipoDato tipo);
        Dato GetOne(long MetaDatoId, TipoDato tipo);
        void Save(Dato obj);
    }
}
