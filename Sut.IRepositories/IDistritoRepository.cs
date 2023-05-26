using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IDistritoRepository : IBaseRepository<Distrito>
    {
        List<Distrito> GetByProvincia(long ProvinciaId);
        Distrito GetOne(long DistritoId);
        List<Distrito> GetByParent(long PadreId);
        List<Distrito> GetByLista();
        void Save(Distrito obj);
        void Eliminar(long DistritoId);
    }
}
