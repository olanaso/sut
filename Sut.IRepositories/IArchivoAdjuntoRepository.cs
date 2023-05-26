using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IArchivoAdjuntoRepository : IBaseRepository<ArchivoAdjunto>
    {
        ArchivoAdjunto GetOne(long ArchivoAdjuntoId);
        void Save(ArchivoAdjunto obj);
    }
}
