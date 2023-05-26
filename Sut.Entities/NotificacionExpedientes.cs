using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class NotificacionExpedientes
    {
        [Key]
        public long NotificacionExpedientesId { get; set; }

        [Required, ForeignKey("Expediente")]
        public long ExpedienteId { get; set; }
        public Expediente Expediente { get; set; } 

        [Required, ForeignKey("Entidad")]
        public long EntidadId { get; set; }
        public Entidad Entidad { get; set; }
        public long SectorId { get; set; }
        public long? ProvinciaId { get; set; }
        public DateTime? FecEnvio { get; set; }
        public int DiasTranscurrido { get; set; }
        public DateTime? FecUltimaRevision { get; set; }
        public int EstadoExpediente { get; set; }
        public int Tomar { get; set; } 
        public int RevEstado { get; set; }
        public int RevInfCdno { get; set; }
        public int RevDatosGenerales { get; set; }
        public int RevSutentoTecnico { get; set; }
        public int RevTablaAsme { get; set; }
        public int RevSustentoCosto { get; set; }
        public int Estado { get; set; }
        public int Nivel { get; set; }
        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }


         
 

    }
}
