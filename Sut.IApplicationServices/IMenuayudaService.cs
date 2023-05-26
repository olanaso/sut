using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IMenuayudaService
    {
       
        void Save(Menuayuda obj);
        void Delete(long MenuayudaId);

        List<Menuayuda> GetByMenuayuda(long EntidadId, int Ruta, int menuid);
         

        List<Menuayuda> GetByEntidad(long UsuarioId);
        Menuayuda GetByone(int menuid, long EntidadId);
        Menuayuda GetByoneid(long EntidadId);


    }
}
