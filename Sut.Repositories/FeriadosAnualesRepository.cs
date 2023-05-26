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
    public class FeriadosAnualesRepository : BaseRepository<FeriadosAnuales>, IFeriadosAnualesRepository
    {
        public FeriadosAnualesRepository(IContext context) : base(context) { }
        
        public FeriadosAnuales GetOne(long FeriadosAnualesId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.FeriadosAnuales
                        .SingleOrDefault(x => x.FeriadosAnualesId == FeriadosAnualesId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FeriadosAnuales> GetByLista()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.FeriadosAnuales
                        .Where(x => x.Estado == 1).ToList()
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<FeriadosAnuales> GetByParent(long PadreId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.FeriadosAnuales
                          .Where(x => x.Estado == 1).ToList() 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(FeriadosAnuales obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.FeriadosAnualesId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(long FeriadosAnualesId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vFeriadosAnualesId = ctx.FeriadosAnuales.Where(x => x.FeriadosAnualesId == FeriadosAnualesId);

                foreach (FeriadosAnuales o in vFeriadosAnualesId)
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
