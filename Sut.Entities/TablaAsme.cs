using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class TablaAsme
    {
        [Key]
        public long TablaAsmeId { get; set; }

        [ForeignKey("AsmeActual")]
        public long? AsmeActualId { get; set; }
        public TablaAsme AsmeActual { get; set; }

        [ForeignKey("Procedimiento")]
        public long ProcedimientoId { get; set; }
        public Procedimiento Procedimiento { get; set; }


        public List<Observacion> Observacion { get; set; }

        [MaxLength(25)]
        public string Codigo { get; set; }
        [MaxLength(4000)]
        public string Descripcion { get; set; }
        
        public int Prestaciones { get; set; }
        public decimal Personal { get; set; }
        public decimal MaterialFungible { get; set; }
        public decimal ServicioIdentificable { get; set; }
        public decimal MaterialNoFungible { get; set; }
        public decimal ServicioTerceros { get; set; }
        public decimal Depreciacion { get; set; }
        public decimal Fijos { get; set; }


        public List<TablaAsmeReproduccion> TablaAsmeReproduccion { get; set; }

        public decimal CostoUnitario { get; set; }
        public decimal Subvencion { get; set; }
        public decimal PctSubvencion { get; set; }
        public decimal DerechoTramitacion { get; set; }

        public bool EsGratuito { get; set; }
        public bool EsSubvencion { get; set; }

        public bool Terminado { get; set; }

        /*nueva información jleon 01032019*/
        //[ForeignKey("Procedimiento")]
        public long? UITID { get; set; } 
        public UIT UIT { get; set; }

        [ForeignKey("ArMotivoAdjunto")]
        public long? ArMotivoAdjuntoId { get; set; }
        public ArMotivoAdjunto ArMotivoAdjunto { get; set; }

        [ForeignKey("ArchivoAdjunto")]
        public long? ArchivoAdjuntoId { get; set; }
        public ArchivoAdjunto ArchivoAdjunto { get; set; }

        

        public Autorizacionmef AutorizacionMEF { get; set; }

        public string DescripcionSusustento { get; set; }

        public string DescripcionRespuesa { get; set; }
        /*fin información jleon 01032019*/


        //[ForeignKey("Comentarios")]
        //public long? ComentarioSeccionId { get; set; }
        //public ComentarioSeccion Comentarios { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        public List<Actividad> Actividad { get; set; } 


        public DateTime? FecEnvioMef { get; set; }


        public DateTime? FecRespuestaMef { get; set; }
        public int EditarNumAsme { get; set; }


        public CalificacionProcedimiento Calificacion { get; set; }
        public TablaAsme()
        {
            Terminado = false;
        }
    }
}
