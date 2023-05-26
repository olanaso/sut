using Sut.Entities;
using System;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface IExpedienteRepository : IBaseRepository<Expediente>
    {
        Expediente GetOne(long ExpedienteId);
        Expediente GetOneEntidad(long ExpedienteId, long EntidadId);
        Expediente GetOneActivo(long EntidadId);
        List<Expediente> GetByEntidad(long EntidadId);
        List<Expediente> GetByEntidadCompleto();
        
        List<Procedimiento> GetByEstados(long ExpedienteId);
        List<Expediente> GetByEntidadMax(long EntidadId);



        List<Expediente> GetByEntidadAnulado(long EntidadId);
        List<Expediente> GetByEntidadRatificador(long? ProvinciaId);


        List<Expediente> GetAllLikePaginTodo();
        List<Expediente> GetAllLikePaginTodo(long LDepartamentoId);

        List<Expediente> GetAllLikePaginAsigando();

        
        List<Expediente> GetAllLikePaginSOLICITUDWORD();
        List<Procedimiento> GetAllLikePaginTodoConfigurar();
        List<Procedimiento> GetAllLikePaginTodoConfigurarProce();
        List<Requisito> GetAllLikePaginTodorequisitos(long ProcedimientoId);

        List<Expediente> GetByEntidadFiscalizador(EstadoExpediente estadoExpediente);


        List<Expediente> GetByEntidadEvaluador(long SectorId);


        List<Expediente> GetAllByRptActividad(long pExpedienteId, int pTipoRep);

        List<Expediente> GetAllLikePaginEvaluadorMEFPCM(EstadoExpediente estadoExpediente);
        List<Expediente> GetAllBy(System.Linq.Expressions.Expression<Func<Expediente, bool>> predicate);
        void Save(Expediente obj);
        
        void EliminarProcesarCostos(long ExpedienteId);
        void ProcesaCostos(long ExpedienteId);
        void SaveOnlyExpediente(Expediente obj);
    }
}
