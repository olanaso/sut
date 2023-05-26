using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class ProcedimientoCargosApe
    {
        [Key]
        public long ProcedimientoCargosApeID { get; set; }
        public long ProcedimientoId { get; set; }
        public string CargoApe { get; set; } 
        public long UndOrgIdApe { get; set; } 
        public int PzoPresentApe { get; set; }
        public int PzoResolApe { get; set; }
        public int ordenape { get; set; }        
        public TipoPlazo TipoApelPresent { get; set; }
        public TipoPlazo TipoApelResol { get; set; }

    }
}
