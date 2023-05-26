using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories.Procedimientos
{
    class ProcedimientoAuditoria
    {
        #region Procedimiento
        public static readonly string registrarAuditoria = "PROC_insertar_Auditoria";
        public static readonly string PresentarAprobar = "Aprobar_Presentar_Exapediente";
        public static readonly string actualizarobservacion = "actualizar_observacion";
        public static readonly string LimpiarObsExpediente = "LimpiarObs_Expediente";
        public static readonly string limpiarposicion = "limpiar_posicion";
        public static readonly string GenerarNumeracion = "Generar_Numeracion";
        public static readonly string BotonSiguienteAtrasINDEX = "BotonSiguienteAtras_INDEX";
        public static readonly string ResetearFactor = "Resetear_Factor";
        public static readonly string ValidarProcedimiento = "Validar_procedimiento";
        public static readonly string actualizarSubsanar = "actualizar_Subsanar";
        public static readonly string ActualizarLicencias = "Actualizar_Licencias";

        public static readonly string InsertarusuarioAsig = "Insertar_usuarioAsig";
        public static readonly string QuitarusuarioAsig = "Quitar_usuarioAsig";


        public static readonly string InsertarusuarioAsigActividad = "Insertar_usuarioAsigActividad";
        public static readonly string QuitarusuarioAsigActividad = "Quitar_usuarioAsigActividad";

        public static readonly string actualizarReconResol = "actualizar_ReconResol";



        public static readonly string actualizarReconResolDet = "actualizar_ReconResolDet";




        public static readonly string eliminarActividadBlanco = "Eliminar_ActividadBlanco";


        public static readonly string VerificarEliminar = "VerificarEliminar_UnidadOrganicaId";
        public static readonly string VerificarEliminarEditar = "VerificarEliminar_UnidadOrganicaIdEditar";


        public static readonly string AnularExpediente = "Anular_Expediente";

        public static readonly string EliminarRecuroEspera = "Eliminar_RecuroEspera";


        public static readonly string VerificarEliminarSede = "VerificarEliminar_SedeId";
        public static readonly string VerificarEliminarRecursoEditar = "VerificarEliminar_RecursoIdEditar";
        public static readonly string VerificarEliminarRecurso = "VerificarEliminar_RecursoId";
        public static readonly string VerificarEliminarInductor = "VerificarEliminar_InductorId";
        public static readonly string VerificarEliminarInductorEditar = "VerificarEliminar_InductorIdEditar";
        public static readonly string VerificarEliminarRequisitoId = "VerificarEliminar_RequisitoId";
        public static readonly string ExcluirEntidadMEF = "Excluir_EntidadMEF";


        public static readonly string ValidarUnidadOrganicaId = "Validar_UnidadOrganicaId";
        public static readonly string ValidarRecursoId = "Validar_RecursoId";
        public static readonly string ValidarInductorId = "Validar_InductorId";

        public static readonly string AgregarObservacion = "Agregar_Observacion";
        public static readonly string CancelarObservacion = "Cancelar_Observacion";
        public static readonly string CambiarEstadoExpediente = "CambiarEstado_Expediente";
        public static readonly string CambiarEstadoSustento = "CambiarEstado_Sustento";
        public static readonly string CambiarEstadoProceso = "CambiarEstado_Proceso";





        public static readonly string OBSTOTAL = "OBS_TOTAL";

        public static readonly string ValidarTOTAL = "ValidarTOTAL";





        public static readonly string calculoexpediente = "calculo_expediente";
        public static readonly string RptFactorTupaNoTupa = "Rpt_FactorTupa_NoTupa";


        public static readonly string ListaCalendario = "Lista_Calendario";

        



        public static readonly string ListaObsCiudadano = "ListaObs_Ciudadano";


        public static readonly string ListaObsArAdjunto = "ListaObs_ArAdjunto";
        public static readonly string ListaObsNORMALST = "ListaObs_NORMALST";
        public static readonly string ListaObsNORMA = "ListaObs_NORMA";


        public static readonly string LISTAUSUARIO = "LISTA_USUARIO";
        public static readonly string LISTAEXPEDIENTE = "LISTA_EXPEDIENTE";
        public static readonly string LISTAEQUIPOTRABAJO = "LISTA_EQUIPOTRABAJO";


        public static readonly string ListaObsConfiguracionAsignacionCosto = "ListaObs_ConfiguracionAsignacionCosto";
        public static readonly string ListaObsConfiguracionInductorCosto = "ListaObs_ConfiguracionInductorCosto";
        public static readonly string ListaObsCostosdeDepreciacionyAmortizacionCosto = "ListaObs_CostosdeDepreciacionyAmortizacionCosto";
        public static readonly string ListaObsCostosdeMaterialNoFungibleCosto = "ListaObs_CostosdeMaterialNoFungibleCosto";
        public static readonly string ListaObsCostosdeServiciosdeTercerosCosto = "ListaObs_CostosdeServiciosdeTercerosCosto";
        public static readonly string ListaObsCostosdeServiciosIdentificablesCosto = "ListaObs_CostosdeServiciosIdentificablesCosto";
        public static readonly string ListaObsCostosFijosCosto = "ListaObs_CostosFijosCosto";
        public static readonly string ListaObsDatosGeneralesComp = "ListaObs_DatosGeneralesComp";
        public static readonly string ListaObsDatosGenerales = "ListaObs_DatosGenerales";
        public static readonly string ListaObsMaterialFungibleCosto = "ListaObs_MaterialFungibleCosto";
        public static readonly string ListaObsPersonalCosto = "ListaObs_PersonalCosto";
        public static readonly string ListaObsResumenCosto = "ListaObs_ResumenCosto";
        public static readonly string ListaObsSTL = "ListaObs_STL";
        public static readonly string ListaObsTablaAsme = "ListaObs_TablaAsme";
        public static readonly string listaObsGeneral = "ListaObs_General";


        public static readonly string ListarEntidades = "Listar_Entidades";
        public static readonly string ListarEntidadesActividad = "Listar_EntidadesActividad";



        public static readonly string ActividadesListar = "Actividades_Listar";

        public static readonly string Listaexpfiscalizador = "Listaexp_fiscalizador";


        


        public static readonly string ListarEntidadesAsignadas = "Listar_EntidadesAsignadas";

        public static readonly string ListarEntidadesAsignadasActividad = "Listar_EntidadesAsignadasActividad";
        


        public static readonly string LISTARARCHIVOSPA = "LISTAR_ARCHIVOS_PA";
        public static readonly string LISTARARCHIVOSPAID = "LISTAR_ARCHIVOS_PA_ID";


        public static readonly string ListarUnidadOrganica = "Listar_UnidadOrganica";
        //Add ESEO
        public static readonly string ListarDashboard = "Listar_Dashboard";
        public static readonly string ListarActividadesFechas = "Listar_ActividadesFechas";
		//JJJMSP2 INI
		public static readonly string paSutProcedimientoComparar = "paSUT_Procedimiento_Comparar";
        #endregion

        #region Parametros
		public static readonly string EntidadId1 = "@piEntId";
		public static readonly string ExpedienteId1 = "@pExpedienteBase";
		public static readonly string ExpedienteId2 = "@pExpedienteComparar";
		//JJJMSP2 FIN
		
        public static readonly string EntidadId = "@pEntidadId";
        public static readonly string Descripcion = "@pDescripcion";
        public static readonly string SectorId = "@pSectorId";
        public static readonly string ProvinciaId = "@pProvinciaId";
        public static readonly string Usuario = "@pUsuario";
        public static readonly string UnidadOrganicaId = "@pUnidadOrganicaId";
        public static readonly string SedeId = "@pSedeId";
        public static readonly string RecursoId = "@pRecursoId";
        public static readonly string InductorId = "@pInductorId";

        public static readonly string RequisitoId = "@pRequisitoId";
        public static readonly string TipoRequisito = "@pTipoRequisito";
        public static readonly string estadoExpediente = "@pestadoExpediente";
        public static readonly string EstadoExpediente = "@pEstadoExpediente";









        public static readonly string Actividad = "@pActividad";
        public static readonly string Pantalla = "@pPantalla";
        public static readonly string UserCreacion = "@pUserCreacion";
        public static readonly string FecCreacion = "@pFecCreacion";
        public static readonly string ExpedienteId = "@pExpedienteId";
        public static readonly string PzoReconResol = "@pPzoReconResol"; 


        public static readonly string TipoRep = "@PTipoRep";

        public static readonly string CalendarioActividadesId = "@pCalendarioActividadesId";
        

        public static readonly string Num = "@pNum";
        public static readonly string ExplorarNum = "@pExplorarNum";
        public static readonly string Ficha = "@pFicha";
        public static readonly string ProcedimientoID = "@pProcedimientoID";


        public static readonly string NivelGobiernoId = "@pNivelGobiernoId";
        public static readonly string Acronimo = "@pAcronimo";



        //agregar observacion

        public static readonly string ObservacionId = "@pObservacionId";
        public static readonly string CodValidacion = "@pCodValidacion";
        public static readonly string Estado = "@pEstado";
        public static readonly string ProcedimientoId = "@pProcedimientoId";
        public static readonly string TablaAsmeId = "@pTablaAsmeId";
        public static readonly string ActividadId = "@pActividadId";
        public static readonly string BaseLegalId = "@pBaseLegalId";
        public static readonly string BaseLegalNormaId = "@pBaseLegalNormaId";
        public static readonly string RecursoCostoId = "@pRecursoCostoId";
        public static readonly string CodPadre = "@pCodPadre";
        public static readonly string CodHijo = "@pCodHijo";
        public static readonly string TipoEstado = "@pTipoEstado";
        public static readonly string NombreCampo = "@pNombreCampo";
        public static readonly string UsuarioCreacion = "@pUsuarioCreacion";
        public static readonly string EstadoCambio = "@pEstadoCambio";
        //-----



        #endregion
    }
}
