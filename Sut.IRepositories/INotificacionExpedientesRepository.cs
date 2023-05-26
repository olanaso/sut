using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface INotificacionExpedientesRepository : IBaseRepository<NotificacionExpedientes>
    {
        
        new void Save(NotificacionExpedientes obj); 
        NotificacionExpedientes GetByone(long notificacionExpedientesId);
        NotificacionExpedientes GetByoneIdExpEnt(long ExpedientesId, long EntidadId);

        NotificacionExpedientes GetByoneExpedinte(long notificacionExpedientesId, int nivel);
        List<NotificacionExpedientes> GetByListaExpedinte(long expedienteId);
        new List<NotificacionExpedientes> GetAll();


        List<NotificacionExpedientes> GetByEntidadRatificadorEval(long? ProvinciaId);
        List<NotificacionExpedientes> GetAllLikePaginEvaluadorPCMEval(EstadoExpediente estadoExpediente);
        List<NotificacionExpedientes> GetAllLikePaginEvaluadorMEFEval(EstadoExpediente estadoExpediente);
        
        List<NotificacionExpedientes> GetByEntidadEvaluadorEval(long SectorId);
        List<NotificacionExpedientes> GetByEntidadFiscalizadorEval(EstadoExpediente estadoExpediente);
    }
}
