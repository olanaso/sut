using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IVCalidadEXANTEService
    {
        VCalidadEXANTE GetOne(long VCalidadid); 
        List<VCalidadEXANTE> GetAll( long CodEntidad);


        List<VCalidadEXANTE> GetAllexante(int ICODCALIDADEXANTE);

        List<VCalidadEXANTE> GetAllLikePagin(VCalidadEXANTE filtro, int pageIndex, int pageSize, ref int totalRows);


    }
}
