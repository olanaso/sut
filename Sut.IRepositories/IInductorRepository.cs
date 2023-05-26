using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface IInductorRepository : IBaseRepository<Inductor>
    {
        Inductor GetOne(long InductorId);
        List<Inductor> GetAll(long EntidadId);
        void Save(Inductor obj);
        List<Inductor> GetByExpediente(long ExpedienteId);


        void Eliminar(long InductorId);
    }
}
