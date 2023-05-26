using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class MaestroFijoSede
    {
        [Key, Column(Order = 0), ForeignKey("MaestroFijo"), Required]
        public long MaestroFijoId { get; set; }
        public MaestroFijo MaestroFijo { get; set; }

        [Key, Column(Order = 1), ForeignKey("Sede"), Required]
        public long SedeId { get; set; }
        public Sede Sede { get; set; }

        [NotMapped]
        public bool Checked { get; set; }

        public bool? Mostrar { get; set; }

        public List<MaestroFijoUndOrgRecepcionDocum> MaestroFijoUndOrgRecepcionDocum { get; set; }

    }
}
