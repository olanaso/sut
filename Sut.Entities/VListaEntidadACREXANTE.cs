using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class VListaEntidadACREXANTE
    {
        [Key]
        public string Codigo { get; set; }
        public string nombre { get; set; }
        public string Acronimo { get; set; }
        public int ActivarACR { get; set; } 
        public long EntidadId { get; set; }
        public int Ejecucion { get; set; }

        


    }
}
