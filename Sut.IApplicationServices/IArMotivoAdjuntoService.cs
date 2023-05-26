using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IArMotivoAdjuntoService
    {
        ArMotivoAdjunto GetOne(long ArMotivoAdjuntoId);

        List<ArMotivoAdjunto> GetAllLikePagin(ArMotivoAdjunto filtro, int pageIndex, int pageSize, ref int totalRows);

        List<ArMotivoAdjunto> GetAllLikePaginEliminar(long ExpedienteId);
        void Eliminar(long ArMotivoAdjuntoId);
        void Save(ArMotivoAdjunto obj);
        void modificar(ArMotivoAdjunto obj);
    }
}
