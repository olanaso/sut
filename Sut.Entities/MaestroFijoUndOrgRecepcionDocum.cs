using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class MaestroFijoUndOrgRecepcionDocum
    {
        [Key]
        public int MFUndOrgRecepcionDocumentosId { get; set; }

        [ForeignKey("MaestroFijo")]
        public long MaestroFijoId { get; set; }
        public MaestroFijo MaestroFijo { get; set; }

        [ForeignKey("UnidadOrganica")]
        public long UnidadOrganicaId { get; set; }
        public UnidadOrganica UnidadOrganica { get; set; }

        [ForeignKey("Sede")]
        public long SedeId { get; set; }
        public Sede Sede { get; set; }
    }
}
