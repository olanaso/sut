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
    public class MapaProcedimeintoRepository : BaseRepository<MapaProcedimiento>, IMapaProcedimientoRepository
    {
        public MapaProcedimeintoRepository(IContext context) : base(context) { }

        public new IQueryable<MapaProcedimiento> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.MapaProcedimiento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new IQueryable<MapaProcedimiento> GetAllBy(System.Linq.Expressions.Expression<Func<MapaProcedimiento, bool>> predicate)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.MapaProcedimiento.Where(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MapaProcedimiento GetOne(long MapaProcedimientoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.MapaProcedimiento
                            .SingleOrDefault(x => x.MapaProcedimientoId == MapaProcedimientoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new void Save(MapaProcedimiento obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.MapaProcedimientoId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
