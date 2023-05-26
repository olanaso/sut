using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class IncentivosCorte
    {
        [Key]  

        public long IncentivosCorteId { get; set; }
        public string NumExpediente { get; set; }
        public DateTime? Fec_Ing_Sol { get; set; }


        [ForeignKey("ArchivoAdjunto")]
        public long? Archivo_Fec_Ing_Sol { get; set; }
        public ArchivoAdjunto ArchivoAdjunto { get; set; }

        public int NivelRiesgo { get; set; }
        public DateTime? Fec_Emision_Cert_ITSE { get; set; }

        [ForeignKey("ArchivoAdjunto2")]
        public long? Archivo_Fec_Emision_Cert_ITSE { get; set; }
        public ArchivoAdjunto2 ArchivoAdjunto2 { get; set; }

        public DateTime? Fec_Notificacion_Cert_ITSE { get; set; } 

        [ForeignKey("ArchivoAdjunto3")]
        public long? Archivo_Fec_Notificacion_Cert_ITSE { get; set; }
        public ArchivoAdjunto3 ArchivoAdjunto3 { get; set; }

        public DateTime? Fec_Emision_Licencia_Funcionamiento { get; set; } 

        [ForeignKey("ArchivoAdjunto4")]
        public long? Archivo_Fec_Emision_Licencia_Funcionamiento { get; set; }
        public ArchivoAdjunto4 ArchivoAdjunto4 { get; set; }


        public DateTime? Fec_Notificacion_Licencia_Funcionamiento { get; set; }

        public DateTime? Fec_Suspencion_ITSE_Subsanacion { get; set; }
        public DateTime? Fec_Resolucion_denegatoria { get; set; }



        [ForeignKey("ArchivoAdjunto5")]
        public long? Archivo_Fec_Notificacion_Licencia_Funcionamiento { get; set; }
        public ArchivoAdjunto5 ArchivoAdjunto5 { get; set; }



        [ForeignKey("ArchivoAdjunto6")]
        public long? Archivo_Fec_Suspencion_ITSE_Subsanacion { get; set; }
        public ArchivoAdjunto6 ArchivoAdjunto6 { get; set; }

        [ForeignKey("ArchivoAdjunto7")]
        public long? Archivo_Fec_Resolucion_denegatoria { get; set; }
        public ArchivoAdjunto7 ArchivoAdjunto7 { get; set; }



        [ForeignKey("ArchivoAdjunto10")]
        public long? Archivo_Fec_Revocacion_Licencia_Funcionamiento { get; set; }
        public ArchivoAdjunto10 ArchivoAdjunto10 { get; set; }
 



        public DateTime? Fec_Revocacion_Licencia_Funcionamiento { get; set; }
        public string Numero_Revocacion_Licencia_Funcionamiento { get; set; }

        public string Cod_Acreditacion_Inspector_ITSE { get; set; }
        public string Numero_Resolucion_Revocatoria { get; set; }
        public DateTime? Renovacion_ITSE { get; set; }
        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        [ForeignKey("Entidad")]
        public long? EntidadId { get; set; }
        public Entidad Entidad { get; set; }
         
        public int Estado { get; set; }
        public int EstadoIngreso { get; set; }


        public string NumRUC { get; set; }
        public string Actividadogironegocio { get; set; }
        public string Girocomplementario { get; set; }


        public string Funcion { get; set; }
        public string Numeral { get; set; }
        public string NivelInicial { get; set; }
        public string NivelFinal { get; set; }



        [NotMapped]
        public long ArchivoAdjuntoId { get; set; }

        [NotMapped]
        public string descripcionnivel { get; set; }
        [NotMapped]
        public string descripcionIngreso { get; set; }
        [NotMapped]
        public string NombreEntidad { get; set; }




        public DateTime? Fec_Suspencion_ITSE { get; set; }

        [ForeignKey("ArchivoAdjunto11")]
        public long? Archivo_Fec_Suspencion_ITSE { get; set; }
        public ArchivoAdjunto11 ArchivoAdjunto11 { get; set; }



        public DateTime? FecCorte { get; set; }
        public int Eva_Fec_doc { get; set; }
        public int Eva_Doc_corresponde { get; set; }
        public string Eva_Comentario { get; set; }
        public string Eva_Observacion { get; set; }
        public int Eva_Resultado { get; set; }



        public DateTime? FecActualizacion { get; set; }
        public int UserActualizacion { get; set; }

    }
}
