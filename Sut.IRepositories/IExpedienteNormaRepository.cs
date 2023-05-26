using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface IExpedienteNormaRepository : IBaseRepository<ExpedienteNorma>
    {
        IEnumerable<ExpedienteNorma> GetByExpediente(long ExpedienteId);
        ExpedienteNorma GetOne(long NormaId);

        ExpedienteNorma GetOneexpediente(long ExpedienteId);

        List<ExpedienteNorma> GetByExpedientenorma(long ExpedienteId);

        new void Save(ExpedienteNorma obj);
        new void eliminar(long id);
    }
}
