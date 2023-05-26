using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface ICorreosRepository : IBaseRepository<Correos>
    {
        
        new void Save(Correos obj); 
        Correos GetByone(long rolId);

        void Eliminar(long rolId);

        new List<Correos> GetAll();


    }
}
