using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IIncentivosFormulasRepository : IBaseRepository<IncentivosFormulas>
    {
        
        new void Save(IncentivosFormulas obj); 
        IncentivosFormulas GetByone(long IncentivosFormulasId); 

        new List<IncentivosFormulas> GetAll();


    }
}
