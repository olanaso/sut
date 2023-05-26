using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using Sut.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.ApplicationServices
{
    public class InductorService : IInductorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInductorRepository _inductorRepository;
        private readonly IInductorValorRepository _inductorValorRepository;
        private readonly IUnidadOrganicaRepository _unidadOrganicaRepository;
        private readonly IActividadRecursoRepository _actividadRecursoRepository;
        private readonly IAuditoriaRepository _auditoriaRepository;

        public InductorService(IUnitOfWork unitOfWork,
                                    IInductorRepository inductorRepository,
                                    IInductorValorRepository inductorValorRepository,
                                    IUnidadOrganicaRepository unidadOrganicaRepository,
                                    IActividadRecursoRepository actividadRecursoRepository,
                                     IAuditoriaRepository auditoriaRepository)
        {
            _unitOfWork = unitOfWork;
            _inductorRepository = inductorRepository;
            _inductorValorRepository = inductorValorRepository;
            _unidadOrganicaRepository = unidadOrganicaRepository;
            _actividadRecursoRepository = actividadRecursoRepository;
            _auditoriaRepository = auditoriaRepository;
        }

        public List<Inductor> GetAll(long EntidadId)
        {
            try
            {
                return _inductorRepository.GetAll(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Inductor GetOne(long InductorId)
        {
            try
            {
                return _inductorRepository.GetOne(InductorId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Inductor> GetAllLikePagin(Inductor filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Inductor> query = _inductorRepository.GetAll(filtro.EntidadId);

                var data = query.Where(x => x.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Nombre) ? x.Nombre : filtro.Nombre).ToUpper()))
                            .OrderBy(x => x.InductorId);

                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(long InductorId)
        {
            try
            {
                _inductorRepository.Eliminar(InductorId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Save(Inductor obj)
        {
            try
            {
                _inductorRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Inductor> GetByExpediente(long ExpedienteId)
        {
            try
            {
                return _inductorRepository.GetByExpediente(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<InductorValor> GetValoresByInductorEntidadid(long EntidadId, long ExpedienteId)
        {
            try
            {
                var uos = _unidadOrganicaRepository.GetAll(EntidadId);

                var inducto = _inductorRepository.GetAll(EntidadId);
                //var valores = _inductorValorRepository.GetAllBy(x => x.ExpedienteId == ExpedienteId);
                var valores = _inductorValorRepository.GetOneExpediente(ExpedienteId);


                var query = uos.GroupJoin(valores,
                                uo => uo.UnidadOrganicaId,
                                v => v.UnidadOrganicaId,
                                (uo, v) => new { unidad_organica = uo, valores = v })
                                .Select(x =>
                                {
                                    InductorValor iv = new InductorValor();
                                    iv.UnidadOrganicaId = x.unidad_organica.UnidadOrganicaId;
                                    iv.UnidadOrganica = x.unidad_organica;
                                    iv.Inductor = x.valores.First().Inductor;
                                    iv.ExpedienteId = ExpedienteId;
                                    if (x.valores.Count() > 0)
                                    {
                                        iv.InductorValorId = x.valores.First().InductorValorId;
                                        iv.Valor = x.valores.First().Valor;
                                    }
                                    return iv;
                                });

                return valores.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<InductorValor> GetValoresByInductor(long EntidadId,long ExpedienteId, long InductorId)
        {
            try
            {
                var uos1 = _unidadOrganicaRepository.GetAll(EntidadId);

                //var uos = _actividadRecursoRepository.GetByActividadEntidad(EntidadId);

                var uos = _auditoriaRepository.GetAllListaUnidadOrganica(EntidadId, ExpedienteId);


                var valores = _inductorValorRepository.GetAllBy(x => x.ExpedienteId == ExpedienteId && x.InductorId == InductorId);

                var query = uos.GroupJoin(valores,
                                uo => uo.UnidadOrganicaId,
                                v => v.UnidadOrganicaId,
                                (uo, v) => new { unidad_organica = uo, valores = v })
                                .Select(x => {
                                    InductorValor iv = new InductorValor();
                                    iv.UnidadOrganicaId = x.unidad_organica.UnidadOrganicaId;
                                    iv.UnidadOrganica = x.unidad_organica;
                                    iv.InductorId = InductorId;
                                    iv.ExpedienteId = ExpedienteId;
                                    if (x.valores.Count() > 0)
                                    {
                                        iv.InductorValorId = x.valores.First().InductorValorId;
                                        iv.Valor = x.valores.First().Valor;
                                    }
                                    return iv;
                                });

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarInductorValor(List<InductorValor> lista)
        {
            try
            {
                _inductorValorRepository.Guardar(lista);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
