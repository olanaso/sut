using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sut.Security;
 

namespace Sut.IApplicationServices
{
    public interface IProcedimientoService
    {
        Procedimiento GetOne(long ProcedimientoId); 
        Procedimiento GetOneCount(string codigo,long ExpedienteId);

        List<Procedimiento> GetByExpediente(long ExpedienteId);
        List<Procedimiento> GetByExpedienteNumero(long ExpedienteId);



        List<Procedimiento> GetByExpedienteNumeroEli(long ExpedienteId);


        List<Procedimiento> GetByExpedienteNumerotipoorder(long ExpedienteId, int tipo);


        List<Procedimiento> GetByExpedienteNumerotipoordersinEliminar(long ExpedienteId, int tipo);

        String Denominacion(long ProcedimientoId);
        List<Procedimiento> codigocorto(string codigocorto);
        List<Procedimiento> codvalor(string codvalor);

        List<Procedimiento> GetAllLikePagin(Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows);


        List<Procedimiento> GetAllLikePaginACR(Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows);

        List<Procedimiento> GetAllLikePaginObservado(Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows);
        List<Procedimiento> GetAllLikePaginFiltro(Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows,Filtros fil);

        List<TablaAsme> GetAllLikePaginFiltroAsme(TablaAsme filtro, int pageIndex, int pageSize,long tablaAsmeId, ref int totalRows);
        void GuardarNuevoCodigo(Procedimiento obj);
        void GuardarPosicion(Procedimiento obj);
        void Save(Procedimiento obj);
        void Savemodalidad(Procedimiento obj);
        void SaveCopia(Procedimiento obj);
        void SaveInformacionCiudadano(Procedimiento obj);
        void SaveTablaAsme(Procedimiento obj);
        void SaveInformacionBasica(Procedimiento obj);


        void CambiarEstadoSustento(TipoSustento tipo, long ProcedimientoId, bool estado);


        



        void quitarduplocado();

        void CambiarOperacion(OperacionExpediente operacion, long ProcedimientoId);
        void CambiarObsEstado(long ProcedimientoId, string Pantalla, int estado);
        void ResetObsEstado(long ProcedimientoId, int estado);
        void GuardarSustEliminacion(Procedimiento obj);


        List<Procedimiento> GetAllLikePaginByTupaEstandar(TipoTupa tipo, Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows);
        List<Procedimiento> GetAllLikePaginByTupaEstandarDelete(long expedienteId, long tipo, int pageIndex, int pageSize, ref int totalRows);


        List<Procedimiento> GetAllLikePaginByTupaxidDelete(long expedienteId, long tipo, Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows);

        List<Procedimiento> GetAllLikePaginByTupa(TipoTupa tipo, Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows);
        List<Procedimiento> GetAllLikePaginByTupaxid(TipoTupa tipo, Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows);
        void CopiarProcedimiento(long ExpedienteId, List<long> ids, long UsuarioId, long EntidadId, int CodigoCanalOficinaEntidad);
        void Eliminar(long id);
        List<ProcedimientoSede> GetSedesByExpediente(long ExpedienteId);
        List<string> ValidarSustTecnico(long ProcedimientoId);
        int GetOrdenNewProcByEntidad(long EntidadId);
        List<string> CopiarDatosProcedimiento(long ProcedimientoOrigenId, long ProcedimientoDestinoId, List<DatoCopia> lstCopia, string CodigoCopia, string CodigoCortoCopia);
        List<string> CopiarDatosProcedimientoEli(long ProcedimientoOrigenId, long ProcedimientoDestinoId, List<DatoCopia> lstCopia, string CodigoCopia, string CodigoCortoCopia);

        List<string> CopiarDatosProcedimientoTablaASME(long ProcedimientoOrigenId, long ProcedimientoDestinoId, long TablaAsmeIdSeleccionado, long TablaAsmeIdCopiar);

        List<string> ImportarProcedimientoACR(Procedimiento obj, long EntidadId);

        List<string> ImportarProcedimientoACREXANTE(Procedimiento obj, long EntidadId);
        void Eliminacionprocedimiento(Procedimiento obj);
        void Updateprocedimiento(Procedimiento obj);
        void guardarnumeroprocedimiento(Procedimiento obj);
        void modificarprocedimiento(Procedimiento obj);

        void Eliminacionorg(Procedimiento obj);
        void activarCondicion(Procedimiento obj);
    }
}
