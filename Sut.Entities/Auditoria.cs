using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class Auditoria
    {
        [Key]
        public long AuditoriaID { get; set; } 
        [ForeignKey("Entidad")]
        public long EntidadId { get; set; }
        public Entidad Entidad { get; set; } 
        public long SectorId { get; set; }
        public long ProvinciaId { get; set; } 
        public string Usuario { get; set; }
        public string Actividad { get; set; } 
        public string Pantalla { get; set; }  
        public string UserCreacion { get; set; } 
        public DateTime? FecCreacion { get; set; }

        [NotMapped]
        public string FecCreaciontexto { get; set; }
   
        public string NombreEntidad { get; set; }

        [NotMapped]
        public string Campo1 { get; set; }
        [NotMapped]
        public string Campo2 { get; set; }
        [NotMapped]
        public string Campo3 { get; set; }
        [NotMapped]
        public string Campo4 { get; set; }
        [NotMapped]
        public string Campo5 { get; set; }
        [NotMapped]
        public string Campo6 { get; set; }
        [NotMapped]
        public string Campo7 { get; set; }
        [NotMapped]
        public string Campo8 { get; set; }
        [NotMapped]
        public string Campo9 { get; set; }
        [NotMapped]
        public string Campo10 { get; set; }
        [NotMapped]
        public string Campo11 { get; set; }
        [NotMapped]
        public string Campo12 { get; set; }
        [NotMapped]
        public string Campo13 { get; set; }
        [NotMapped]
        public string Campo14 { get; set; }
        [NotMapped]
        public string Campo15 { get; set; }
        [NotMapped]
        public string Campo16 { get; set; }
        [NotMapped]
        public string Campo17 { get; set; }
        [NotMapped]
        public string Campo18 { get; set; }
        [NotMapped]
        public string Campo19 { get; set; }
        [NotMapped]
        public string Campo20 { get; set; }
        [NotMapped]
        public string Campo21 { get; set; }
        [NotMapped]
        public string Campo22 { get; set; }
        [NotMapped]
        public string Campo23 { get; set; }

    }
}
