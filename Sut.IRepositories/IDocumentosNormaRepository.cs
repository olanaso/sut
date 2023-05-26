using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IDocumentosNormaRepository : IBaseRepository<DocumentosNorma>
    {
        DocumentosNorma GetOne(long DocumentosNormaid);
        DocumentosNorma LsitaGetOne();
        List<DocumentosNorma> GetAll();
        List<DocumentosNorma> GetAllAdmin();
        void Save(DocumentosNorma obj);
      
    }
}
