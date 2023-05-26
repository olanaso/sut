using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class InformeDocumentos
    {
        [Key]
        public long InformeDocumentosID { get; set; }

        [ForeignKey("Informe")]
        public long InformeId { get; set; }
        public Informe Informe { get; set; } 
         
        public Int32 Tipoid { get; set; }
        public Int32 E { get; set; }
        public Int32 NO { get; set; } 
        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

    }
}
