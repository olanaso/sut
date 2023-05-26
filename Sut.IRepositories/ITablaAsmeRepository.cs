using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface ITablaAsmeRepository : IBaseRepository<TablaAsme>
    {
        TablaAsme GetOne(long TablaAsmeId);

        void Guardarexcel(TablaAsme obj);

        void Guardar(TablaAsme obj);
        void Guardardelete(TablaAsme obj);
        void  delete(TablaAsme obj);


        List<TablaAsme> GetResumenCostos(long ExpedienteId);
        
        List<TablaAsme> GetByExpediente(long ExpedienteId);
        List<TablaAsme> GetByExpedienteSinEliminados(long ExpedienteId);
        List<TablaAsme> GetByMef();
        void GuardarSubvencion(List<TablaAsme> lstSubvencion); 
        new void Save(TablaAsme obj);
    }
}
