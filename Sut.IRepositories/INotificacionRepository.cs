using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface INotificacionRepository : IBaseRepository<Notificacion>
    {
        
        new void Save(Notificacion obj); 
        Notificacion GetByone(long notificacionId); 

        new List<Notificacion> GetAll();


    }
}
