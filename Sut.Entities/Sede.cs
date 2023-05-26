using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class Sede
    {
        [Key]
        public long SedeId { get; set; }

        [Required, ForeignKey("Entidad")]
        public long EntidadId { get; set; }
        public Entidad Entidad { get; set; }

        [Required]
        public long? TipoSedeId { get; set; }

        [Required, MaxLength(4000)]
        public string Nombre { get; set; }
        [MaxLength(4000)]
        public string Direccion { get; set; }
        
        [Required, ForeignKey("Distrito")]
        public long? DistritoId { get; set; }
        public Distrito Distrito { get; set; }

        public bool EsLunesViernes { get; set; }
        /*JJJMSP2*/
        public bool EsLunes { get; set; }
        public bool EsMartes { get; set; }
        public bool EsMiercoles { get; set; }
        public bool EsJueves { get; set; }
        public bool EsViernes { get; set; }
        public bool EsSabado { get; set; }
        public bool EsDomingo { get; set; }

        public int Tipogrupo { get; set; }

        [MaxLength(1)]
        public string TipoTurno { get; set; }

        public DateTime? CorridoHorIni { get; set; }
        public DateTime? CorridoHorFin { get; set; }
        public DateTime? Turno1HorIni { get; set; }
        public DateTime? Turno1HorFin { get; set; }
        public DateTime? Turno2HorIni { get; set; }
        public DateTime? Turno2HorFin { get; set; }
        public DateTime? SabadoHorIni { get; set; }
        public DateTime? SabadoHorFin { get; set; }
        public DateTime? DomingoHorIni { get; set; }
        public DateTime? DomingoHorFin { get; set; }

        [Required]
        public bool Activo { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        public List<SedeUnidadOrganica> SedeUnidadOrganica { get; set; }
        
        [NotMapped]
        public string ListaUndOrganica { get; set; }
        [NotMapped]
        public string TextoUndOrganica { get; set; }
        [NotMapped]
        public string TipoSede { get; set; }
        [NotMapped]
        public string OficinasEntidad { get; set; }
    }
}
