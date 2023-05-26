using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IApplicationServices
{
    public interface ISedeService
    {
        Sede GetOne(long SedeId);
        List<Sede> GetAll(long EntidadId); 
         List<Sede> GetAllLikePagin(Sede filtro, int pageIndex, int pageSize, ref int totalRows);
        List<Sede> GetAllLikePaginGrupo(Sede filtro, int pageIndex, int pageSize, ref int totalRows);


        List<Sede> GetAllLikePaginCarga(Sede filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(Sede obj);
        void Eliminar(int SedeId);
    }
}
