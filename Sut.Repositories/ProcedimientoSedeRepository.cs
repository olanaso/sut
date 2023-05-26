using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories
{
    public class ProcedimientoSedeRepository : BaseRepository<ProcedimientoSede>, IProcedimientoSedeRepository
    {
        public ProcedimientoSedeRepository(IContext context) : base(context) { }

        public List<ProcedimientoSede> GetByExpediente(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoSede
                        .Include("Sede")
                        .Where(x => x.Procedimiento.ExpedienteId == ExpedienteId && x.Procedimiento.Estado != 3 && x.Procedimiento.Operacion != OperacionExpediente.Eliminacion)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
