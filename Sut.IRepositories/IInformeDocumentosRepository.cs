using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IInformeDocumentosRepository : IBaseRepository<InformeDocumentos>
    {
        InformeDocumentos GetOne(long InformeDocumentosid);
        InformeDocumentos LsitaGetOne();
        List<InformeDocumentos> GetAll();
        void Save(InformeDocumentos obj);
      
    }
}
