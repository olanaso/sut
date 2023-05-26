using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface ITablaAsmeService
    {
        TablaAsme GetOne(long TablaAsmeId);

        List<TablaAsme> GetByProcedimiento(long ProcedimientoId);
        List<TablaAsme> GetByExpediente(long ExpedienteId);

        List<TablaAsme> GetByExpedienteSinEliminados(long ExpedienteId);
        List<TablaAsme> GetByMef(TablaAsme filtro, int pageIndex, int pageSize, ref int totalRows); 
     
        void Guardar(TablaAsme obj);
        void Guardarexcel(TablaAsme obj);
        void Guardardelete(TablaAsme obj);


        void delete(TablaAsme obj);
        List<TablaAsme> ResumenGetAllLikePagin(TablaAsme filtro, int pageIndex, int pageSize, ref int totalRows);
        void GuardarSubvencion(List<TablaAsme> lstSubvencion); 
        void Save(TablaAsme obj);
    }
}
