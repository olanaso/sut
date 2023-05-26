using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IDistritoService
    {
        List<Distrito> GetByProvincia(long ProvinciaId);
        Distrito GetOne(long DistritoId);
        List<Distrito> GetByParent(long PadreId);

        List<Distrito> GetByLista();
        List<Distrito> GetAllLikePagin(Distrito filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(Distrito obj);
        void Eliminar(long DistritoId);
    }
}
