using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface IGrupoEntidadRepository : IBaseRepository<GrupoEntidad>
    {
        GrupoEntidad GetOne(long GrupoEntidadId);
        List<GrupoEntidad> GetAll(long EntidadId);
        void Save(GrupoEntidad obj); 


        void Eliminar(long GrupoEntidadId);
    }
}
