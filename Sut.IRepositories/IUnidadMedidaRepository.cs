using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface IUnidadMedidaRepository : IBaseRepository<UnidadMedida>
    {
        UnidadMedida GetOne(long UnidadMedidaId);
        List<UnidadMedida> GetAll();
        void Save(UnidadMedida obj);
    }
}
