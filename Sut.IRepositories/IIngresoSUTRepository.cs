using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IIngresoSUTRepository : IBaseRepository<IngresoSUT>
    {
  
        void Save(IngresoSUT obj);
      
    }
}
