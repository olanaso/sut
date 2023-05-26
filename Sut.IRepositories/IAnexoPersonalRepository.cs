using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IAnexoPersonalRepository : IBaseRepository<AnexoPersonal>
    {
        

        List<AnexoPersonal> GetByExpedientelista(long ExpedienteId);


        List<AnexoPersonal> GetAnexoPeronalByExpedienteTablaASMEId(long ExpedienteId, long TablaASMEId);
        List<AnexoPersonal> GetByExpediente(long ExpedienteId);
    }
}
