using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IIncentivosFormulasCorteService
    {
       
        void Save(IncentivosFormulasCorte obj);
         
        List<IncentivosFormulasCorte> GetByIncentivosFormulasCorte(IncentivosFormulasCorte filtro, int pageIndex, int pageSize, ref int totalRows);
        IncentivosFormulasCorte GetByone(long EntidadId);


    }
}
