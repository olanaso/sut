using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IRecursoCostoRepository : IBaseRepository<RecursoCosto>
    {
        List<RecursoCosto> GetCostoPersonal(long ExpedienteId);
        List<RecursoCosto> GetRecursoCosto(long ExpedienteId, TipoRecurso tipo);

        List<RecursoCosto> GetRecursoCostoLista(long ExpedienteId);
        void Guardar(List<RecursoCosto> lista);
    }
}
