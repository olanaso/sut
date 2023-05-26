using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IInformeDocumentosService
    {
        InformeDocumentos GetOne(long InformeDocumentosid);
        InformeDocumentos LsitaGetOne();
        List<InformeDocumentos> GetAllLikePagin(InformeDocumentos filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(InformeDocumentos obj);
 
    }
}
