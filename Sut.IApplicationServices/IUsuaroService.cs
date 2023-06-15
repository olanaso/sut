using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IUsuarioService
    {
        Usuario Validar(string NroDocumento, string Clave, int TipoDoc);
        List<Usuario> GetAllLikePagin(Usuario filtro, int pageIndex, int pageSize, ref int totalRows);
        Usuario GetOne(long UsuarioId);
        Usuario GetOnecompleto(long UsuarioId);
        Usuario GetOne(string NroDocumento);
        Usuario GetOnerol(string NroDocumento, Rol rol);
        void Save(Usuario obj);
        void Delete(long UsuarioId);
        List<Usuario> GetByEntidad(long EntidadId);
        List<UsuarioRol> GetByEntidadROL(long EntidadId);

        void SaveRoles(Usuario obj);
    }
}
