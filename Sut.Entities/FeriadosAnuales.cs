using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class FeriadosAnuales
    {
        [Key]
        public long FeriadosAnualesId { get; set; } 
        public DateTime? Fecha { get; set; }

        public int Anio { get; set; }
        public int Estado { get; set; } 
        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
          
    }
}
