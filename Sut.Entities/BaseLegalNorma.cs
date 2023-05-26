using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class BaseLegalNorma
    {
        [Key, Column(Order = 0), ForeignKey("BaseLegal")]
        public long BaseLegalId { get; set; }
        public BaseLegal BaseLegal { get; set; }

        [Key, Column(Order = 1)]
        public long BaseLegalNormaId { get; set; }

        public TipoBaseLegal TipoBaseLegal { get; set; }

        [ForeignKey("Norma")]
        public long? NormaId { get; set; }
        public Norma Norma { get; set; }
        
        [ForeignKey("TipoNorma")]
        public long? TipoNormaId { get; set; }
        public MetaDato TipoNorma { get; set; }

        [MaxLength(4000)]
        public string Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string Sector { get; set; }
         
        public string Articulo { get; set; }
        public string Numero { get; set; }
        public string Url { get; set; }

        public string EstadoACR { get; set; }

        public string DescripcionACR { get; set; }

        [ForeignKey("ArchivoAdjunto")]
        public long? ArchivoAdjuntoId { get; set; }
        public ArchivoAdjunto ArchivoAdjunto { get; set; }

        [NotMapped]
        public long MigracionEntidad { get; set; }
        

    }
}
