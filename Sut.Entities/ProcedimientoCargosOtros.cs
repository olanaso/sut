using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class ProcedimientoCargosOtros
    {
        [Key]
        public long ProcedimientoCargosOtrosID { get; set; }
        public long ProcedimientoId { get; set; }
        public string CargoOtros { get; set; } 
        public long UndOrgIdOtros { get; set; } 
        public int PzoPresentOtros { get; set; }
        public int PzoResolOtros { get; set; }
        public int ordenotros { get; set; }

        public TipoPlazo TipoOtrosPresent { get; set; }
        public TipoPlazo TipoOtrosResol { get; set; }

    }
}
