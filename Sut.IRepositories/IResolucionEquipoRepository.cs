using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IResolucionEquipoRepository : IBaseRepository<ResolucionEquipo>
    {
        IEnumerable<ResolucionEquipo> GetByEntidad(long EntidadId);
        ResolucionEquipo GetOne(long ResolucionEquipoId);
        ResolucionEquipo GetOneEntidad(long EntidadId);
        new void Save(ResolucionEquipo obj);
    }
}
