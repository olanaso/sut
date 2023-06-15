using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        
        Usuario GetOne2(string NroDocumento, int TipoDoc);
        Usuario GetOne(string NroDocumento);
        Usuario GetOnerol(string NroDocumento,Rol rol);
        Usuario GetOne(long UsuarioId);
        Usuario GetOnecompleto(long UsuarioId);
        
        new void Save(Usuario obj);
        new void SaveRoles(Usuario obj);
        new List<Usuario> GetAll();
        List<Usuario> GetByEntidad(long EntidadId);
        List<UsuarioRol> GetByEntidadROL(long EntidadId);
    }
}
