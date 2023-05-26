using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class Provincia
    {
        [Key]
        public long ProvinciaId { get; set; }
        //[Required]
        public string Nombre { get; set; }

        [Required, ForeignKey("Departamento")]
        public long DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
    }
}
