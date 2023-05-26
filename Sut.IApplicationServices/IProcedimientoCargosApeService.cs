using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IProcedimientoCargosApeService
    {
        ProcedimientoCargosApe GetOne(long ProcedimientoCargosApeid);

        List<ProcedimientoCargosApe> GetOnelista(long ProcedimientoCargosApeid);
        ProcedimientoCargosApe LsitaGetOne();
        ProcedimientoCargosApe LsitaGetOneorden(long ProcedimientoCargosApeid, int orden);
        List<ProcedimientoCargosApe> GetAllLikePagin(ProcedimientoCargosApe filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(ProcedimientoCargosApe obj);

        void Eliminar(int ProcedimientoID, int orden);

    }
}
