using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IBaseLegalRepository : IBaseRepository<BaseLegal>
    {
        void Save(BaseLegal obj);
    }
}
