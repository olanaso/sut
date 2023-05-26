using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IApplicationServices
{
    public interface IGrupoEntidadService
    {
        GrupoEntidad GetOne(long GrupoEntidadId);
        List<GrupoEntidad> GetAll(long EntidadId);
        List<GrupoEntidad> GetAllLikePagin(GrupoEntidad filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(GrupoEntidad obj); 
        void Eliminar(long GrupoEntidadId);
    }
}
