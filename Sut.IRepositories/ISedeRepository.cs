using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface ISedeRepository : IBaseRepository<Sede>
    {
        Sede GetOne(long SedeId); 
         List<Sede> GetAll(long EntidadId);
        List<Sede> GetAllgrupo(long EntidadId); 
              List<Sede> GetAllgrupoCarga(long EntidadId);
        void Save(Sede obj);
        void Eliminar(int SedeId);

    }
}
