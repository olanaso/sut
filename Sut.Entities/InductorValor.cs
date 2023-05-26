using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class InductorValor
    {
        [Key]
        public long InductorValorId { get; set; }

        [ForeignKey("Expediente")]
        public long ExpedienteId { get; set; }
        public Expediente Expediente { get; set; }

        [ForeignKey("Inductor")]
        public long InductorId { get; set; }
        public Inductor Inductor { get; set; }

        [ForeignKey("UnidadOrganica")]
        public long UnidadOrganicaId { get; set; }
        public UnidadOrganica UnidadOrganica { get; set; }

        public decimal Valor { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
    }
}
