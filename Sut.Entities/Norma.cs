using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class Norma
    {
        [Key]
        public long NormaId { get; set; }

        [ForeignKey("TipoNorma")]
        public long TipoNormaId { get; set; }
        public MetaDato TipoNorma { get; set; }

        [MaxLength(20)]
        public string Numero { get; set; }

        [MaxLength(200)]
        public string Descripcion { get; set; }

        public DateTime? FechaPublicacion { get; set; }
        public string Sector { get; set; }

        [ForeignKey("ArchivoAdjunto")]
        public long? ArchivoAdjuntoId { get; set; }
        public ArchivoAdjunto ArchivoAdjunto { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
    }
}
