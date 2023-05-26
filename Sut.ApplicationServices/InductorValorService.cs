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
    public class InductorValorService : IInductorValorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInductorRepository _inductorRepository;
        private readonly IInductorValorRepository _inductorValorRepository;
        private readonly IUnidadOrganicaRepository _unidadOrganicaRepository;

        public InductorValorService(IUnitOfWork unitOfWork, 
                                    IInductorValorRepository inductorValorRepository )
        {
            _unitOfWork = unitOfWork; 
            _inductorValorRepository = inductorValorRepository; 
        }

        public InductorValor GetOne(long InductorId)
        {
            try
            {
                return _inductorValorRepository.GetOne(InductorId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InductorValor GetOneValor(long InductorId, long ExpedienteId, long UnidadOrganicaId)
        {
            try
            {
                return _inductorValorRepository.GetOneValor(InductorId, ExpedienteId, UnidadOrganicaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<InductorValor> listGetOne(long InductorId)
        {
            try
            {
                return _inductorValorRepository.listGetOne(InductorId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
