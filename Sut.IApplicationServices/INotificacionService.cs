using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface INotificacionService
    {
       
        void Save(Notificacion obj);
         
        List<Notificacion> GetByNotificacion(Notificacion filtro, int pageIndex, int pageSize, ref int totalRows);


        Notificacion GetByone(long notificacionId);


    }
}
