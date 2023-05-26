using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class MiembroEquipo
    {
        [Key]
        public long MiembroEquipoId { get; set; }

        [ForeignKey("Entidad")]
        public long EntidadId { get; set; }
        public Entidad Entidad { get; set; }

        [ForeignKey("ArchivoAdjunto")]
        public long? ArchivoAdjuntoId { get; set; }
        public ArchivoAdjunto ArchivoAdjunto { get; set; }


        //[StringLength(8)]
        public string NroDocumento { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string Nombres { get; set; }

        [ForeignKey("RolEquipo")]
        public long? RolEquipoId { get; set; }
        public MetaDato RolEquipo { get; set; }

        public string Cargo { get; set; }

        [ForeignKey("UnidadOrganica")]
        public long? UnidadOrganicaId { get; set; }
        public UnidadOrganica UnidadOrganica { get; set; }

        [EmailAddress]
        public string Correo { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }
        public int Sexo { get; set; }

        public int TipoDoc { get; set; }

        public EstadoMiembroEquipo Estado { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }


        [NotMapped]
        public string NombreCompleto
        {
            get
            {
                return string.Format("{0} {1}, {2}", ApePaterno, ApeMaterno, Nombres);
            }
        }

        /*FVN*/
        [NotMapped]
        public string NombreCompletoBuscar { get; set; }

        [NotMapped]
        public long EstadoId { get; set; }
        [NotMapped]
        public string FecCreaciontexto { get; set; }

        [NotMapped]
        public long DepartamentoId { get; set; }

        [NotMapped]
        public long? ProvinciaId { get; set; }
    }
}
