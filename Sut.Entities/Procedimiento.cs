using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class Procedimiento
    {
        [Key]
        public long ProcedimientoId { get; set; }

        [ForeignKey("Expediente")]
        public long ExpedienteId { get; set; }
        public Expediente Expediente { get; set; }

        [ForeignKey("Tupa")]
        public long? TupaId { get; set; }
        public Tupa Tupa { get; set; }

        public int Orden { get; set; }
        public int OrdenUnidOrg { get; set; }

        public int FlagCopiarProcedimiento { get; set; }

        //[Required, MaxLength(30)]
        public string Codigo { get; set; }

        //[MaxLength(20)]
        public string CodigoACR { get; set; }


         

        [Required, MaxLength(9000)]
        public string Denominacion { get; set; } 
        public string DenominacionAnterior { get; set; }

        public Plazorenovacion Plazorenovacion { get; set; }


        public int? FrecuenciaRenovacion { get; set; }

        //public listarenovacion Plazorenovaciones { get; set; }
        public string Objetivo { get; set; }

        public Renovacio Renovacio { get; set; }
        [Required]
        public TipoProcedimiento TipoProcedimiento { get; set; }
        [Required]
        public OperacionExpediente Operacion { get; set; }
        [Required]
        public CalificacionProcedimiento Calificacion { get; set; }

        // Codificación
        public long? CategoriaProcedimientoId { get; set; }
        public long? TipoProcedimientoId { get; set; }

        public string SustTecCalificacion { get; set; }

        [ForeignKey("TipoAtencion")]
        public long? TipoAtencionId { get; set; }
        public MetaDato TipoAtencion { get; set; }
        public string BaseLegalTexto { get; set; }
        //public bool Renovacion { get; set; }
        //public short FrecuenciaRenovacion { get; set; }
        public int? PlazoAtencion { get; set; }

        public string SustentoPlazo { get; set; }
 
        public bool EsGratuito { get; set; }

        #region Datos Adicionales

        [ForeignKey("UndOrgPresentDocumentacion")]
        public long? UndOrgPresentDocumentacionId { get; set; }
        public UnidadOrganica UndOrgPresentDocumentacion { get; set; }

        public List<ProcedimientoDatoAdicional> ProcedimientoDatoAdicional { get; set; }

        public string Telefono { get; set; }
        public string Anexo { get; set; }
        public string Correo { get; set; }

        #endregion

        [MaxLength(4000)]
        public string CargoResponsable { get; set; }
        [MaxLength(4000)]
        public string CargoReconsideracion { get; set; }
        [MaxLength(4000)]
        public string CargoApelacion { get; set; }

        public int PzoReconPresent { get; set; }
        public int PzoReconResol { get; set; }
        public int PzoApelPresent { get; set; }
        public int PzoApelResol { get; set; }
        public int ExplorarNum { get; set; }

        

        [ForeignKey("UndOrgResponsable")]
        public long? UndOrgResponsableId { get; set; }
        public UnidadOrganica UndOrgResponsable { get; set; }

        //unidades

        [MaxLength(4000)]
        public string CargoOtros { get; set; }

        [ForeignKey("UndOrgOtros")]
        public long? UndOrgIdOtros { get; set; }
        public UnidadOrganica UndOrgOtros { get; set; }


        public int PzoPresentOtros { get; set; }
        public int PzoResolOtros { get; set; }
         
        public TipoPlazo TipoOtrosPresent { get; set; }
        public TipoPlazo TipoOtrosResol { get; set; }

        public long UndOrgResponsableId2 { get; set; }
 
        public long UndOrgResponsableId3 { get; set; }
        public long UndOrgResponsableId4 { get; set; }
        public long UndOrgResponsableId5 { get; set; }

        //unidades

        //Observacion 
        public long ObsDG { get; set; }
        public long ObsSTL { get; set; }
        public long ObsASME { get; set; }

        public long ObsDGH { get; set; }
        public long ObsSTLH { get; set; }
        public long ObsASMEH { get; set; }
        //public long ObsCOSTO { get; set; } 
        //unidades



        [NotMapped]
        public int condicion { get; set; }

        [NotMapped]
        public int TipoFicha{ get; set; }



        [ForeignKey("UndOrgReconsideracion")]
        public long? UndOrgReconsideracionId { get; set; }
        public UnidadOrganica UndOrgReconsideracion { get; set; }

        [ForeignKey("UndOrgApelacion")]
        public long? UndOrgApelacionId { get; set; }
        public UnidadOrganica UndOrgApelacion { get; set; }

   

        [ForeignKey("BaseLegal")]
        public long? BaseLegalId { get; set; }
        public BaseLegal BaseLegal { get; set; }

        public string SustentoLegal { get; set; }
        public string SustentoNorma { get; set; }

        public string SustentoEliminacion { get; set; }

        public bool SustTecnicoTerminado { get; set; }
        public bool SustLegalTerminado { get; set; }

        public bool DatosGeneralesTerminado { get; set; }

        public bool InfCdnoTerminado { get; set; }
        public bool TablaAsmeTerminado { get; set; }


        [ForeignKey("ComentProc")]
        public long? ComentProcId { get; set; }
        public ComentarioSeccion ComentProc { get; set; }
        [ForeignKey("ComentSustTecnico")]
        public long? ComentSustTecnicoId { get; set; }
        public ComentarioSeccion ComentSustTecnico { get; set; }
        [ForeignKey("ComentSustLegal")]
        public long? ComentSustLegalId { get; set; }
        public ComentarioSeccion ComentSustLegal { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        public List<PasoSeguir> PasoSeguir { get; set; }
        public List<NotaCiudadano> NotaCiudadano { get; set; }
        public List<ProcedimientoSede> ProcedimientoSede { get; set; }
        public List<Requisito> Requisito { get; set; }
        public List<TablaAsme> TablaAsme { get; set; }
        public List<Asignacion> Asignacion { get; set; }

        public List<ProcedimientoCargos> ProcedimientoCargos { get; set; }

        public List<ProcedimientoCargosOtros> ProcedimientoCargosOtros { get; set; }
        public List<ProcedimientoCargosApe> ProcedimientoCargosApe { get; set; }



        public List<ProcedimientoUndOrgResponsable> ProcedimientoUndOrgResponsable { get; set; }

        public string PlazoAtencionDes { get; set; }
        public string PlazoAtencionSus { get; set; }
        [Required]
        public TipoPlazo TipoPlazo { get; set; }
        public int EstadoInfo { get; set; }
        public int Estadodatos { get; set; }
        public int Estadostl { get; set; }
        public int Estadotablaasme { get; set; }
        public int EstadoInfoEva { get; set; }
        public int EstadodatosEva { get; set; }
        public int EstadostlEva { get; set; }
        public int EstadotablaasmeEva { get; set; }
        public int Activar { get; set; }
        public int Activar2 { get; set; }
        public int Activar3 { get; set; }
        public int ActivarAtencionTlf { get; set; }
        public int EstadoNinguno { get; set; }


        

        public List<Observacion> Observacion { get; set; }
        public List<ProcedimientoCategoria> ProcedimientoCategoria { get; set; }
        public Procedimiento()
        {
            //EsGratuito = false;

            SustTecnicoTerminado = false;
            SustLegalTerminado = false;

            DatosGeneralesTerminado = false;
        }
        [NotMapped]
        public List<PlazoAtencion> listplazoatencion { get; set; }
        [NotMapped]
        public short FiltroOperacion { get; set; }
        [NotMapped]
        public short FiltroTipoProcedimiento { get; set; }

        public string CodigoCorto { get; set; }
        public bool? Editado { get; set; }
        [NotMapped]
        public bool Es_Copia { get; set; }
        [NotMapped]
        public bool Es_Seccion { get; set; }

        [NotMapped]
        public string NombreCategoria { get; set; }


        public int Pprestacioncosto { get; set; }

        public int Pplazoatencion { get; set; }

        public int Pprestacionesanuales { get; set; }

        public int Pduracion { get; set; }

        public int Prequisitos { get; set; }


        public string Organo { get; set; }


        public string Sustentolegalrequisitotexto { get; set; }

        //public long Requisitocodigo { get; set; }
        //public FiltrosOrdenar filtroPprestacioncosto { get; set; } 
        //public FiltrosOrdenar filtroPplazoatencion { get; set; } 
        //public FiltrosOrdenar filtroPprestacionesanuales { get; set; } 
        //public FiltrosOrdenar filtroPduracion { get; set; } 
        //public FiltrosOrdenar filtroPrequisitos { get; set; }

        public int? PlazoAtencionEstandar { get; set; }
        public int Estado { get; set; }
         
        public int Numero { get; set; }

        public int sinnotas { get; set; }
        public int reclamacion { get; set; }
        public int revision { get; set; }

        public int EditableG { get; set; }

        public int EditableN { get; set; }

        public int TipoACR { get; set; }

        public int Posicion { get; set; }


        public int MigracionEntidad { get; set; }

        public TipoPlazo TipoReconPresent { get; set; }
        public TipoPlazo TipoReconResol { get; set; }
        public TipoPlazo TipoApelPresent { get; set; }
        public TipoPlazo TipoApelResol { get; set; }




        [NotMapped]
        List<elemento2> elemento { get; set; }

        [NotMapped]
        public int Ascendente { get; set; }


        [NotMapped]
        public string CargoPordependencia { get; set; }


        public int Calificaciones { get; set; }
        public int Bloque { get; set; }
        
    }
}

public class elemento2
{

    public string codigo { get; set; }

    public string nombre { get; set; }

}