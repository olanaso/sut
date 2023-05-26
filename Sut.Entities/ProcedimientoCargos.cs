using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class ProcedimientoCargos
    {
        [Key]
        public long ProcedimientoCargosID { get; set; }
        public long ProcedimientoId { get; set; }
        public string Cargo { get; set; } 
        public long UndOrgId { get; set; } 
        public int PzoPresent { get; set; }
        public int PzoResol { get; set; }
        public int Tipo { get; set; }
        public int orden { get; set; }
        public TipoPlazo TipoReconPresent { get; set; }
        public TipoPlazo TipoReconResol { get; set; }

    }
}
