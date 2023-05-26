using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IRecursoCostoService
    {
        List<RecursoCosto> GetCostoPersonal(long ExpedienteId);
        List<RecursoCosto> GetRecursoCosto(long ExpedienteId, TipoRecurso tipo);
        void Guardar(List<RecursoCosto> lista);
    }
}
