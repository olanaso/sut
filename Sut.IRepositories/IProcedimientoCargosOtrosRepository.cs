
using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IProcedimientoCargosOtrosRepository : IBaseRepository<ProcedimientoCargosOtros>
    {
        ProcedimientoCargosOtros GetOne(long ProcedimientoCargosOtrosid);
        ProcedimientoCargosOtros LsitaGetOne();
        List<ProcedimientoCargosOtros> GetAll();
        List<ProcedimientoCargosOtros> GetOnelista(long ProcedimientoCargosApeid);
        void Save(ProcedimientoCargosOtros obj);

        void Eliminar(long ProcedimientoCargosOtrosID);

    }
}
