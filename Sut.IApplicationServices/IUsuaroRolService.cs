using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IUsuarioRolService
    {
       
        void Save(UsuarioRol obj);
        void Delete(long UsuarioRolId);

        List<UsuarioRol> GetByUsuarioRol(long UsuarioId);
        List<UsuarioRol> GetByEntidad(long UsuarioId);
        UsuarioRol GetByone(long UsuarioId);


    }
}
