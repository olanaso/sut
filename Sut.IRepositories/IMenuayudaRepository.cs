using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IMenuayudaRepository : IBaseRepository<Menuayuda>
    {
        
        new void Save(Menuayuda obj);
        List<Menuayuda> GetByMenuayuda(long EntidadId, int Ruta, int menuid);
         
        List<Menuayuda> GetByEntidad(long EntidadId);        
        Menuayuda GetByoneid( long EntidadId);
        Menuayuda GetByone(int menuid, long EntidadId);


    }
}
