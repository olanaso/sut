using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class VCalidadRequisitos_CABECERA
    {
        [Key] 
        public int ICODCALIDAD { get; set; } 
        public string P5_4_REQUISITOS { get; set; }
        public string CodEntidad { get; set; }

    }
}
