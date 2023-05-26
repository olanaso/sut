using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface IUnidadOrganicaRepository : IBaseRepository<UnidadOrganica>
    {
        UnidadOrganica GetOne(long UnidadOrganicaId);
        List<UnidadOrganica> GetAll(long EntidadId);
        void Save(UnidadOrganica obj);
        List<UnidadOrganica> GetByExpediente(long ExpedienteId);
        void Eliminar(long UnidadOrganicaId);
    }
}
