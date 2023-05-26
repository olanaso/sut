
using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IProcedimientoUndOrgResponsableRepository : IBaseRepository<ProcedimientoUndOrgResponsable>
    {
        ProcedimientoUndOrgResponsable GetOne(long ProcedimientoUndOrgResponsableid);
        ProcedimientoUndOrgResponsable LsitaGetOne(long procedimientoId);
        List<ProcedimientoUndOrgResponsable> GetAll(long procedimientoId);
        void Save(ProcedimientoUndOrgResponsable obj);

        void Eliminar(long ProcedimientoUndOrgResponsableID);

    }
}
