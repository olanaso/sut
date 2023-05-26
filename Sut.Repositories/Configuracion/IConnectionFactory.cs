using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories.Configuracion
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnectionSUT { get; }
    }
}
