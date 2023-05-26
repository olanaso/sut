using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IProcedimientoUndOrgResponsableService
    {
        ProcedimientoUndOrgResponsable GetOne(long ProcedimientoUndOrgResponsableid);
        ProcedimientoUndOrgResponsable LsitaGetOne(long procedimientoId);

        List<ProcedimientoUndOrgResponsable> GetAll(long procedimientoId);
        List<ProcedimientoUndOrgResponsable> GetAllLikePagin(ProcedimientoUndOrgResponsable filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(ProcedimientoUndOrgResponsable obj);

        void Eliminar(long ProcedimientoUndOrgResponsableID);

    }
}
