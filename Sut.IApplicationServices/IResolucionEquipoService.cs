using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IResolucionEquipoService
    {
        List<ResolucionEquipo> GetAllLikePagin(ResolucionEquipo filtro, int pageIndex, int pageSize, ref int totalRows);
        ResolucionEquipo GetOne(long ResolucionEquipoId);

        ResolucionEquipo GetOneEntidad(long EntidadId);
        void Save(ResolucionEquipo obj);
    }
}
