using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IIncentivosFormulasService
    {
       
        void Save(IncentivosFormulas obj);
         
        List<IncentivosFormulas> GetByIncentivosFormulas(IncentivosFormulas filtro, int pageIndex, int pageSize, ref int totalRows);
        IncentivosFormulas GetByone(long EntidadId);


    }
}
