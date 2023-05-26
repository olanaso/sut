using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class UnidadMedida
    {
        [Key]
        public long UnidadMedidaId { get; set; }

        [MaxLength(10)]
        public string Abreviatura { get; set; }

        [MaxLength(20)]
        public string Nombre { get; set; }

        public bool Activo { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        public UnidadMedida()
        {
            Activo = true;
        }
    }
}
