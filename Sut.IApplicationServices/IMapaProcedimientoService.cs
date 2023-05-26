using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IMapaProcedimientoService
    {
        List<MapaProcedimiento> GetAllLikePagin(MapaProcedimiento filtro, int pageIndex, int pageSize, ref int totalRows);
        MapaProcedimiento GetOne(long MapaProcedimeintoId);
        List<MapaProcedimiento> GetByParent(long PadreId);
        void Save(MapaProcedimiento obj);
    }
}
