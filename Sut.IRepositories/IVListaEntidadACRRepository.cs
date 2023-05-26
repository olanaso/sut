using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IVListaEntidadACRRepository : IBaseRepository<VListaEntidadACR>
    {
      
        List<VListaEntidadACR> GetAll();
        List<VListaEntidadACR> GetAllproceso();

    }
}
