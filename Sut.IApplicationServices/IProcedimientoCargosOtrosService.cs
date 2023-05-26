using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IProcedimientoCargosOtrosService
    {
        ProcedimientoCargosOtros GetOne(long ProcedimientoCargosOtrosid);
        ProcedimientoCargosOtros LsitaGetOne();

        List<ProcedimientoCargosOtros> GetOnelista(long ProcedimientoCargosApeid);
        List<ProcedimientoCargosOtros> GetAllLikePagin(ProcedimientoCargosOtros filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(ProcedimientoCargosOtros obj);

        void Eliminar(long ProcedimientoCargosOtrosID);

    }
}
