using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IApplicationServices
{
    public interface IExpedienteService
    {
        Expediente GetOne(long ExpedienteId);
        Expediente GetOneEntidad(long ExpedienteId, long EntidadId);
        Expediente GetOneActivo(long ExpedienteId);

        List<Expediente> GetByEntidad(long EntidadId);
        List<Expediente> GetByEntidadCompleto();

        List<Procedimiento> GetByEstados(long ExpedienteId);

        List<Expediente> GetByEntidadMax(long EntidadId);

        List<Expediente> GetByEntidadAnulado(long EntidadId);
         
        List<Expediente> GetAllLikePagin(Expediente filtro, int pageIndex, int pageSize, ref int totalRows);
        List<Expediente> GetAllLikePaginRatificador(Expediente filtro, int pageIndex, int pageSize, ref int totalRows);


        List<Expediente> GetAllLikePaginTodo(Expediente filtro, int pageIndex, int pageSize, ref int totalRows);

        List<Expediente> GetAllLikePaginAsigando(Expediente filtro, int pageIndex, int pageSize, ref int totalRows, long UsuarioId);


        List<Requisito> GetAllLikePaginTodorequisitos(Requisito filtro, int pageIndex, int pageSize, ref int totalRows);

        List<Procedimiento> GetAllLikePaginTodoConfigurar(Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows);
        List<Expediente> GetAllLikePaginSOLICITUDWORD(Expediente filtro, int pageIndex, int pageSize, ref int totalRows);

        List<Procedimiento> GetAllLikePaginTodoConfigurarProce(Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows);

        List<Expediente> GetAllLikePaginFiscalizador(Expediente filtro, int pageIndex, int pageSize, ref int totalRows);

        List<Expediente> GetAllLikePaginEvaluador(Expediente filtro, int pageIndex, int pageSize, ref int totalRows); 

        List<Expediente> GetAllLikePaginEvaluadorMEFPCM(Expediente filtro, int pageIndex, int pageSize, ref int totalRows);
        List<Expediente> PresentadosGetAllLikePagin(Expediente filtro, int pageIndex, int pageSize, ref int totalRows);
        List<Expediente> GetAllLikePaginRptActividad(Expediente filtro, int pageIndex, int pageSize, ref int totalRows);
        List<Expediente> GetAllLikePaginRptActividadRPT(Expediente filtro);
        Dictionary<string, int> GetEstadisticas(long ExpedienteId);
        Dictionary<string, int> GetEstadisticasconteo(Procedimiento filtro);
        void Save(Expediente obj); 
        string ValidarNuevoExpediente(Expediente obj, long UsuarioId);
        void GenerarNuevo(Expediente obj, long UsuarioId, long Sector, long? Provincia, long EntidadId, long rol);
        bool EsPosibleCargaInicial(long EntidadId);
        void ProcesarCostos(long ExpedienteId);
        void EliminarProcesarCostos(long ExpedienteId);
        List<string> ValidarCostos(long ExpedienteId, int procesogratuito);
        List<string> ValidarSubvencion(long ExpedienteId, long Sector);
        void CambiarEstadoSustento(long ExpedienteId, bool estado);
        void CambiarEstado(long ExpedienteId, EstadoExpediente estado);
        void activarpublicado(long ExpedienteId, int estado);

        void activarinfcondicion(long ExpedienteId, TipoOrdenPa estado);

        void ActivarSolicitudWord(long expedienteId);
        void activarpublicadoexp(long ExpedienteId, int estado);
        void activaImportarPAEliminados(long ExpedienteId, int estado);
        void DespublicarCambiarEstado(long ExpedienteId, EstadoExpediente estado); 

        void CambiarEstadoEvaluadores(long ExpedienteId, EstadoExpediente estado,short rol, long EntidadId, short EstadoMinistrio);
    }
}
