using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IApplicationServices
{
    public interface IPlazoAtencionService
    {
        PlazoAtencion GetOne(long PlazoAtencionId);
        List<PlazoAtencion> GetAll();

        List<PlazoAtencion> GetAll(long TablaAsmeId);
        void Save(PlazoAtencion obj);
        void Eliminar(long ReproduccionId);
    }
}
