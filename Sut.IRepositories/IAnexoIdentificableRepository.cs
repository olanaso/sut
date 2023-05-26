using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IAnexoIdentificableRepository : IBaseRepository<AnexoIdentificable>
    {
        List<AnexoIdentificable> GetByExpediente(long ExpedienteId, TipoRecurso tipo);

        List<AnexoIdentificable> GetAnexoIdentificableByExpedienteTablaASMEId(long ExpedienteId, TipoRecurso tipo, long TablaASMEId);
    }
}
