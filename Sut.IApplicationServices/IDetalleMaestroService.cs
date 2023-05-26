using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IDetalleMaestroService
    {
        DetalleMaestro GetOne(long DetalleMaestroId);
        List<DetalleMaestro> GetAllXTIPO(TipoDetalleMaestro tipo); 
        void Save(DetalleMaestro obj);
        void Eliminar(long idDetalleMaestro);
    }
}
