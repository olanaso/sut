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
    public class ProcedimientoCargosApeRepository : BaseRepository<ProcedimientoCargosApe>, IProcedimientoCargosApeRepository
    {
        public ProcedimientoCargosApeRepository(IContext context) : base(context) { }

        public ProcedimientoCargosApe GetOne(long ProcedimientoCargosApeid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoCargosApe
                        .SingleOrDefault(x => x.ProcedimientoCargosApeID == ProcedimientoCargosApeid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedimientoCargosApe> GetOnelista(long ProcedimientoCargosApeid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoCargosApe
                    .Where(x=>x.ProcedimientoId== ProcedimientoCargosApeid)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(int ProcedimientoID, int orden)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                //var vProcedimientoCargosApeID = ctx.ProcedimientoCargosApe.Where(x => x.ProcedimientoId == ProcedimientoID && x.ordenape== orden);

                var vProcedimientoCargosApeID = ctx.ProcedimientoCargosApe.Where(x => x.ProcedimientoCargosApeID == ProcedimientoID);

                foreach (ProcedimientoCargosApe o in vProcedimientoCargosApeID)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public ProcedimientoCargosApe LsitaGetOne()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoCargosApe
                        .SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProcedimientoCargosApe LsitaGetOneorden(long ProcedimientoCargosApeid, int orden)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoCargosApe
                    .Where(x=>x.ProcedimientoId== ProcedimientoCargosApeid && x.ordenape==orden)
                     .SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProcedimientoCargosApe> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.ProcedimientoCargosApe 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         

        public new void Save(ProcedimientoCargosApe obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.ProcedimientoCargosApeID > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
    }
}
