using Sut.Entities;
using Sut.IDomainServices;
using Sut.IRepositories;
using System;
using System.Linq;

namespace Sut.DomainServices
{
    public class ExpedienteDomainService : IExpedienteDomainService
    {
        private readonly IExpedienteRepository _expedienteRepository;

        public ExpedienteDomainService (IExpedienteRepository expedienteRepository)
        {
            _expedienteRepository = expedienteRepository;
        }

        public bool ValidarCrearNuevoExpediente(long EntidadId)
        {
            try
            {
                var expedientes = _expedienteRepository.GetByEntidad(EntidadId);
                
                if (expedientes.Count() > 0)
                {
                    long maxId = expedientes.Max(x => x.ExpedienteId);
                    var exp = expedientes.First(x => x.ExpedienteId == maxId);

                    return exp.EstadoExpediente == EstadoExpediente.Aprobado || exp.EstadoExpediente == EstadoExpediente.Anulado;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
