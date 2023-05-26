using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sut.Repositories
{
    public class PlazoAtencionRepository : BaseRepository<PlazoAtencion>, IPlazoAtencionRepository
    {
        public PlazoAtencionRepository(IContext context) : base(context) { }

        public PlazoAtencion GetOne(long ReproduccionId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.PlazoAtencion
                        .SingleOrDefault(x => x.PlazoAtencionId == ReproduccionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlazoAtencion> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.PlazoAtencion 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlazoAtencion> GetAll(long TablaAsmeId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.PlazoAtencion
                        .Where(x=>x.ProcedimientoId == TablaAsmeId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(long ReproduccionId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vReproduccionId = ctx.PlazoAtencion.Where(x => x.PlazoAtencionId == ReproduccionId);

                foreach (PlazoAtencion o in vReproduccionId)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(PlazoAtencion obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.PlazoAtencionId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
