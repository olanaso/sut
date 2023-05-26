using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class Observacion
    {
        [Key]
        public long ObservacionId { get; set; }  
        public string Descripcion { get; set; }   
        public string CodValidacion { get; set; } 
        public string Estado { get; set; }

        public Expediente Expediente { get; set; }
        public long ExpedienteId { get; set; }

        public Procedimiento Procedimiento  { get; set; }
        public long ProcedimientoId { get; set; }
        public long RequisitoId { get; set; }
        public long TablaAsmeId { get; set; }
        public long ActividadId { get; set; }
        
        public long BaseLegalId { get; set; }
        public long BaseLegalNormaId { get; set; }
        public long RecursoCostoId { get; set; }
        public long RecursoId { get; set; }        
        public Entidad Entidad { get; set; }
        public long EntidadId { get; set; }
        public Int32 CodPadre { get; set; }
        public Int32 CodHijo { get; set; }
        public string TipoEstado { get; set; }
        public string NombreCampo { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModifica { get; set; }
        public string Pantalla { get; set; }
        
        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }


        [NotMapped]
        public string EntidadExpediente { get; set; }
        [NotMapped]
        public string DiasExpediente { get; set; }
        [NotMapped]
        public string UltimaRevision { get; set; }

    }
}
