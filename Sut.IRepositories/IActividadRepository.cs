using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IActividadRepository : IBaseRepository<Actividad>
    {

        void ActualizarActividad(Actividad obj);
        void Guardar(Actividad obj);

        void SaveOnlyActividad(Actividad obj);

        void GuardarRecursosEliminar(Actividad obj);
        List<Actividad> GetAllBy(System.Linq.Expressions.Expression<Func<Actividad, bool>> predicate);
    }
}
