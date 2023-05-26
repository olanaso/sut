using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IAuditoriaService
    {
        Auditoria GetOne(long Auditoriaid);
        List<Auditoria> GetAllLikePagin(Auditoria filtro, int pageIndex, int pageSize, ref int totalRows);


        List<Auditoria> GetAllReporte(Auditoria filtro, int roladmi);

        
        List<Auditoria> ListaRptEquipoTrabajo();
        List<Auditoria> ListaRptUsuario();
        List<Auditoria> ListaRptExpediente();

        List<Auditoria> GetAllLikePaginAudi(Auditoria filtro, int roladmi, int pageIndex, int pageSize, ref int totalRows);
        void Save(Auditoria obj);

         

        int registrarAuditoria(Auditoria auditoria);

        int InsertarusuarioAsig(UsuarioAsignacion auditoria);
        int QuitarusuarioAsig(UsuarioAsignacion auditoria);


        int InsertarusuarioAsigActividad(GrupoEntidad auditoria);
        int QuitarusuarioAsigActividad(GrupoEntidad auditoria);

        int PresentarAprobar(long pExpedienteId);
        int ActualizarObservacion(long pExpedienteId);


        int ActualizarResolver(long pProcedimientoId, long pPzoReconResol);

        int ActualizarResolverDet(long pProcedimientoId, long pPzoReconResol);

        int Eliminar_ActividadBlanco(long pTablaAsmeId);

        int LimpiarObs_Expediente(long pExpedienteId, string pUsuarioCreacion, string pUserCreacion);
        int AlimpiarPosicion(long pExpedienteId, long pProcedimientoId);
        int GenerarNumeracion(long pExpedienteId);

        int BotonSiguienteAtras_INDEX(long pExpedienteId, long pExplorarNum);


        int ActualizarLicencias(long pUsuario);

        List<Auditoria> listaObs_NORMA(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_NORMALISTA(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_ArAdjunto(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_ciudadano(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_DatoGenerales(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_DatoGeneralesComp(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_STL(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_TablaAsme(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_ResumenCosto(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_PersonalCosto(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_MATERIALFUNGIBLE(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_SERVICIOIDENTIFICABLE(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_MATERIALNOFUNGIBLE(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_SERVICIOTERCEROS(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_DEPRECIACIONAMORIZACION(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_COSTOSFIJOS(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_VALORESINDUCTORES(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_INDUCTORSIGNACIONCOSTOS(long pExpedienteId, long pEntidadId);
        List<Auditoria> listaObs_General(long pExpedienteId, long pEntidadId);

		/*JJJMSP2*/
		List<CompararPA> getProcedimientoComparar(long pEntidadId1, long pExpedienteId1, long pExpedienteId2);

        int ValidarUnidadOrganicaId(long pUnidadOrganicaId, long pEntidadId, string pDescripcion);


        int ValidarRecursoId(long pRecursoId, long pEntidadId, string pDescripcion);




        int ValidarInductorId(long pInductorId, long pEntidadId, string pDescripcion);
        int AgregarObservacion(Observacion filtro, int pEstadoCambio, string pUsuarioCreacion, string pUserCreacion, long pEntidadId);
        int CancelarObservacion(Observacion filtro, int pEstadoCambio, string pUsuarioCreacion, string pUserCreacion, long pEntidadId);

        int ResetearFactor(long pExpedienteId);

        int CambiarEstado(long pExpedienteId, long pEstadoExpediente, long pEntidadId);

        int CambiarEstadoSustento(TipoSustento pTipoEstado, long pProcedimientoId, long pEstado, long pExpedienteId);

        int CambiarEstadoProceso(TipoSustento pTipoEstado, long pProcedimientoId, long pEstado, long pExpedienteId);


        int ObservarTtotal(long pExpedienteId, long pNum);
        int ValidarTotal(long pExpedienteId, long pNum);


        string VerificarEliminarReq(long RequisitoId, long TipoRequisito, long estadoExpediente);
        string VerificarEliminar(long UnidadOrganicaId);
        string VerificarRecursoEspera(long pProcedimientoId);

        int AnularExpediente(long pExpedienteId);
        string VerificarEliminareditar(long UnidadOrganicaId);

        List<FactorDedicacion> rptFactorTupa_NoTupa(long pExpedienteId);

        string VerificarEliminarSede(long SedeId);
        string VerificarEliminarRecursoId(long RecursoId);
        string VerificarEliminarRecursoIdEditar(long RecursoId);




        string VerificarEliminarInductorId(long InductorId);

        string VerificarEliminarInductorIdEditar(long InductorId);



        List<RequisitoFormulario> GetAllListaReqID(long pProcedimientoId);
        List<RequisitoFormulario> GetAllListaReq(long pExpedienteId);


        List<Expediente> listagrafico(long pSectorId, long pNivelGobiernoId, string pAcronimo);

        int ActualizarSubsanar(long pExpedienteId);
        int ValidarProcedimiento(long pExpedienteId, long pFicha);


    }
}
