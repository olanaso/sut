﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class Entidad
    {
        [Key]
        public long EntidadId { get; set; }
        public int Correlativo { get; set; }
        [MaxLength(10)]
        public string Codigo { get; set; }
        [MaxLength(50)]
        public string Acronimo { get; set; }
        [MaxLength(250)]
        public string Nombre { get; set; }

        [ForeignKey("NivelGobierno")]
        public long NivelGobiernoId { get; set; }
        public MetaDato NivelGobierno { get; set; }
        [ForeignKey("Sector")]
        public long SectorId { get; set; }
        public MetaDato Sector { get; set; }

        [ForeignKey("Provincia")]
        public long? ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }

        //[ForeignKey("Departamento")]
        //public long? DepartamentoId { get; set; }
        //public Departamento Departamento { get; set; }

        public NivelPadre EstadoProvincia { get; set; }

        public bool EsACR { get; set; }
        public TipoTupa TipoTupa { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
        public string CodPadre { get; set; } 

        public int EstadoMinistrio { get; set; } 

        public int DJ { get; set; } 

        public Respuesta Estado { get; set; }

        public int ActivarACR { get; set; }

        public int Ejecucion { get; set; } 
        public int ProcesoGratuito { get; set; }
        public int ActivarAlgoritmo { get; set; }
        public int ActivarExpediente { get; set; }
        public int ActivarMultiUsuario { get; set; }

        public string Bandeja { get; set; }


        [NotMapped]
        public long DepartamentoId { get; set; } 

        [NotMapped]
        public string NivelGobiernodes { get; set; }
        [NotMapped]
        public string Sectordes { get; set; } 

        [NotMapped]
        public string Provinciades { get; set; }
        [NotMapped]
        public string Departamentodes { get; set; }

        public string Logoentidad { get; set; } /*JJJMSP2*/
    }
}
