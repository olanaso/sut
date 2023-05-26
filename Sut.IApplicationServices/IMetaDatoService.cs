using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IApplicationServices
{
    public interface IMetaDatoService
    {
        MetaDato GetOne(long MetaDatoId);
        List<MetaDato> GetByParent(long PadreId);

        List<MetaDato> GetByLista(); 
        List<MetaDato> GetAllLikePagin(MetaDato filtro, int pageIndex, int pageSize, ref int totalRows);
        List<MetaDato> GetAllLikePaginAsistencia(MetaDato filtro, int pageIndex, int pageSize, ref int totalRows);


        
        void Save(MetaDato obj);
        void Eliminar(long MetaDatoId);
    }
}
