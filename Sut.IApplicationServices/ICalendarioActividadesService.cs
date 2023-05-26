using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IApplicationServices
{
    public interface ICalendarioActividadesService
    {
        CalendarioActividades GetOne(long CalendarioActividadesId);
        List<CalendarioActividades> GetAll(long EntidadId);
        List<CalendarioActividades> GetAllLikePagin(CalendarioActividades filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(CalendarioActividades obj); 
        void Eliminar(long CalendarioActividadesId);
    }
}
