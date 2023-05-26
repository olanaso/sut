using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IIncentivosFormulasCorteRepository : IBaseRepository<IncentivosFormulasCorte>
    {
        
        new void Save(IncentivosFormulasCorte obj); 
        IncentivosFormulasCorte GetByone(long IncentivosFormulasCorteId); 

        new List<IncentivosFormulasCorte> GetAll();


    }
}
