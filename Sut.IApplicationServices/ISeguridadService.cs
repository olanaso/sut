using Sut.Entities;

namespace Sut.IApplicationServices
{
    public interface ISeguridadService
    {
        bool ValidarUsuario(string tipo, string usuario, string clave);
        Usuario GetUsuario(string tipo, string usuario);
    }
}
