using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class ArMotivoAdjunto
    {
        [Key]
        public long ArMotivoAdjuntoId { get; set; }



        [ForeignKey("Expediente")]
        public long ExpedienteId { get; set; }
        public Expediente Expediente { get; set; }

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


        public int Activo { get; set; }


        public long NormaId { get; set; }
    }
}
