using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface IPlazoAtencionRepository : IBaseRepository<PlazoAtencion>
    {
        PlazoAtencion GetOne(long PlazoAtencionId);
        List<PlazoAtencion> GetAll();

        List<PlazoAtencion> GetAll(long TablaAsmeId);
        void Save(PlazoAtencion obj);

        void Eliminar(long ReproduccionId);
    }
}
