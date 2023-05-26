using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface ICalendarioActividadesRepository : IBaseRepository<CalendarioActividades>
    {
        CalendarioActividades GetOne(long CalendarioActividadesId);
        List<CalendarioActividades> GetAll(long EntidadId);
        void Save(CalendarioActividades obj); 

        void Eliminar(long CalendarioActividadesId);
    }
}
