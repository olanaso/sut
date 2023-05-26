using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IVCalidadEXANTERepository : IBaseRepository<VCalidadEXANTE>
    {
        VCalidadEXANTE GetOne(long VCalidadEXANTEid); 
        List<VCalidadEXANTE> GetAll(long CodEntidad);
        List<VCalidadEXANTE> GetAllexante(int ICODCALIDADEXANTE);
    }
}
