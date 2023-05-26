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
    public class ProcedimientoCargosOtrosRepository : BaseRepository<ProcedimientoCargosOtros>, IProcedimientoCargosOtrosRepository
    {
        public ProcedimientoCargosOtrosRepository(IContext context) : base(context) { }

        public ProcedimientoCargosOtros GetOne(long ProcedimientoCargosOtrosid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoCargosOtros
                        .SingleOrDefault(x => x.ProcedimientoCargosOtrosID == ProcedimientoCargosOtrosid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(long ProcedimientoCargosOtrosID)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vProcedimientoCargosOtrosID = ctx.ProcedimientoCargosOtros.Where(x => x.ProcedimientoCargosOtrosID == ProcedimientoCargosOtrosID);

                foreach (ProcedimientoCargosOtros o in vProcedimientoCargosOtrosID)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProcedimientoCargosOtros LsitaGetOne()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoCargosOtros
                        .SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedimientoCargosOtros> GetOnelista(long ProcedimientoCargosApeid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoCargosOtros
                    .Where(x => x.ProcedimientoId == ProcedimientoCargosApeid)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProcedimientoCargosOtros> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.ProcedimientoCargosOtros 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         

        public new void Save(ProcedimientoCargosOtros obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.ProcedimientoCargosOtrosID > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
    }
}
