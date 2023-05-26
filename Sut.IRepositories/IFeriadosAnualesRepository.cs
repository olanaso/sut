using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface IFeriadosAnualesRepository : IBaseRepository<FeriadosAnuales>
    {
        FeriadosAnuales GetOne(long FeriadosAnualesId);
        List<FeriadosAnuales> GetByParent(long PadreId);
        List<FeriadosAnuales> GetByLista();
        void Save(FeriadosAnuales obj);
        void Eliminar(long FeriadosAnualesId);
    }
}
