using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class Recurso
    {
        [Key]
        public long RecursoId { get; set; }

        [Required, ForeignKey("Entidad")]
        public long EntidadId { get; set; }
        public Entidad Entidad { get; set; }

        [MaxLength(20)]
        public string Codigo { get; set; }
        //[MaxLength(80)]
        public string Nombre { get; set; }

        [Required]
        public TipoRecurso TipoRecurso { get; set; }

        [ForeignKey("TipoDepreciacion")]
        public long? TipoDepreciacionId { get; set; }
        public MetaDato TipoDepreciacion { get; set; }

        [ForeignKey("UnidadMedida")]
        public long? UnidadMedidaId { get; set; }
        public UnidadMedida UnidadMedida { get; set; }

        public bool Activo { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        public Recurso()
        {
            Activo = true;
            RecursoCosto = new List<RecursoCosto>();
        }

        public List<RecursoCosto> RecursoCosto { get; set; }

        [NotMapped]
        public decimal DuracionTotal { get; set; }

        [NotMapped]
        public string[] listaNombre { get; set; }
        public long[] listaUnidadMedidaId { get; set; }
        public long[] listaTipoDepreciacionId { get; set; }

        public string[] chkbuscarUndOrg { get; set; } 

        public string[] listaNombreCargado { get; set; }

        public long[] listaUnidadMedidaIdCargado { get; set; }
        public long[] listaTipoDepreciacionIdCargado { get; set; }
    }
}
