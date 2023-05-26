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
    public class ProcedimientoUndOrgResponsableRepository : BaseRepository<ProcedimientoUndOrgResponsable>, IProcedimientoUndOrgResponsableRepository
    {
        public ProcedimientoUndOrgResponsableRepository(IContext context) : base(context) { }

        public ProcedimientoUndOrgResponsable GetOne(long ProcedimientoUndOrgResponsableid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoUndOrgResponsable
                        .SingleOrDefault(x => x.ProcedimientoUndOrgResponsableID == ProcedimientoUndOrgResponsableid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(long ProcedimientoUndOrgResponsableID)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vProcedimientoUndOrgResponsableID = ctx.ProcedimientoUndOrgResponsable.Where(x => x.ProcedimientoUndOrgResponsableID == ProcedimientoUndOrgResponsableID);

                foreach (ProcedimientoUndOrgResponsable o in vProcedimientoUndOrgResponsableID)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProcedimientoUndOrgResponsable LsitaGetOne(long procedimientoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoUndOrgResponsable
                        .SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProcedimientoUndOrgResponsable> GetAll(long procedimientoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.ProcedimientoUndOrgResponsable
                        .Where(x=>x.ProcedimientoId == procedimientoId)
                        .OrderBy(x=>x.ProcedimientoUndOrgResponsableID)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         

        public new void Save(ProcedimientoUndOrgResponsable obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.ProcedimientoUndOrgResponsableID > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
    }
}
