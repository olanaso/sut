using Sut.Context;
using Sut.Entities;
using Sut.Extensions;
using Sut.IRepositories;
using Sut.Log;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sut.Security;

namespace Sut.Repositories
{
    public class ProcedimientoCategoriaRepository : BaseRepository<ProcedimientoCategoria>, IProcedimientoCategoriaRepository
    {
        public ProcedimientoCategoriaRepository(IContext context) : base(context) { }
        public ProcedimientoCategoria GetOne(long ProcedimientoCategoriaid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoCategoria
                        .FirstOrDefault(x => x.ProcedimientoId == ProcedimientoCategoriaid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
        public List<ProcedimientoCategoria> Lsitaprocedimientocategoria(long ExpedienteId, long ProcedimientoCategoriaid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoCategoria
                        .Where(x => x.ExpedienteId == ExpedienteId && x.ProcedimientoId == ProcedimientoCategoriaid) 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedimientoCategoria> Lsitaprocedimientocategoriavalor0(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoCategoria
                        .Where(x => x.ExpedienteId == ExpedienteId && x.ProcedimientoId == 0)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedimientoCategoria> Lsitaprocedimientocategoriavalor(long ProcedimientoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ProcedimientoCategoria
                        .Where(x => x.ProcedimientoId == ProcedimientoId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new void Save(ProcedimientoCategoria obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 

                ctx.Entry(obj).State = EntityState.Added;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new void Modificar(ProcedimientoCategoria obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;


                ctx.Entry(obj).State = EntityState.Modified;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new void Eliminar(ProcedimientoCategoria obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var oldBL = ctx.ProcedimientoCategoria.Where(x => x.ProcedimientoId == obj.ProcedimientoId && x.ExpedienteId == obj.ExpedienteId);
                oldBL.ToList()
                    .ForEach(x => {
                        if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                    }); 

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
