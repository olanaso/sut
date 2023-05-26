using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class IngresoSUT
    {
        [Key]
        public long IngresoID { get; set; }
        public string Nombre { get; set; }
        public long Usuarioid { get; set; } 
        public DateTime? FechaHoraEntrada { get; set; }

    }
}
