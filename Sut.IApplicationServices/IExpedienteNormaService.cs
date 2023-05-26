using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IExpedienteNormaService
    {
        List<ExpedienteNorma> GetAllLikePagin(ExpedienteNorma filtro, int pageIndex, int pageSize, ref int totalRows);
        ExpedienteNorma GetOne(long NormaId);
        ExpedienteNorma GetOneexpediente(long ExpedienteId);
        List<ExpedienteNorma> GetByExpedientenorma(long ExpedienteId);
         
        void Save(ExpedienteNorma obj);

        void eliminar(long id);
    }
}
