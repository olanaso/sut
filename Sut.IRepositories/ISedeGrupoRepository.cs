using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface ISedeGrupoRepository : IBaseRepository<SedeGrupo>
    {
        SedeGrupo GetOne(long SedeGrupoId); 
         List<SedeGrupo> GetAll(long EntidadId, long SedePadreId);
        List<SedeGrupo> GetAllgrupo(long EntidadId);
        void Save(List<SedeGrupo> obj); 
        void Eliminar(long SedeGrupoId);
        void EliminarGrupo(long SedeGrupoId);
    }
}
