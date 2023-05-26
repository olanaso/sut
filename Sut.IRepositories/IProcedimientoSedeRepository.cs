using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IProcedimientoSedeRepository : IBaseRepository<ProcedimientoSede>
    {
        List<ProcedimientoSede> GetByExpediente(long ExpedienteId);
    }
}
