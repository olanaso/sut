using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IApplicationServices
{
    public interface IInductorService
    {
        Inductor GetOne(long InductorId);
        List<Inductor> GetAll(long EntidadId);
        List<Inductor> GetAllLikePagin(Inductor filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(Inductor obj);
        List<Inductor> GetByExpediente(long ExpedienteId);
        List<InductorValor> GetValoresByInductor(long EntidadId, long ExpedienteId, long InductorId);


        List<InductorValor> GetValoresByInductorEntidadid(long EntidadId, long ExpedienteId);

        void GuardarInductorValor(List<InductorValor> lista);
        void Eliminar(long InductorId);
    }
}
