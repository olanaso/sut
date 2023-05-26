using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sut.Entities;

namespace Sut.Security
{
    public class UsuarioInfo
    {
        public long UsuarioId { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; }

        public string Cargo { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string Nombres { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }


        public int Sexo { get; set; }

        public long EntidadId { get; set; }
        public string CodEntidad { get; set; }
        public string NomEntidad { get; set; }
        public short TipoTupa { get; set; }

        public short Rol { get; set; }
        public string NomRol { get; set; }
        public short Sector { get; set; }
        public short Provincia { get; set; } 
        public string CodPadre { get; set; } 
        public short EstadoMinistrio { get; set; }
        public short Estadoprovincia { get; set; }


        public int MultipleRol { get; set; }

        public int validrol { get; set; }
        

        public int Ayuda { get; set; }


        public int tiporegla { get; set; }

        public int DJ { get; set; }

        public List<Menu> menu { get; set; }


        public List<RolMenu> rolmenu { get; set; }

        public int procesogratuito { get; set; }

        public int ActivarAlgoritmo { get; set; }
        public int ActivarMultiUsuario { get; set; }
        public int ActivarExpediente { get; set; }


        public string Bandeja { get; set; }


        public int TipoEnlace { get; set; }

    }

   
}
