using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class ProcedimientoDatoAdicional
    {
        [Key, Column(Order = 0), ForeignKey("Procedimiento")]
        public long ProcedimientoId { get; set; }
        public Procedimiento Procedimiento { get; set; }

        [Key, Column(Order = 1)]
        public long MetaDatoId { get; set; }

        public string Comentario { get; set; }


        [NotMapped]
        public Dato MetaDato { get; set; }
        
        public bool Checked { get; set; }
        [NotMapped]
        public int Tipo { get; set; }


        public ProcedimientoDatoAdicional() {
            this.Checked = false;
        }
    }
}
