using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface INotificacionExpedientesService
    {
       
        void Save(NotificacionExpedientes obj);
         
        List<NotificacionExpedientes> GetByNotificacionExpedientes(NotificacionExpedientes filtro, int pageIndex, int pageSize, ref int totalRows);
        NotificacionExpedientes GetByone(long notificacionExpedientesId);

        NotificacionExpedientes GetByoneIdExpEnt(long ExpedientesId,long EntidadId);

        NotificacionExpedientes GetByoneExpedinte(long expedienteId, int nivel);

        List<NotificacionExpedientes> GetByListaExpedinte(long expedienteId);

        List<NotificacionExpedientes> GetAllLikePaginRatificadorEval(NotificacionExpedientes filtro, int pageIndex, int pageSize, ref int totalRows);

        List<NotificacionExpedientes> GetAllLikePaginEvaluadorMEFPCMEval(NotificacionExpedientes filtro, int pageIndex, int pageSize, ref int totalRows);

        List<NotificacionExpedientes> GetAllLikePaginEvaluadorEval(NotificacionExpedientes filtro, int pageIndex, int pageSize, ref int totalRows);

        List<NotificacionExpedientes> GetAllLikePaginFiscalizadorEval(NotificacionExpedientes filtro, int pageIndex, int pageSize, ref int totalRows);

    }
}
