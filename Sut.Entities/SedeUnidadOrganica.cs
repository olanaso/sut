using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class SedeUnidadOrganica
    {
        [Key, Column(Order = 0), ForeignKey("Sede")]
        public long SedeId { get; set; }
        public Sede Sede { get; set; }

        [Key, Column(Order = 1), ForeignKey("UnidadOrganica")]
        public long UnidadOrganicaId { get; set; }
        public UnidadOrganica UnidadOrganica { get; set; }
    }
}
