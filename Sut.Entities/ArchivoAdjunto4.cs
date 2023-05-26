using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class ArchivoAdjunto4
    {
        [Key]
        public long Archivo_Fec_Emision_Licencia_Funcionamiento { get; set; }
        
        public string Ruta { get; set; }
        public string NombreArchivo { get; set; }
        public string Extension { get; set; }
        public string Tipo { get; set; }
        public decimal Tamano { get; set; }

        public string Descripcion { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
        [NotMapped]
        public long Archivo_Fec_Ing_Sol { get; set; }


    }
}
