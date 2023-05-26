using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IDocumentosNormaService
    {
        DocumentosNorma GetOne(long DocumentosNormaid);
        DocumentosNorma LsitaGetOne();
        List<DocumentosNorma> GetAllLikePagin(DocumentosNorma filtro, int pageIndex, int pageSize, ref int totalRows);
        List<DocumentosNorma> GetAllLikePaginAdmin(DocumentosNorma filtro, int pageIndex, int pageSize, ref int totalRows);

        List<DocumentosNorma> GetAll();

        void Save(DocumentosNorma obj);
 
    }
}
