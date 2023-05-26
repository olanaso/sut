using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IArMotivoAdjuntoRepository : IBaseRepository<ArMotivoAdjunto>
    {
        ArMotivoAdjunto GetOne(long ArMotivoAdjuntoId);
        void Save(ArMotivoAdjunto obj);
        void modificar(ArMotivoAdjunto obj);
        
        List<ArMotivoAdjunto> GetAll(long ArMotivoAdjuntoId); 

        List<ArMotivoAdjunto> GetAllxid(long ExpedienteId, long NormaId);

        List<ArMotivoAdjunto> GetAllLikePaginEliminar(long ExpedienteId);

        void Eliminar(long ArMotivoAdjuntoId);
    }
}
