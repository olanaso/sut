using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class ProcedimientoCategoria
    {
        [Key]
        public long ProcedimientoCategoriaID { get; set; }

        public long ProcedimientoId { get; set; }
        public long ExpedienteId { get; set; }
        public string CategoriaProcedimientoId { get; set; }
         

    }
}
