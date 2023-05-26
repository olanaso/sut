using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IProcedimientoCargosService
    {
        ProcedimientoCargos GetOne(long ProcedimientoCargosid);
        List<ProcedimientoCargos> GetOnelista(long ProcedimientoCargosid);
        ProcedimientoCargos LsitaGetOne();
        List<ProcedimientoCargos> GetAllLikePagin(ProcedimientoCargos filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(ProcedimientoCargos obj);

        void Eliminar(long ProcedimientoCargosID);

    }
}
