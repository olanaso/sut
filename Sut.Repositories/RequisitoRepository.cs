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
    public class RequisitoRepository : BaseRepository<Requisito>, IRequisitoRepository
    {
        public RequisitoRepository(IContext context) : base(context) { }

  
       public RequisitoFormulario GetOneForm(long RequisitoId, long FormularioId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.RequisitoFormulario
                        .SingleOrDefault(x => x.RequisitoId == RequisitoId && x.FormularioId == FormularioId && x.Eliminado==0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Requisito GetOne(long RequisitoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Requisito
                        .SingleOrDefault(x => x.RequisitoId == RequisitoId && x.Eliminado == 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Requisito> GetByProcedimiento(long ProcedimientoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Requisito
                        .Where(x => x.ProcedimientoId == ProcedimientoId && x.Eliminado == 0)
                        .Include(x => x.BaseLegal)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Requisito> GetByProcedimientoELI(long ProcedimientoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Requisito
                        .Where(x => x.ProcedimientoId == ProcedimientoId && x.Eliminado == 3)
                        .Include(x => x.BaseLegal)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<RequisitoFormulario> listProcedimiento(long RequisitoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.RequisitoFormulario
                        .Where(x => x.RequisitoId == RequisitoId && x.Eliminado == 0) 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Requisito obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.RequisitoId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SaveFormulario(RequisitoFormulario obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.FormularioId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
