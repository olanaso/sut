using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IAnexoNoIdentificableRepository : IBaseRepository<AnexoNoIdentificable>
    {
        List<AnexoNoIdentificable> GetByExpediente(long ExpedienteId, TipoRecurso tipo);
        List<AnexoNoIdentificable> GetByExpedientep1(long ExpedienteId, TipoRecurso tipo);
    }
}
