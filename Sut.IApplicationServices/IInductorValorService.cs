using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IApplicationServices
{
    public interface IInductorValorService
    {
        InductorValor GetOne(long InductorId);

        InductorValor GetOneValor(long InductorId, long ExpedienteId, long UnidadOrganicaId);
        List<InductorValor> listGetOne(long InductorId);
    }
}
