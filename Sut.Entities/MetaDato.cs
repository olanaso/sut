using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class MetaDato
    {
        [Key]
        public long MetaDatoId { get; set; }

        [ForeignKey("Padre")]
        public long? PadreId { get; set; }
        public MetaDato Padre { get; set; }

        [MaxLength(20)]
        public string Codigo { get; set; }
        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(50)]
        public string Valor01 { get; set; }
        public decimal Valor02 { get; set; }


        public TipoRegla TipoRegla { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        public List<MetaDato> MetaDatos { get; set; }
        public int? Orden { get; set; }
    }
}
