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
    public class MetaDatoRepository : BaseRepository<MetaDato>, IMetaDatoRepository
    {
        public MetaDatoRepository(IContext context) : base(context) { }
        
        public MetaDato GetOne(long MetaDatoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.MetaDato
                        .SingleOrDefault(x => x.MetaDatoId == MetaDatoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MetaDato> GetByLista()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.MetaDato
                        .Where(x => x.PadreId==null && x.Valor01 == "1").ToList()
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MetaDato> GetByParent(long PadreId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.MetaDato
                        .Where(x => x.PadreId == PadreId)
                        .OrderBy(o => o.Orden)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(MetaDato obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.MetaDatoId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(long MetaDatoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vMetaDatoId = ctx.MetaDato.Where(x => x.MetaDatoId == MetaDatoId);

                foreach (MetaDato o in vMetaDatoId)
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
