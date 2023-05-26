using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IEntidadService
    {
        List<Entidad> EstandarGetAllLikePagin(Entidad filtro, int pageIndex, int pageSize, ref int totalRows);
        List<Entidad> GetAllLikePagin(Entidad filtro, int pageIndex, int pageSize, ref int totalRows);

        List<Entidad> GetAllLikePaginDetalle(Entidad filtro, int pageIndex, int pageSize, ref int totalRows,long  usuarioId);
        List<Entidad> GetAllLikePaginDetalleActividad(Entidad filtro, int pageIndex, int pageSize, ref int totalRows, long usuarioId);

        
        List<Entidad> GetAllLikePaginDetalleAsignado(Entidad filtro, int pageIndex, int pageSize, ref int totalRows, long usuarioId);

        List<Entidad> GetAllLikePaginDetalleAsignadoActividad(Entidad filtro, int pageIndex, int pageSize, ref int totalRows, long pCalendarioActividadesId);



        


        Entidad GetOne(long EntidadId);
        Entidad GetOneMin(long SectorID);
        Entidad GetOneProvincia(long ProvinciaId);

        Entidad GetOne(string Codigo);
        Entidad GetOneByCodigo(string Codigo);
        Entidad GetOneByAcronimo(string Acronimo);
        void Save(Entidad obj);


        List<Entidad> GetAll();
        List<Entidad> GetByTipoTupa(TipoTupa tipo);
    }
}
