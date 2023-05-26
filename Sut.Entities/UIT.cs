using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class UIT
    {
        [Key]
        public long UITID { get; set; } 
        
        public string Descripcion { get; set; } 
        public decimal Monto { get; set; } 
        public string Estado { get; set; } 
        public DateTime Fecha { get; set; }
        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

    }
}
