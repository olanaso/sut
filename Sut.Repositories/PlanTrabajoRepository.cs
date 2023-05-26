using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories
{
    public class PlanTrabajoRepository : BaseRepository<PlanTrabajo>, IPlanTrabajoRepository
    {
        public PlanTrabajoRepository(IContext context) : base(context) { }

        public IEnumerable<PlanTrabajo> GetByEntidad(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.PlanTrabajo.Where(x => x.EntidadId == EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PlanTrabajo GetOne(long PlanTrabajoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.PlanTrabajo
                            .Include(x => x.ArchivoAdjunto)
                            .SingleOrDefault(x => x.PlanTrabajoId == PlanTrabajoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PlanTrabajo GetOneEntidad(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.PlanTrabajo
                            .Include(x => x.ArchivoAdjunto)
                            .SingleOrDefault(x => x.Principal == "1" && x.EntidadId == EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new void Save(PlanTrabajo obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.PlanTrabajoId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
