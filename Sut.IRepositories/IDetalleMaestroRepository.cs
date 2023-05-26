using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface IDetalleMaestroRepository : IBaseRepository<DetalleMaestro>
    {
        DetalleMaestro GetOne(long DetalleMaestroId);
        List<DetalleMaestro> GetAllXTIPO(TipoDetalleMaestro tipo);
     
        void Save(DetalleMaestro obj);
        void Eliminar(long IdDetalleMaestro); 
    }
}
