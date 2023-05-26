using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class VCalidadRequisitos_4_2
    {
        [Key]
        public int ICODREQUISITO { get; set; }  
        public int ICODCALIDAD { get; set; } 
        public string VREQUISITO { get; set; } 
        public string VNORMA { get; set; }
        public string VRESPUESTA { get; set; }
        public string CodEntidad { get; set; }
        

    }
}
