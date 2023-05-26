using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class Dato
    {
        [Key]
        public long DatoId { get; set; }

        public TipoDato Tipo { get; set; }
        public long MetaDatoId { get; set; }

        [MaxLength(20)]
        public string Codigo { get; set; }
        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string Texto01 { get; set; }
        public decimal Decimal01 { get; set; }
        public int Entero01 { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        [NotMapped]
        public Dato Padre { get; set; }
        /*Esta variable solo servirar para reconocer el canal de atencion "Oficinas de entidad" para su autocompletado en caso 
 su informacion predeterminada no lo tenga
     */
        [NotMapped]
        public int OficinasAutocompletar { get; set; }

        [NotMapped]
        public long idpro { get; set; }
        [NotMapped]
        public long idex { get; set; }
    }
}
