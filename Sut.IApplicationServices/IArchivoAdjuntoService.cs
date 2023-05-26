using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IArchivoAdjuntoService
    {
        ArchivoAdjunto GetOne(long ArchivoAdjuntoId);
        void Save(ArchivoAdjunto obj);
    }
}
