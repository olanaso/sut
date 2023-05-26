using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sut.Repositories
{
    public class DetalleMaestroRepository : BaseRepository<DetalleMaestro>, IDetalleMaestroRepository
    {
        public DetalleMaestroRepository(IContext context) : base(context) { }

        public DetalleMaestro GetOne(long DetalleMaestroId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.DetalleMaestro
                        .SingleOrDefault(x => x.DetalleMaestroId == DetalleMaestroId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    

        public List<DetalleMaestro> GetAllXTIPO(TipoDetalleMaestro tipo)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                short o = (short)tipo;
                return ctx.DetalleMaestro 
                        .Where(x => (short)x.TipoDetalleMaestro == o && x.Activo)
                        .OrderBy(x=>x.DetalleMaestroId)
                        .ToList();
                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(DetalleMaestro obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.DetalleMaestroId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

        public void Eliminar(long IdDetalleMaestro)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vDetalleMaestroId = ctx.DetalleMaestro.Where(x => x.DetalleMaestroId == IdDetalleMaestro);

                foreach (DetalleMaestro o in vDetalleMaestroId)
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
