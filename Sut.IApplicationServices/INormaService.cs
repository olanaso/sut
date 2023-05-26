using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IApplicationServices
{
    public interface INormaService
    {
        Norma GetOne(long NormaId);
        List<Norma> GetAll();
        List<Norma> GetAllLikePagin(Norma filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(Norma obj);
    }
}
