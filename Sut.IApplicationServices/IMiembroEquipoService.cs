using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IMiembroEquipoService
    {
        List<MiembroEquipo> GetAllLikePagin(MiembroEquipo filtro, int pageIndex, int pageSize, ref int totalRows);
        MiembroEquipo GetOne(long MiembroEquipoId);
        MiembroEquipo GetOne(string NroDocumento);
        List<MiembroEquipo> GetByEntidad(long EntidadId);
        void Save(MiembroEquipo obj);
        void Delete(long id);
    }
}
