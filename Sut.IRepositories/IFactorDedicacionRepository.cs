using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IFactorDedicacionRepository : IBaseRepository<FactorDedicacion>
    {
        void Guardar(List<FactorDedicacion> lista);
        void Eliminar(long ExpedienteId);
        List<FactorDedicacion> GetByFactorDedicacion(long ExpedienteId);
        List<FactorDedicacion> GetByFactorDedicacionF1(long ExpedienteId, TipoRecurso tiporecurso);
    }
}
