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
    public class FactorDedicacionService : IFactorDedicacionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFactorDedicacionRepository _factorDedicacionRepository;
        private readonly IRecursoRepository _recursoRepository;
        private readonly IInductorRepository _inductorRepository;
        private readonly IInductorValorRepository _inductorValorRepository;

        public FactorDedicacionService(IUnitOfWork unitOfWork,
                                    IFactorDedicacionRepository factorDedicacionRepository,
                                    IRecursoRepository recursoRepository,
                                    IInductorRepository inductorRepository,
                                    IInductorValorRepository inductorValorRepository)
        {
            _unitOfWork = unitOfWork;
            _factorDedicacionRepository = factorDedicacionRepository;
            _recursoRepository = recursoRepository;
            _inductorRepository = inductorRepository;
            _inductorValorRepository = inductorValorRepository;
        }

        public List<FactorDedicacion> GetValoresByUndOrganicaLista(long ExpedienteId, TipoRecurso tiporecurso)
        {
            try
            {
               
                var recs = _recursoRepository.GetByExpedienteUndOrganica2(ExpedienteId, tiporecurso);
                //var valores = _factorDedicacionRepository.GetAllBy(x => x.ExpedienteId == ExpedienteId);

                //var valores = _factorDedicacionRepository.GetByFactorDedicacion(ExpedienteId);

                var valores = _factorDedicacionRepository.GetByFactorDedicacionF1(ExpedienteId, tiporecurso);

                var query = recs.GroupJoin(valores,
                                r => r.RecursoId,
                                v => v.RecursoId,
                                (r, v) => new { recurso = r, valores = v })
                                .Select(x =>
                                {
                                    FactorDedicacion f = new FactorDedicacion();
                                    f.RecursoId = x.recurso.RecursoId;
                                    f.Recurso = x.recurso;
                                    f.UnidadOrganica= x.valores.First().UnidadOrganica;
                                    f.UnidadOrganicaId = x.valores.First().UnidadOrganicaId;
                                    f.ExpedienteId = ExpedienteId;
                                    if (x.valores.Count() > 0)
                                    {
                                        f.FactorDedicacionId = x.valores.First().FactorDedicacionId;
                                        f.ValorTupa = x.valores.First().ValorTupa;
                                        f.ValorNoTupa = x.valores.First().ValorNoTupa;
                                        f.AutoCalculo = x.valores.First().AutoCalculo;
                                    }
                                    return f;
                                });

                return valores.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<FactorDedicacion> GetValoresByUndOrganica(long ExpedienteId, long UnidadOrganicaId)
        {
            try
            {
                var recs = _recursoRepository.GetByExpedienteUndOrganica(ExpedienteId, UnidadOrganicaId);
                var valores = _factorDedicacionRepository.GetAllBy(x => x.ExpedienteId == ExpedienteId && x.UnidadOrganicaId == UnidadOrganicaId);

                var query = recs.GroupJoin(valores,
                                r => r.RecursoId,
                                v => v.RecursoId,
                                (r, v) => new { recurso = r, valores = v })
                                .Select(x => {
                                    FactorDedicacion f = new FactorDedicacion();
                                    f.RecursoId = x.recurso.RecursoId;
                                    f.Recurso = x.recurso;
                                    f.UnidadOrganicaId = UnidadOrganicaId;
                                    f.ExpedienteId = ExpedienteId;
                                    if (x.valores.Count() > 0)
                                    {
                                        f.FactorDedicacionId = x.valores.First().FactorDedicacionId;
                                        f.ValorTupa = x.valores.First().ValorTupa;
                                        f.ValorNoTupa = x.valores.First().ValorNoTupa;
                                        f.AutoCalculo = x.valores.First().AutoCalculo;
                                    }
                                    return f;
                                });

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<FactorDedicacion> GetAutoCalculoByUndOrganica(long EntidadId, long ActivarAlgoritmo, long ExpedienteId, long UnidadOrganicaId)
        {
            try
            {
                var recs = _recursoRepository.GetByExpedienteUndOrganicaWithDuracionTotal(ExpedienteId, UnidadOrganicaId);
                var valores = _factorDedicacionRepository.GetAllBy(x => x.ExpedienteId == ExpedienteId && x.UnidadOrganicaId == UnidadOrganicaId);
                long InductorId = _inductorRepository.GetAllBy(x => x.EntidadId == EntidadId)
                                    .Min(x => x.InductorId);
                var val = _inductorValorRepository.GetOne(x => x.ExpedienteId == ExpedienteId
                                                                && x.UnidadOrganicaId == UnidadOrganicaId
                                                                && x.InductorId == InductorId);
                decimal cant = 0;
                if (val != null) cant = val.Valor;
                
                var query = recs.GroupJoin(valores,
                                r => r.RecursoId,
                                v => v.RecursoId,
                                (r, v) => new { recurso = r, valores = v })
                                .Select(x => {
                                    FactorDedicacion f = new FactorDedicacion();
                                    f.RecursoId = x.recurso.RecursoId;
                                    f.Recurso = x.recurso;
                                    f.UnidadOrganicaId = UnidadOrganicaId;
                                    f.ExpedienteId = ExpedienteId;
                                    f.AutoCalculo = true;
                                    if (x.valores.Count() > 0)
                                    {
                                        f.FactorDedicacionId = x.valores.First().FactorDedicacionId;
                                    }
                                        // nuemero de persona, 8 horas, 264 dias, 60 minutos
                                        if (cant > 0) f.ValorTupa = x.recurso.DuracionTotal * 100 / (cant * 8 * 264 * 60);
                                   

                                    return f;
                                }); 
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<FactorDedicacion> GetAutoCalculoByUndOrganicacantidad(long EntidadId, long ExpedienteId, long UnidadOrganicaId)
        {
            try
            {
         
                var valores = _factorDedicacionRepository.GetAllBy(x => x.ExpedienteId == ExpedienteId && x.UnidadOrganicaId == UnidadOrganicaId);
              
                return valores.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Guardar(List<FactorDedicacion> lista)
        {
            try
            {
                _factorDedicacionRepository.Guardar(lista);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(long ExpedienteId)
        {
            try
            {
                _factorDedicacionRepository.Eliminar(ExpedienteId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> Validarfactor(long ExpedienteId, long UnidadOrganicaId)
        {
            try
            {
                List<string> lista = new List<string>();
                bool valid = true; 

                var actividades = _factorDedicacionRepository
                                    .GetAllBy(x => x.ExpedienteId==ExpedienteId && x.UnidadOrganicaId== UnidadOrganicaId );
                if (actividades.Count() == 0)
                {
                    List<string> procs = actividades.GroupBy(x => x.Expediente.Codigo).Select(x => x.Key).ToList();
                    lista.Add(string.Format("Auto Calcular"));
                }
                 

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
