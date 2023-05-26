using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IEnumeradoService
    {
        List<Enumerado> GetByTipo(TipoEnumerado tipo);
    }
}
