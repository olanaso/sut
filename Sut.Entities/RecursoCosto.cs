using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class RecursoCosto
    {
        [Key]
        public long RecursoCostoId { get; set; }

        [ForeignKey("Expediente")]
        public long ExpedienteId { get; set; }
        public Expediente Expediente { get; set; }

        [ForeignKey("Recurso")]
        public long RecursoId { get; set; }
        public Recurso Recurso { get; set; }

        [ForeignKey("UnidadOrganica")]
        public long? UnidadOrganicaId { get; set; }
        public UnidadOrganica UnidadOrganica { get; set; }

        [ForeignKey("Inductor")]
        public long? InductorId { get; set; }
        public Inductor Inductor { get; set; }

        public string DocSustento { get; set; }

        public decimal Cantidad { get; set; }
        public decimal CostoAdquisicion { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal CostoAnual { get; set; }
        public decimal EscalaIngreso { get; set; }       

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
    }
}
