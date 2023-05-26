using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface IMetaDatoRepository : IBaseRepository<MetaDato>
    {
        MetaDato GetOne(long MetaDatoId);
        List<MetaDato> GetByParent(long PadreId);
        List<MetaDato> GetByLista();
        void Save(MetaDato obj);
        void Eliminar(long MetaDatoId);
    }
}
