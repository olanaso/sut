using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IApplicationServices
{
    public interface IUnidadOrganicaService
    {
        UnidadOrganica GetOne(long UnidadOrganicaId);
        List<UnidadOrganica> GetAll(long EntidadId);
        List<UnidadOrganica> GetAllLikePagin(UnidadOrganica filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(UnidadOrganica obj);
        List<UnidadOrganica> GetByExpediente(long ExpedienteId);
        void Eliminar(long UnidadOrganicaId);
    }
}
