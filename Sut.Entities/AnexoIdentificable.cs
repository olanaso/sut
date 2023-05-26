using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class AnexoIdentificable
    {
        [Key, Column(Order = 0)]
        public long AnexoIdentificableId { get; set; }

        [Key, Column(Order = 1), ForeignKey("Expediente")]
        public long ExpedienteId { get; set; }
        public Expediente Expediente { get; set; }

        [ForeignKey("TablaAsme")]
        public long TablaAsmeId { get; set; }
        public TablaAsme TablaAsme { get; set; }

        public TipoRecurso TipoRecurso { get; set; }
        public string TipoRecursoNom { get; set; }
        public string UnidadOrganica { get; set; }
        public int Nro { get; set; }
        public string Actividad { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string UnidadMedida { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal CostoTotal { get; set; }
    }
}
