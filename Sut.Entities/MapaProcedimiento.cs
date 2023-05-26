using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class MapaProcedimiento
    {
        [Key]
        public long MapaProcedimientoId { get; set; }
        public long PadreId { get; set; }
        [Required, MaxLength(20)]
        public string Codigo { get; set; }
        [Required, MaxLength(250)]
        public string Nombre { get; set; }

        public TipoMapaProcedimiento Tipo { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        [NotMapped]
        public short FilterTipo { get; set; }
    }
}
