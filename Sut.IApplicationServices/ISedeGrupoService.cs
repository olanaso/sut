using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IApplicationServices
{
    public interface ISedeGrupoService
    {
        SedeGrupo GetOne(long SedeGrupoId);
        List<SedeGrupo> GetAll(long EntidadId); 
         List<SedeGrupo> GetAllLikePagin(SedeGrupo filtro, int pageIndex, int pageSize, ref int totalRows);
        List<SedeGrupo> GetAllLikePaginGrupo(SedeGrupo filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(List<SedeGrupo> obj);
        void Eliminar(int SedeGrupoId);
        void EliminarGrupo(int SedeGrupoId);
    }
}
