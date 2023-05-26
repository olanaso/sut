using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface ITablaAsmeReproduccionRepository : IBaseRepository<TablaAsmeReproduccion>
    {
        TablaAsmeReproduccion GetOne(long TablaAsmeReproduccionId);
        List<TablaAsmeReproduccion> GetAll();

        List<TablaAsmeReproduccion> GetAll(long TablaAsmeId);
        List<AnexoIdentificable> GetAllAnexoIdentificable(long TablaAsmeId);
        List<AnexoNoIdentificable> GetAllAnexoNoIdentificable(long TablaAsmeId);
        List<AnexoPersonal> GetAllAnexoPersonal(long TablaAsmeId);
        List<Actividad> GetAllActividad(long TablaAsmeId);
        void Save(TablaAsmeReproduccion obj);

        void Eliminar(long ReproduccionId);
        void EliminarAnexoIdentificable(long AnexoIdentificableId);
        void EliminarAnexoNoIdentificable(long AnexoNoIdentificableId);

        void EliminarAnexoPersonal(long AnexoPersonalId);
        void EliminarActividad(long ActividadId);
    }
}
