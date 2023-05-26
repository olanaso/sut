
using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IProcedimientoCargosApeRepository : IBaseRepository<ProcedimientoCargosApe>
    {
        ProcedimientoCargosApe GetOne(long ProcedimientoCargosApeid);

        List<ProcedimientoCargosApe> GetOnelista(long ProcedimientoCargosApeid);
        ProcedimientoCargosApe LsitaGetOne();
        ProcedimientoCargosApe LsitaGetOneorden(long ProcedimientoCargosApeid, int orden);
        List<ProcedimientoCargosApe> GetAll();
        void Save(ProcedimientoCargosApe obj);

        void Eliminar(int ProcedimientoID, int orden);

    }
}
