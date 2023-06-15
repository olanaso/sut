using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class Usuario
    {

        [NotMapped]
        public int iosp;

        [Key]
        public long UsuarioId { get; set; }

        [ForeignKey("Entidad")]
        public long EntidadId { get; set; }
        public Entidad Entidad { get; set; }

        public string NroDocumento { get; set; }
        public string Clave { get; set; }

        public Rol Rol { get; set; }

        [NotMapped]
        public int RolId;
        public EstadoUsuario Estado { get; set; }

        [ForeignKey("MiembroEquipo")]
        public long MiembroEquipoId { get; set; }
        public MiembroEquipo MiembroEquipo { get; set; }
        
        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        public int Ayuda { get; set; }
        public int ActivarCorreo { get; set; }

      
       

        public AsignarEntidades AsigEntidad { get; set; }
         

        [NotMapped]
        public short FilterRol { get; set; }

        [NotMapped]
        public long usurol { get; set; }



        [NotMapped]
        public long EstadoId { get; set; }
        [NotMapped]
        public string FecCreaciontexto { get; set; }


        [NotMapped]
        public long DepartamentoId { get; set; }

        [NotMapped]
        public long? ProvinciaId { get; set; }

        [NotMapped]
        public int chekActivar { get; set; }

        public DateTime? FechaSolicitud { get; set; }
        

    }
}
