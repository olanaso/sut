using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IBaseLegalNormaRepository : IBaseRepository<BaseLegalNorma>
    {
        BaseLegalNorma GetOne(long BaseLegalId, long BaseLegalNormaId);
        List<BaseLegalNorma> GetDetails(long BaseLegalId);
        List<BaseLegalNorma> GetByExpediente(List<long> ids);
    }
}
