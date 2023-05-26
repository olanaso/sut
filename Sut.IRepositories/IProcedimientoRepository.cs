using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sut.Security;
namespace Sut.IRepositories
{
    public interface IProcedimientoRepository : IBaseRepository<Procedimiento>
    {
        Procedimiento GetOne(long ProcedimientoId);
         
        Procedimiento GetOneCount(string codigo, long ExpedienteId);
        string Denominacion(long ProcedimientoId);

        List<Procedimiento> duplicadocodigo();
        List<Procedimiento> duplicadocodigolargo();
        
        List<Procedimiento> codigocorto(string codigocorto);
        List<Procedimiento> codvalor(string codvalor);
        List<Procedimiento> GetByExpediente(long ExpedienteId);

        List<Procedimiento> GetByExpedientesinEliminar(long ExpedienteId);
        List<Procedimiento> GetByExpedienteACR(long ExpedienteId);
        List<Procedimiento> GetByExpedienteNumero(long ExpedienteId);
        List<Procedimiento> GetByExpedienteNumeroEli(long ExpedienteId);


        List<Procedimiento> GetByExpedienteFiltro(long ExpedienteId, Filtros fill);


        List<TablaAsme> GetByExpedienteFiltroAsme(long ExpedienteId,long tablaAsmeId);


        void Save(Procedimiento obj);
        void Savemodalidad(Procedimiento obj);
        void SaveInformacionCiudadano(Procedimiento obj);
        void SaveTablaAsme(Procedimiento obj);
        void SaveInformacionBasica(Procedimiento obj);
        void SaveOnlyProcedimiento(Procedimiento obj);
        List<Procedimiento> GetByExpedienteNoTracking(long ExpedienteId); 
        List<Procedimiento> GetNoTracking(List<long> ids);
        List<Procedimiento> GetByTipoTupa(TipoTupa tipo);

        List<Procedimiento> GetByTipoTupaEstantar(TipoTupa tipo);

        List<Procedimiento> GetByTipoTupaEstantarDelete(long expedienteId, long tipo);
        List<Procedimiento> GetByTipoTupaxidDelete(long expedienteId, long tipo, long ExpedienteId);
        List<Procedimiento> GetByTipoTupaxid(TipoTupa tipo, long ExpedienteId);
        IQueryable<Procedimiento> GetProcByEntidad(long EntidadId);
        List<string> CopiarDatosProcedimiento(long ProcedimientoOrigenId, long ProcedimientoDestinoId, List<DatoCopia> lstCopia, string CodigoCopia, string CodigoCortoCopia);
        List<string> CopiarDatosProcedimientoEli(long ProcedimientoOrigenId, long ProcedimientoDestinoId, List<DatoCopia> lstCopia, string CodigoCopia, string CodigoCortoCopia);

        List<string> CopiarDatosProcedimientoTablaASME(long ProcedimientoOrigenId, long ProcedimientoDestinoId, long TablaAsmeIdSeleccionado, long TablaAsmeIdCopiar);

        List<string> ImportarProcedimiento(Procedimiento obj, long EntidadId);
        List<string> ImportarProcedimientoACR(Procedimiento obj, long EntidadId);

        List<string> ImportarProcedimientoACREXANTE(Procedimiento obj, long EntidadId);
        
        //void CopiarTablaAsmeProcedimiento(long ProcedimientoOrigenId, long ProcedimientoDestinoId, List<DatoCopia> lstCopia, string CodigoCopia, string CodigoCortoCopia);
    }
}
