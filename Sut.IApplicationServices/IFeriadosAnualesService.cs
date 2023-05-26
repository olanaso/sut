using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IApplicationServices
{
    public interface IFeriadosAnualesService
    {
        FeriadosAnuales GetOne(long FeriadosAnualesId);
        List<FeriadosAnuales> GetByParent(long PadreId);

        List<FeriadosAnuales> GetByLista(); 
        List<FeriadosAnuales> GetAllLikePagin(FeriadosAnuales filtro, int pageIndex, int pageSize, ref int totalRows);
        List<FeriadosAnuales> GetAllLikePaginAsistencia(FeriadosAnuales filtro, int pageIndex, int pageSize, ref int totalRows);


        
        void Save(FeriadosAnuales obj);
        void Eliminar(long FeriadosAnualesId);
    }
}
