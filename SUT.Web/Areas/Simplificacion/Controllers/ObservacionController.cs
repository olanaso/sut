using Newtonsoft.Json;
using Sut.Entities;
using Sut.IApplicationServices;
using Sut.Log;
using Sut.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sut.Web.Areas.Simplificacion.Controllers
{
    [Authorize]
    public class ObservacionController : Controller
    {
        private readonly ILogService<ObservacionController> _log;
        private readonly IObservacionService _ObservacionService;
        private readonly IExpedienteService _ExpedienteService;
        private readonly ITupaService _TupaService;
        private readonly INotificacionExpedientesService _notificacionExpedientesService;

        public ObservacionController(IObservacionService ObservacionService, IExpedienteService ExpedienteService, ITupaService TupaService, INotificacionExpedientesService notificacionExpedientesService)
        {
            _log = new LogService<ObservacionController>();
            _ObservacionService = ObservacionService;
            _ExpedienteService = ExpedienteService;
            _TupaService = TupaService;
            _notificacionExpedientesService = notificacionExpedientesService;
        }

        public ActionResult ListaObservacion(UsuarioInfo user)
        {

            ViewBag.Usuario = user;
            return View();
        }

        public string GetAllLikePagin(Observacion filtro, int pageIndex, int pageSize, UsuarioInfo user)
        {
            int totalRows = 0;
            if (user.EntidadId != 1)
            {
                filtro.EntidadId = user.EntidadId;
            }
            long entid = 0;

            Tupa tupaactivo = _TupaService.GetTupaVigenteByEntidad(user.EntidadId);
            List<Observacion> listaTotal = new List<Observacion>();
            if (tupaactivo != null)
            {
                filtro.ExpedienteId = tupaactivo.ExpedienteId;
                listaTotal = _ObservacionService.GetAllLikePagin(filtro, pageIndex, pageSize, ref totalRows);
            }

            if (user.Rol == (short)Rol.Evaluador || user.Rol == (short)Rol.Ratificador || user.Rol == (short)Rol.EntidadFiscalizadora)
            {
                listaTotal = _ObservacionService.GetAllLikePaginEntidad(filtro, pageIndex, pageSize, ref totalRows);
            }

            if (user.Rol == (short)Rol.Administrador)
            {
                listaTotal = _ObservacionService.GetAllLikePaginGeneral(filtro, pageIndex, pageSize, ref totalRows);
            }



            List<Observacion> lista = new List<Observacion>();
            foreach (Observacion l in listaTotal)
            {
                Observacion obj = new Observacion();
                if (entid != l.ExpedienteId)
                {
                    obj.ExpedienteId = l.ExpedienteId;
                    obj.Expediente = l.Expediente;
                    obj.Entidad = l.Entidad;
                    obj.EntidadId = l.EntidadId;
                    obj.FecCreacion = l.FecCreacion;
                    obj.EntidadId = l.EntidadId;
                    entid = l.ExpedienteId;
                    Expediente extpenot = _ExpedienteService.GetOne(l.ExpedienteId);
                    //NotificacionExpedientes not = _notificacionExpedientesService.GetByoneIdExpEnt(l.ExpedienteId,l.EntidadId);
                    //obj.UltimaRevision = not.FecUltimaRevision.ToString();
                    obj.EntidadExpediente = extpenot.Entidad.Nombre.ToString();
                    //obj.UltimaRevision = extpenot.FecAprobacion.ToString(); 
                    lista.Add(obj);

                }
            }

            return JsonConvert.SerializeObject(new
            {
                lista = lista.Distinct().Select(x => new
                {
                    ExpedienteId = x.ExpedienteId,
                    Descripcion = x.Expediente.Codigo,
                    TipoExpediente = (short)x.Expediente.TipoExpediente,
                    Nombre = x.Entidad.Nombre,
                    EntidadId = x.EntidadId,
                    //UltimaRevision = x.UltimaRevision,
                    EntidadExpediente = x.EntidadExpediente,
                    //DiasExpediente = x.DiasExpediente,
                    FecCreacion = x.FecCreacion.Value.ToString("dd/MM/yyyy"),
                }),
                totalRows = lista.Count()
            });
        }

    }
}