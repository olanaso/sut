using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IApplicationServices
{
    public interface IBandejaService
    {
        Bandeja GetOne(long BandejaId);
        List<Bandeja> GetAll(long ExpedienteId);
        List<Bandeja> GetAllusuario(long UsuarioId);
        List<Bandeja> GetAllLikePagin(Bandeja filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(Bandeja obj);

        void Save2(List<Bandeja> obj);
        void Eliminar(long ExpedienteId);
    }
}
