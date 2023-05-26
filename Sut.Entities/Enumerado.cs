using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class Enumerado
    {
        [Key]
        public int ID_ENUMERADO { get; set; }

        public int ID_TIPO_ENUMERADO { get; set; }

        public string CODIGO { get; set; }

        public string VALOR { get; set; }

        public int ORDEN { get; set; }

        public bool ES_BORRADO { get; set; }

        public bool ES_ACTIVO { get; set; }
    }
}
