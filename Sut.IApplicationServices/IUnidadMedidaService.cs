using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IApplicationServices
{
    public interface IUnidadMedidaService
    {
        UnidadMedida GetOne(long UnidadMedidaId);
        List<UnidadMedida> GetAll();
        List<UnidadMedida> GetAllLikePagin(UnidadMedida filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(UnidadMedida obj);
    }
}
