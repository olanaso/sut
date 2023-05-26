using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IUsuarioRolRepository : IBaseRepository<UsuarioRol>
    {
        
        new void Save(UsuarioRol obj); 
        List<UsuarioRol> GetByUsuarioRol(long UsuarioId);
        List<UsuarioRol> GetByEntidad(long EntidadId);

        UsuarioRol GetByone(long UsuarioId);

        
    }
}
