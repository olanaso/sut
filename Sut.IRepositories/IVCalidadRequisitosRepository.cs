using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IVCalidadRequisitosRepository : IBaseRepository<VCalidadRequisitos>
    {
        VCalidadRequisitos GetOne(long VCalidadRequisitosid); 
        List<VCalidadRequisitos> GetAll(); 
      
    }
}
