using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IMapaProcedimientoRepository : IBaseRepository<MapaProcedimiento>
    {
        new IQueryable<MapaProcedimiento> GetAll();
        new IQueryable<MapaProcedimiento> GetAllBy(System.Linq.Expressions.Expression<Func<MapaProcedimiento, bool>> predicate);
        MapaProcedimiento GetOne(long MapaProcedimientoId);
        new void Save(MapaProcedimiento obj);
    }
}
