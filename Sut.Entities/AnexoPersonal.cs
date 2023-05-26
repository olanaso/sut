using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class AnexoPersonal
    {
        [Key, Column(Order = 0)]
        public long AnexoPersonalId { get; set; }

        [Key, Column(Order = 1), ForeignKey("Expediente")]
        public long ExpedienteId { get; set; }
        public Expediente Expediente { get; set; }

        [ForeignKey("TablaAsme")]
        public long TablaAsmeId { get; set; }
        public TablaAsme TablaAsme { get; set; }
        
        public string UnidadOrganica { get; set; }
        public int Nro { get; set; }
        public string Actividad { get; set; }
        public string Cargo { get; set; }
        public string EscalaIngreso { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Duracion { get; set; }
        public decimal DuracionTotal { get; set; }
        public decimal CostoMinuto { get; set; }
        public decimal CostoTotal { get; set; }

    }
}
