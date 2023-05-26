using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IInformeRepository : IBaseRepository<Informe>
    {
        Informe GetOne(long Informeid);
        List<Informe> listaGetOne(long Informeid);
        Informe LsitaGetOne();
        List<Informe> GetAll();
        void Save(Informe obj);
      
    }
}
