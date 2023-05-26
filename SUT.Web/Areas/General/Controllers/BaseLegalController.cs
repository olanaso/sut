using Sut.Entities;
using Sut.IApplicationServices;
using Sut.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Sut.Web.Areas.General.Controllers
{
    [Authorize]
    public class BaseLegalController : Controller
    {
        private readonly ILogService<BaseLegalController> _log;
        private readonly IBaseLegalService _baseLegalService;
        private readonly INormaService _normaService;
        private readonly IMetaDatoService _metaDatoService;
        private readonly IArchivoAdjuntoService _archivoAdjuntoService;
        private readonly IProcedimientoService _procedimientoService;
        private readonly IRequisitoService _requisitoService;
        private readonly IVCalidadRequisitos_4_2Service _vCalidadRequisitos_4_2Service;

        public BaseLegalController(IBaseLegalService baseLegalService,
                                    INormaService normaService,
                                    IMetaDatoService metaDatoService,
                                    IArchivoAdjuntoService archivoAdjuntoService, IProcedimientoService procedimientoService, IRequisitoService requisitoService, IVCalidadRequisitos_4_2Service VCalidadRequisitos_4_2Service)
        {
            _log = new LogService<BaseLegalController>();
            _baseLegalService = baseLegalService;
            _normaService = normaService;
            _metaDatoService = metaDatoService;
            _archivoAdjuntoService = archivoAdjuntoService;
            _procedimientoService = procedimientoService;
            _vCalidadRequisitos_4_2Service = VCalidadRequisitos_4_2Service;
            _requisitoService = requisitoService;
        }

        public ActionResult EditarDetalle(BaseLegalNorma model, short index, short nuevo, string fnAdd, int ProcedimientoId, string CodigoACR, int requisito, int cantidad)
        {
            try
            {
                ViewBag.Index = index;
                ViewBag.FnAdd = fnAdd;
                ViewBag.Nuevo = nuevo;
                ViewBag.CodigoACR = CodigoACR;

                ViewBag.cantidad = cantidad;
                var texto = "";
                ViewBag.BaseLegalTexto2 = texto;

                if (requisito == 0)
                {
                    Procedimiento model3 = new Procedimiento();
                    model3 = _procedimientoService.GetOne(ProcedimientoId);
                    ViewBag.BaseLegalTexto = model3.BaseLegalTexto;
                }
                else
                {
                    Requisito model3 = new Requisito();
                    model3 = _requisitoService.GetOne(requisito);

                    if (model3 != null)
                    {
                        if (model3.BaseLegalTexto == "null")
                        {
                            Procedimiento modelacr = new Procedimiento();
                            modelacr = _procedimientoService.GetOne(ProcedimientoId);
                            List<VCalidadRequisitos_4_2> listaCalidadRequisitos_4_2 = _vCalidadRequisitos_4_2Service.GetAll(Int32.Parse(modelacr.CodigoACR));


                            foreach (var procacr in listaCalidadRequisitos_4_2)
                            {
                                texto = texto + "Requisito.-\n" + procacr.VREQUISITO + "\n \n" + "Norma.-\n" + procacr.VNORMA + "\n \n";
                            }

                            ViewBag.BaseLegalTexto = model3.BaseLegalTexto;
                            ViewBag.BaseLegalTexto2 = texto;
                        }
                        else
                        {
                            ViewBag.BaseLegalTexto = model3.BaseLegalTexto;
                        }
                    }



                }




                if (model.NormaId != null && model.NormaId != 0)
                {
                    if (model.NormaId > 0)
                        model.Norma = _normaService.GetOne(model.NormaId.Value);

                    model.ArchivoAdjuntoId = 0;
                }
                else if (model.TipoBaseLegal == TipoBaseLegal.Adjunto)
                {
                    if (model.ArchivoAdjuntoId != null)
                        model.ArchivoAdjunto = _archivoAdjuntoService.GetOne(model.ArchivoAdjuntoId.Value);
                }
                else
                {
                    model.ArchivoAdjuntoId = 0;
                }

                ViewBag.listTipoBL = new List<SelectListItem>() {
                    //new SelectListItem() { Value = "1", Text = "Norma Registrada" },
                    new SelectListItem() { Value = "2", Text = "Registrar Norma" },
                    new SelectListItem() { Value = "3", Text = "Archivo en Línea" }
                };


                List<SelectListItem> listaTipoNorma = _metaDatoService.GetByParent(12)
                                        .Select(x => new SelectListItem()
                                        {
                                            Value = x.MetaDatoId.ToString(),
                                            Text = x.Nombre
                                        })
                                        .ToList();
                listaTipoNorma.Insert(0, new SelectListItem() { Value = "0", Text = "- SELECCIONAR -" });

                ViewBag.listTipoNorma = listaTipoNorma;

                List<SelectListItem> listarenovacion = _metaDatoService.GetByParent(14)
                                       .Select(x => new SelectListItem()
                                       {
                                           Value = x.MetaDatoId.ToString(),
                                           Text = x.Nombre
                                       })
                                       .ToList();

                listarenovacion.Insert(0, new SelectListItem() { Value = "0", Text = "- SELECCIONAR -" });

                ViewBag.lrenovacion = listarenovacion;

                return PartialView("_EditarDetalle", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _log.Error(ex);
                ModelState.AddModelError("", ex.Message);
                return PartialView("_Error");
            }
        }

    }
}
