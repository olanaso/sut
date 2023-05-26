using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IInductorValorRepository : IBaseRepository<InductorValor>
    {
        void Guardar(List<InductorValor> lista);

        InductorValor GetOne(long InductorId);

        InductorValor GetOneValor(long InductorId, long ExpedienteId, long UnidadOrganicaId);
        List<InductorValor> GetOneExpediente(long ExpedienteId);
        List<InductorValor> listGetOne(long InductorId);
    }
}
