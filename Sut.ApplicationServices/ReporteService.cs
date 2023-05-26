using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.ApplicationServices
{
    public class ReporteService : IReporteService
    {
        private readonly IAnexoPersonalRepository _anexoPersonalRepository;
        private readonly IAnexoIdentificableRepository _anexoIdentificableRepository;
        private readonly IAnexoNoIdentificableRepository _anexoNoIdentificableRepository;
        private readonly IRecursoCostoRepository _RecursoCostoRepository;
        private readonly IActividadRecursoRepository _ActividadRecursoRepository;

        public ReporteService (IAnexoPersonalRepository anexoPersonalRepository,
                                IAnexoIdentificableRepository anexoIdentificableRepository,
                                IAnexoNoIdentificableRepository anexoNoIdentificableRepository,
                                IRecursoCostoRepository RecursoCostoRepository,
                                IActividadRecursoRepository ActividadRecursoRepository)
        {
            _anexoPersonalRepository = anexoPersonalRepository;
            _anexoIdentificableRepository = anexoIdentificableRepository;
            _anexoNoIdentificableRepository = anexoNoIdentificableRepository;
            _RecursoCostoRepository = RecursoCostoRepository;
            _ActividadRecursoRepository = ActividadRecursoRepository;
        }

        public List<ActividadRecurso> GetActividadRecursoListaProceso1(long ExpedienteId, TipoRecurso tiporecurso)
        {
            try
            {
                return _ActividadRecursoRepository.GetActividadRecursoListaProceso1(ExpedienteId, tiporecurso);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ActividadRecurso> GetActividadRecursoLista(long ExpedienteId)
        {
            try
            {
                return _ActividadRecursoRepository.GetActividadRecursoLista(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<RecursoCosto> GetRecursoCostoLista(long ExpedienteId)
        {
            try
            {
                return _RecursoCostoRepository.GetRecursoCostoLista(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnexoPersonal> GetAnexoPeronalByExpedienteTablaASMEId(long ExpedienteId, long TablaASMEId)
        {
            try
            {
                return _anexoPersonalRepository.GetAnexoPeronalByExpedienteTablaASMEId(ExpedienteId, TablaASMEId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AnexoPersonal> GetAnexoPeronalByExpediente(long ExpedienteId)
        {
            try
            {
                return _anexoPersonalRepository.GetByExpediente(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AnexoPersonal> GetAnexoPeronalByExpedientelista(long ExpedienteId)
        {
            try
            {
                return _anexoPersonalRepository.GetByExpedientelista(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AnexoIdentificable> GetAnexoIdentificableByExpedienteTablaASMEId(long ExpedienteId, TipoRecurso tipo, long TablaASMEId)
        {
            try
            {
                return _anexoIdentificableRepository.GetAnexoIdentificableByExpedienteTablaASMEId(ExpedienteId, tipo, TablaASMEId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnexoIdentificable> GetAnexoIdentificableByExpediente(long ExpedienteId, TipoRecurso tipo)
        {
            try
            {
                return _anexoIdentificableRepository.GetByExpediente(ExpedienteId, tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnexoNoIdentificable> GetAnexoNoIdentificableByExpediente(long ExpedienteId, TipoRecurso tipo)
        {
            try
            {
                return _anexoNoIdentificableRepository.GetByExpediente(ExpedienteId, tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnexoNoIdentificable> GetAnexoNoIdentificableByExpedientep1(long ExpedienteId, TipoRecurso tipo)
        {
            try
            {
                return _anexoNoIdentificableRepository.GetByExpedientep1(ExpedienteId, tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
