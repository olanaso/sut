using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class MaestroFijo
    {
        [Key]
        public long MaestroFijoId { get; set; }

        [Required, ForeignKey("Entidad")]
        public long EntidadId { get; set; }
        public Entidad Entidad { get; set; }

        public string Telefono { get; set; }
        public string Anexo { get; set; }
        public string Correo { get; set; }
        public bool InfPredeterminadaTerminado { get; set; }

        public List<MaestroFijoDatoAdicional> MaestroFijoDatoAdicional { get; set; }
        public List<MaestroFijoSede> MaestroFijoSede { get; set; }
        public List<MaestroFijoPasoSeguir> MaestroFijoPasoSeguir { get; set; }
        public List<MaestroFijoNotaCiudadano> MaestroFijoNotaCiudadano { get; set; }

    }
}
