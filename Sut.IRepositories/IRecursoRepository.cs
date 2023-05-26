using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface IRecursoRepository : IBaseRepository<Recurso>
    {
        Recurso GetOne(long RecursoId);
        List<Recurso> GetAll(long EntidadId, TipoRecurso tipo);
        List<Recurso> GetAll(long EntidadId);
        void Save(Recurso obj);
        void Eliminar(long IdRecurso);
        List<Recurso> GetByExpedienteUndOrganica(long ExpedienteId, long UnidadOrganicaId);

        List<Recurso> GetByExpedienteUndOrganica2(long ExpedienteId, TipoRecurso tiporecurso);
        List<Recurso> GetByExpedienteUndOrganicaWithDuracionTotal(long ExpedienteId, long UnidadOrganicaId);
    }
}
