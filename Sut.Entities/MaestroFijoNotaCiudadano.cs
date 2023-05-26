using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class MaestroFijoNotaCiudadano
    {

        [Key, Column(Order = 0), ForeignKey("MaestroFijo")]
        public long MaestroFijoId { get; set; }
        public MaestroFijo MaestroFijo { get; set; }

        [Key, Column(Order = 1)]
        public long NotaCiudadanoId { get; set; }
        [NotMapped]
        public Dato MetaDato { get; set; }

        public string Comentario { get; set; }

        public int MetaDatoTipoNotaId { get; set; }
        public int Orden { get; set; }


    }
}
