using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IProcedimientoCargosRepository : IBaseRepository<ProcedimientoCargos>
    {
        ProcedimientoCargos GetOne(long ProcedimientoCargosid);
        List<ProcedimientoCargos> GetOnelista(long ProcedimientoCargosid);
        ProcedimientoCargos LsitaGetOne();
        List<ProcedimientoCargos> GetAll();
        void Save(ProcedimientoCargos obj);
        void Eliminar(long ProcedimientoCargosID);
    }
}
