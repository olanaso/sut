using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IMiembroEquipoRepository : IBaseRepository<MiembroEquipo>
    {
        MiembroEquipo GetOne(string NroDocumento);
        MiembroEquipo GetOne(long MiembroEquipoId);
        new void Save(MiembroEquipo obj);
        new List<MiembroEquipo> GetAll();
        List<MiembroEquipo> GetByEntidad(long EntidadId);
    }
}
