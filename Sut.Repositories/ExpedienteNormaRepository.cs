using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Sut.Repositories
{
    public class ExpedienteNormaRepository: BaseRepository<ExpedienteNorma>, IExpedienteNormaRepository
    {
        public ExpedienteNormaRepository(IContext context) : base(context) { }

        public IEnumerable<ExpedienteNorma> GetByExpediente(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ExpedienteNorma
                    .Include(x => x.TipoNorma)
                    .Where(x => x.ExpedienteId == ExpedienteId && x.EstadoPublicado==0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExpedienteNorma GetOne(long NormaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ExpedienteNorma
                            .Include(x => x.ArchivoAdjunto)
                            .Include(x => x.ArMotivoAdjunto)
                            .SingleOrDefault(x => x.NormaId == NormaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExpedienteNorma GetOneexpediente(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ExpedienteNorma
                            .Include(x => x.ArchivoAdjunto)
                            .Include(x => x.ArMotivoAdjunto)
                            .SingleOrDefault(x => x.ExpedienteId == ExpedienteId && x.EstadoPublicado==1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ExpedienteNorma> GetByExpedientenorma(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ExpedienteNorma 
                            .Include(x => x.TipoNorma)
                        .Where(x => x.ExpedienteId == ExpedienteId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public new void Save(ExpedienteNorma obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.NormaId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new void eliminar(long id)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vProcedimientoCargosApeID = ctx.ExpedienteNorma.Where(x => x.NormaId == id );

                foreach (ExpedienteNorma o in vProcedimientoCargosApeID)
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
