using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IPlanTrabajoRepository : IBaseRepository<PlanTrabajo>
    {
        IEnumerable<PlanTrabajo> GetByEntidad(long EntidadId);
        PlanTrabajo GetOne(long PlanTrabajoId);
        PlanTrabajo GetOneEntidad(long EntidadId);
        new void Save(PlanTrabajo obj);
    }
}
