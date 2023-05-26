using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface ICorreosService
    {
       
        void Save(Correos obj);
         
        List<Correos> GetByCorreos(Correos filtro, int pageIndex, int pageSize, ref int totalRows);
        
        List<Correos> GetAll();
        Correos GetByone(long rolId);


        void Eliminar(long rolId);


    }
}
