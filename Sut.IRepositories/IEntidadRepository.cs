using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IEntidadRepository : IBaseRepository<Entidad>
    {
        new Entidad GetOne(System.Linq.Expressions.Expression<Func<Entidad, bool>> predicate);
        Entidad GetOne(long EntidadId);
        Entidad GetOneMin(long SectorID);
        Entidad GetOneProvincia(long ProvinciaId);
        Entidad GetOne(string Codigo);
        void Guardar(Entidad obj);
        List<Entidad> GetByTipoTupa(TipoTupa tipo);

        List<Entidad> lista_entidades(long pExpedienteId, long pEntidadId);

        List<Entidad> lista_entidadesActividad(long pExpedienteId, long pEntidadId);

        List<Entidad> lista_entidadesAsignado(long pExpedienteId, long pEntidadId);
        List<Entidad> lista_entidadesAsignadoActividad(long pCalendarioActividadesId);
        List<Entidad> SearchEntidadesNombre(string term);
        int MaxCorrelativo();
    }
}
