using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class MenuPrincipal
    {
        [Key]
        public long MenuId { get; set; }
 
        public string Descripcion { get; set; }
        public string Estilo { get; set; }
        public int Estado { get; set; }
        public int IdPadreMenu { get; set; }
        public string Gragico { get; set; }
        public string Dimension { get; set; } 
        public string url { get; set; }


    }
}
