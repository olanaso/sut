using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class PasoSeguir
    {
        [Key, Column(Order = 0), ForeignKey("Procedimiento")]
        public long ProcedimientoId { get; set; }
        public Procedimiento Procedimiento { get; set; }

        [Key, Column(Order = 1)]
        public long PasoSeguirId { get; set; }
        
        public string Descripcion { get; set; }
    }
}
