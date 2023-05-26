using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class ArchivoAdjunto6
    {
        [Key]
        public long Archivo_Fec_Suspencion_ITSE_Subsanacion { get; set; }
        
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


    }
}
