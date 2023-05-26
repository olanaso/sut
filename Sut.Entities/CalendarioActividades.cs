using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class CalendarioActividades
    {
        [Key]
        public long CalendarioActividadesId { get; set; }
        public long EquipoSubId { get; set; }
        public long AsisTecId { get; set; }
        public long ModalidadId { get; set; }
        public long ResponsableId { get; set; }
        public string LugarEjecucion { get; set; }
        public DateTime? FecProgramada { get; set; }
        public DateTime? FecInicio { get; set; }
        public DateTime? FecFin { get; set; }
        public DateTime? FecEjecucion { get; set; }
        public int Cumplimiento { get; set; }
        public string Observaciones { get; set; }
        public long MateriaId { get; set; }
        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
        public int TipoRango { get; set; }

        public long DepartamentoId { get; set; }

        [NotMapped]
        public string EquipoSub { get; set; }
        [NotMapped]
        public string AsisTec { get; set; }
        [NotMapped]
        public string Modalidad { get; set; }
        [NotMapped]
        public string Responsable { get; set; }
        [NotMapped]
        public string Materia { get; set; } 
        public int Cantidad { get; set; }

        
    }
}
