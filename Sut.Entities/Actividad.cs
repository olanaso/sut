using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class Actividad
    {
        [Key]
        public long ActividadId { get; set; }

        [ForeignKey("TablaAsme")]
        public long TablaAsmeId { get; set; }
        public TablaAsme TablaAsme { get; set; }

        public int? Orden { get; set; }

        [ForeignKey("UnidadOrganica")]
        public long UnidadOrganicaId { get; set; }
        public UnidadOrganica UnidadOrganica { get; set; }

        //[MaxLength(500)]
        public string Descripcion { get; set; }
        public decimal Duracion { get; set; }
        public TipoActividad TipoActividad { get; set; }
        public TipoValor TipoValor { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        public List<ActividadRecurso> ActividadRecurso { get; set; }
         

        [NotMapped]
        public string NomUnidadOrganica { get; set; }
        [NotMapped]
        public string NomTipoActividad { get; set; }
        [NotMapped]
        public string NomTipoValor { get; set; }


    }
}
