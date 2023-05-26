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
    public class ProcedimientoCargosRepository : BaseRepository<ProcedimientoCargos>, IProcedimientoCargosRepository
    {
        public ProcedimientoCargosRepository(IContext context) : base(context) { }

        public ProcedimientoCargos GetOne(long ProcedimientoCargosid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoCargos
                        .SingleOrDefault(x => x.ProcedimientoCargosID == ProcedimientoCargosid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedimientoCargos> GetOnelista(long ProcedimientoCargosid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoCargos
                    .Where(x=>x.ProcedimientoId== ProcedimientoCargosid)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProcedimientoCargos LsitaGetOne()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoCargos
                        .SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProcedimientoCargos> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.ProcedimientoCargos 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         

        public new void Save(ProcedimientoCargos obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.ProcedimientoCargosID > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(long ProcedimientoCargosID)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vProcedimientoCargos = ctx.ProcedimientoCargos.Where(x => x.ProcedimientoCargosID == ProcedimientoCargosID);

                foreach (ProcedimientoCargos o in vProcedimientoCargos)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
