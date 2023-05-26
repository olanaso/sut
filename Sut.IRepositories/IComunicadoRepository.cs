using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IComunicadoRepository : IBaseRepository<Comunicado>
    {
        Comunicado GetOne(long Comunicadoid);
        Comunicado LsitaGetOne();
        List<Comunicado> GetAll();
        List<Comunicado> GetAllBaner();
        void Save(Comunicado obj);
      
    }
}
