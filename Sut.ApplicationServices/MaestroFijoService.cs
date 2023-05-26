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
    public class MaestroFijoService : IMaestroFijoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMaestroFijoRepository _maestroFijoRepository;
        private readonly IExpedienteRepository _expedienteRepository;

        public MaestroFijoService(IUnitOfWork unitOfWork,
                                IMaestroFijoRepository maestroFijoRepository,
                                IExpedienteRepository expedienteRepository)
        {
            _unitOfWork = unitOfWork;
            _maestroFijoRepository = maestroFijoRepository;
            _expedienteRepository = expedienteRepository;
        }


        public MaestroFijo GetOne(long MaestroFijoId)
        {
            try
            {
                return _maestroFijoRepository.GetOne(MaestroFijoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MaestroFijo GetOneByEntidad(long EntidadId)
        {
            try
            {
                return _maestroFijoRepository.GetOneByEntidad(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MaestroFijo GetOneByEntidadMaestro(long EntidadId)
        {
            try
            {
                return _maestroFijoRepository.GetOneByEntidadMaestro(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CambiarEstadoInfCdo(bool tipo, long EntidadId)
        {
            try
            {
                MaestroFijo obj = _maestroFijoRepository.GetOne(x => x.EntidadId == EntidadId);
                obj.InfPredeterminadaTerminado = tipo;
                 
                _maestroFijoRepository.SaveOnlyEntidadMaestro(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(MaestroFijo obj)
        {
            try
            {
                _maestroFijoRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FijarDatoAdicional(long EntidadId, MaestroFijoDatoAdicional da)
        {
            try
            {
                var expedientes = _expedienteRepository.GetByEntidad(EntidadId);
                var exp = expedientes.SingleOrDefault(x => x.EstadoExpediente == EstadoExpediente.EnProceso || x.EstadoExpediente == EstadoExpediente.Observado);

                if(exp != null)
                {
                    _maestroFijoRepository.FijarDatoAdicional(exp.ExpedienteId, da);
                    _unitOfWork.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FijarDatoConsultaTramite(long EntidadId, MaestroFijo model)
        {
            try
            {
                var expedientes = _expedienteRepository.GetByEntidad(EntidadId);
                var exp = expedientes.SingleOrDefault(x => x.EstadoExpediente == EstadoExpediente.EnProceso || x.EstadoExpediente == EstadoExpediente.Observado);

                if (exp != null)
                {
                    _maestroFijoRepository.FijarDatoConsultaTramite(exp.ExpedienteId, model);
                    _unitOfWork.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FijarSede(long EntidadId, MaestroFijoSede sede)
        {
            try
            {
                var expedientes = _expedienteRepository.GetByEntidad(EntidadId);
                var exp = expedientes.SingleOrDefault(x => x.EstadoExpediente == EstadoExpediente.EnProceso || x.EstadoExpediente == EstadoExpediente.Observado);

                if (exp != null)
                {
                    _maestroFijoRepository.FijarSede(exp.ExpedienteId, sede);
                    _unitOfWork.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FijarPasoSeguir(long EntidadId, MaestroFijoPasoSeguir ps)
        {
            try
            {
                var expedientes = _expedienteRepository.GetByEntidad(EntidadId);
                var exp = expedientes.SingleOrDefault(x => x.EstadoExpediente == EstadoExpediente.EnProceso || x.EstadoExpediente == EstadoExpediente.Observado);

                if (exp != null)
                {
                    _maestroFijoRepository.FijarPasoSeguir(exp.ExpedienteId, ps);
                    _unitOfWork.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void FijarNotaCiudadano(long EntidadId, MaestroFijoNotaCiudadano ps)
        {
            try
            {
                var expedientes = _expedienteRepository.GetByEntidad(EntidadId);

                //MaestroFijoNotaCiudadano MaestroFijoNotaCiudadano = _maestroFijoRepository.GetOne(ps.NotaCiudadanoId).MaestroFijoNotaCiudadano.FirstOrDefault();

                var exp = expedientes.SingleOrDefault(x => x.EstadoExpediente == EstadoExpediente.EnProceso || x.EstadoExpediente == EstadoExpediente.Observado);

                if (exp != null)
                {
                    //_maestroFijoRepository.FijarNotaCiudadano(exp.ExpedienteId, MaestroFijoNotaCiudadano);
                    _maestroFijoRepository.FijarNotaCiudadano(exp.ExpedienteId, ps);
                    _unitOfWork.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
