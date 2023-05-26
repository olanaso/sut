using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class Expediente
    {
        [Key]
        public long ExpedienteId { get; set; }

        [Required, ForeignKey("Entidad")]
        public long EntidadId { get; set; }
        public Entidad Entidad { get; set; }

        [MaxLength(20)]
        public string Codigo { get; set; }
        public int Numero { get; set; }
        public int publicarCIU { get; set; }
        public int Ano { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public TipoExpediente TipoExpediente { get; set; }
        [Required]
        public EstadoExpediente EstadoExpediente { get; set; }



        ////[Required]
        //[ForeignKey("Tupa")]
        //public long? TupaId { get; set; }
        //public Tupa Tupa { get; set; }

        public bool SustCostosTerminado { get; set; }
        //obs INICIO

        public long ObsCOSTO { get; set; }
        public long ObsCP { get; set; }
        public long ObsCMF { get; set; }
        public long ObsCSI { get; set; }
        public long ObsCMNF { get; set; }
        public long ObsCST { get; set; }
        public long ObsCDA { get; set; }
        public long ObsCF { get; set; }
        public long ObsVI { get; set; }
        public long ObsIAC { get; set; }
        public long ObsCI { get; set; }
        public long ObsCOSTOH { get; set; }
        public long ObsCPH { get; set; }
        public long ObsCMFH { get; set; }
        public long ObsCSIH { get; set; }
        public long ObsCMNFH { get; set; }
        public long ObsCSTH { get; set; }
        public long ObsCDAH { get; set; }
        public long ObsCFH { get; set; }
        public long ObsVIH { get; set; }
        public long ObsIACH { get; set; }
        public long ObsCIH { get; set; }
        //obs FIN

        public int ObsInfCiudadanoTotal { get; set; }
        public int ObsDatosTotal { get; set; }
        public int ObsSTLTotal { get; set; }
        public int ObsTablaAsmeTotal { get; set; }


        public int ProcesarCosto { get; set; }
        
        public DateTime? FecPresentacion { get; set; }
        public DateTime? FecAprobacion { get; set; }
        public DateTime? FecObservacion { get; set; }
        public DateTime? FecAnulacion { get; set; }
        public DateTime? FecPublicacion { get; set; }

        public DateTime? FecSubsanado { get; set; }
        

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
        public string OcultarDes { get; set; }
        public string OcultarNor { get; set; }
        public EstadoExpediente EstadoRactificador { get; set; }
        public EstadoExpediente EstadoEvaluadorMinisterio { get; set; }
        public EstadoExpediente EstadoEvaluadorPCM { get; set; }
        public EstadoExpediente EstadoEvaluadorMEF { get; set; }
        public EstadoExpediente EstadoFiscalizadorPCM { get; set; }
        public EstadoExpediente EstadoFiscalizadorMEF { get; set; }
        public EstadoExpediente EstadoFiscalizadorINDECOPI { get; set; }

        public long SectorId { get; set; }

        public long? ProvinciaId { get; set; }

        //[Required, ForeignKey("Provincia")]
        public virtual Provincia Provincia { get; set; }
        public string EditarNum { get; set; }


        [Required, ForeignKey("ArchivoAdjunto")]
        public long ArchivoAdjuntoId { get; set; }
        public ArchivoAdjunto ArchivoAdjunto { get; set; }

        public int EstadoReporteWord { get; set; }        

        public List<Procedimiento> Procedimiento { get; set; } 
        public List<ProcedimientoCategoria> ProcedimientoCategoria { get; set; }
        public List<Observacion> Observacion { get; set; }
        public int Ascendente { get; set; }
        public Expediente()
        {
            SustCostosTerminado = false;
            Procedimiento = new List<Procedimiento>();
            ProcedimientoCategoria = new List<ProcedimientoCategoria>();
        }

        public int Importar { get; set; }


        [NotMapped]
        public long EstadoId { get; set; }
        [NotMapped]
        public long EstadoTipo { get; set; }
        [NotMapped]
        public string FecCreaciontexto { get; set; }
        [NotMapped]
        public string Entidadtexto { get; set; }


        [NotMapped]
        public string NombreEntidad { get; set; }
        

        public int GenerarPDF { get; set; }

        public int PublicEstandar { get; set; }

        [NotMapped]
        public int condicion { get; set; }

        public int Estado { get; set; } 
        public TipoOrdenPa OrdenPa { get; set; }  
        


        [NotMapped]
        public long NivelGobiernoId { get; set; }


        [NotMapped]
        public int totalterminados { get; set; }

        [NotMapped]
        public long totalpa { get; set; }


        [NotMapped]
        public long multiplicado { get; set; }


        [NotMapped]
        public string Acronimo { get; set; }

        [NotMapped]
        public int ActivarResolver { get; set; }

        //add eseo


        [NotMapped]
        public long DepartamentoId { get; set; }

        [NotMapped]
        public DateTime fecha_inicio { get; set; }

        [NotMapped]
        public DateTime fecha_fin { get; set; }




        [NotMapped]
        public string campo1 { get; set; }
        [NotMapped]
        public int campo2 { get; set; }

        [NotMapped]
        public string campo3 { get; set; }

        [NotMapped]
        public string campo4 { get; set; }

        [NotMapped]
        public string campo5 { get; set; }

        [NotMapped]
        public int campo6 { get; set; }

        [NotMapped]
        public string campo7 { get; set; }

        [NotMapped]
        public int campo8 { get; set; }

        [NotMapped]
        public string campo9 { get; set; }

        [NotMapped]
        public string campo10 { get; set; }

        [NotMapped]
        public string campo11 { get; set; }

        [NotMapped]
        public int campo12 { get; set; }

        [NotMapped]
        public int ptiporeporte { get; set; }
        

    }
}
