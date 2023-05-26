using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class ProcedimientoSede
    {
        [Key, Column(Order = 0), ForeignKey("Procedimiento"), Required]
        public long ProcedimientoId { get; set; }
        public Procedimiento Procedimiento { get; set; }

        [Key, Column(Order = 1), ForeignKey("Sede"), Required]
        public long SedeId { get; set; }
        public Sede Sede { get; set; }



        public List<UndOrgRecepcionDocumentos> UndOrgRecepcionDocumentos { get; set; }
    }
}
