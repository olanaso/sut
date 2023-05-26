using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface IUbigeoRepository : IBaseRepository<Ubigeo>
    {
        List<Ubigeo> GetDepartamentos();
        List<Ubigeo> GetProvincias(int DepartamentoId);
        List<Ubigeo> GetDistritos(int DepartamentoId, int ProvinciaId);
    }
}
