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
    public class ArMotivoAdjuntoRepository : BaseRepository<ArMotivoAdjunto>, IArMotivoAdjuntoRepository
    {
        public ArMotivoAdjuntoRepository(IContext context) : base(context) { }

        public ArMotivoAdjunto GetOne(long ArMotivoAdjuntoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ArMotivoAdjunto
                        .SingleOrDefault(x => x.ArMotivoAdjuntoId == ArMotivoAdjuntoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<ArMotivoAdjunto> GetAll(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ArMotivoAdjunto
                        .Where(x => x.ExpedienteId == ExpedienteId && x.Activo==0)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ArMotivoAdjunto> GetAllxid(long ExpedienteId, long NormaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ArMotivoAdjunto
                        .Where(x => x.ExpedienteId == ExpedienteId && x.NormaId == NormaId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ArMotivoAdjunto> GetAllLikePaginEliminar(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ArMotivoAdjunto
                        .Where(x => x.ExpedienteId == ExpedienteId && x.Activo==0)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(long ArMotivoAdjuntoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vArMotivoAdjuntoId = ctx.ArMotivoAdjunto.Where(x => x.ArMotivoAdjuntoId == ArMotivoAdjuntoId);

                foreach (ArMotivoAdjunto o in vArMotivoAdjuntoId)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Save(ArMotivoAdjunto obj)
        {
            try
            { 
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.ArMotivoAdjuntoId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public void modificar(ArMotivoAdjunto obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State =  EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
