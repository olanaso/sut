using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IApplicationServices
{
    public interface IUbigeoService
    {
        Ubigeo GetOne(long UbigeoId);
        List<Ubigeo> GetDepartamentos();
        List<Ubigeo> GetProvincias(int DepartamentoId);
        List<Ubigeo> GetDistritos(int DepartamentoId, int ProvinciaId);
    }
}
