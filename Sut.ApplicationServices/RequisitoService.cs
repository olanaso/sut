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
    public class RequisitoService : IRequisitoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequisitoRepository _requisitoRepository;
        private readonly IBaseLegalRepository _baseLegalRepository;
        private readonly IProcedimientoRepository _procedimientoRepository;

        public RequisitoService(IUnitOfWork unitOfWork,
                                    IRequisitoRepository requisitoRepository,
                                    IBaseLegalRepository baseLegalRepository,
                                    IProcedimientoRepository procedimientoRepository)
        {
            _unitOfWork = unitOfWork;
            _requisitoRepository = requisitoRepository;
            _baseLegalRepository = baseLegalRepository;
            _procedimientoRepository = procedimientoRepository;
        }

        public List<Requisito> GetByProcedimiento(long ProcedimientoId)
        {
            try
            {
                return _requisitoRepository.GetByProcedimiento(ProcedimientoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Requisito> GetByProcedimientoELI(long ProcedimientoId)
        {
            try
            {
                return _requisitoRepository.GetByProcedimientoELI(ProcedimientoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RequisitoFormulario> listProcedimiento(long RequisitoId)
        {
            try
            {
                return _requisitoRepository.listProcedimiento(RequisitoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Requisito> GetByExpediente(long ExpedienteId)
        {
            try
            {
                return _requisitoRepository.GetAllBy(x => x.Procedimiento.ExpedienteId == ExpedienteId && x.Eliminado != 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 

        public RequisitoFormulario GetOneForm(long RequisitoId, long FormularioId)
        {
            try
            {
                return _requisitoRepository.GetOneForm(RequisitoId, FormularioId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Requisito GetOne(long RequisitoId)
        {
            try
            {
                return _requisitoRepository.GetOne(RequisitoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveFormulario(RequisitoFormulario obj)
        {
            try
            {
                _requisitoRepository.SaveFormulario(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Requisito obj)
        {
            try
            {
                _requisitoRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarSustentoTecnico(Procedimiento obj)
        {
            try
            {
                Procedimiento proc = _procedimientoRepository.GetOne(x => x.ProcedimientoId == obj.ProcedimientoId);
                // borrar estado ninguno
                proc.EstadoNinguno = 0;
                proc.SustTecCalificacion = obj.SustTecCalificacion;
                proc.SustentoPlazo = obj.SustentoPlazo;
                _procedimientoRepository.SaveOnlyProcedimiento(proc);
                var requisitos = GetByProcedimiento(obj.ProcedimientoId);
                if (requisitos != null)
                { 
                    foreach (Requisito req in requisitos)
                    {
                        var r = obj.Requisito.First(x => x.RequisitoId == req.RequisitoId);
                        if (!string.IsNullOrEmpty(r.SustentoTecnico))
                        {
                            req.SustentoTecnico = r.SustentoTecnico;
                            _requisitoRepository.Save(req);
                        }
                        else
                        {
                            r.SustentoLegal = "";
                            req.SustentoTecnico = r.SustentoTecnico;
                            _requisitoRepository.Save(req);
                        }

                        if (r.BaseLegal.BaseLegalNorma != null)
                        {
                            _baseLegalRepository.Save(r.BaseLegal);
                        }
                        else
                        {
                            r.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>();
                            _baseLegalRepository.Save(r.BaseLegal);
                        }
                        _unitOfWork.SaveChanges();
                    }
                }
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarSustentoLegal(Procedimiento obj)
        {
            try
            {
                foreach (Requisito req in obj.Requisito)
                {
                    _baseLegalRepository.Save(req.BaseLegal);
                }
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
