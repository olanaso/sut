using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class FactorDedicacion
    {
        [Key]
        public long FactorDedicacionId { get; set; }

        [ForeignKey("Expediente")]
        public long ExpedienteId { get; set; }
        public Expediente Expediente { get; set; }

        [ForeignKey("UnidadOrganica")]
        public long UnidadOrganicaId { get; set; }
        public UnidadOrganica UnidadOrganica { get; set; }

        [ForeignKey("Recurso")]
        public long RecursoId { get; set; }
        public Recurso Recurso { get; set; }

        public decimal ValorTupa { get; set; }
        public decimal ValorNoTupa { get; set; }
        public bool AutoCalculo { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        public FactorDedicacion()
        {
            AutoCalculo = false;
        }


        [NotMapped]
        public string Unidadorganicanombre { get; set; }
        //[NotMapped]
        //public string Unidadorganicaid { get; set; }
        [NotMapped]
        public string Codigo { get; set; }
        [NotMapped]
        public string Procemiento { get; set; }
        [NotMapped]
        public string Orden { get; set; }
        [NotMapped]
        public string Actividad { get; set; }
        [NotMapped]
        public string Duracion { get; set; }
        [NotMapped]
        public string Prestaciones { get; set; }
        [NotMapped]
        public string Tipoactividad { get; set; }
        [NotMapped]
        public string Tiporecurso { get; set; }
        [NotMapped]
        public string Recursos { get; set; }
        [NotMapped]
        public string Cantidad { get; set; }
        //[NotMapped]
        //public string Expedienteid { get; set; }
        [NotMapped]
        public string Modalidad { get; set; }
    }
}
