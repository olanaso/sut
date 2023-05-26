using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IPlanTrabajoService
    {
        List<PlanTrabajo> GetAllLikePagin(PlanTrabajo filtro, int pageIndex, int pageSize, ref int totalRows);
        PlanTrabajo GetOne(long PlanTrabajoId);
        PlanTrabajo GetOneEntidad(long EntidadId);
        void Save(PlanTrabajo obj);
    }
}
