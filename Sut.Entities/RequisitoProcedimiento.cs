using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class RequisitoProcedimiento
    {
        [Key]
        public long RequisitoProcedimientoId { get; set; }

        [ForeignKey("Requisito")]
        public long RequisitoId { get; set; }
        public Requisito Requisito { get; set; }

        [Required]
        public string CodProcedimiento { get; set; }
    }
}
