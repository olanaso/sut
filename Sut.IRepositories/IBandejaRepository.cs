using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface IBandejaRepository : IBaseRepository<Bandeja>
    {
        Bandeja GetOne(long BandejaId);
        List<Bandeja> GetAll(long ExpedienteId);
        List<Bandeja> GetAllusuario(long UsuarioId);
        void Save(Bandeja obj);
        void Save2(List<Bandeja> obj);

        void Eliminar(long ExpedienteId);
    }
}
