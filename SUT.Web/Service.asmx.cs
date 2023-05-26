using Sut.ApplicationServices;
using Sut.Context;
using Sut.Entities;
using Sut.IApplicationServices;
using Sut.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;

namespace Sut.Web
{
    /// <summary>
    /// Descripción breve de Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        private readonly IEntidadService _entidadService;
        private readonly IMaestroFijoService _maestroFijoService;
        private readonly ProcedimientoRepository _procedimientoRepository;

        public Service()
        {
            SutContext context = new SutContext();
            Sut.Repositories.Context ctx = new Repositories.Context(context);
            EntidadRepository entidadRepository = new EntidadRepository(ctx);
            MetaDatoRepository metaDatoRepository = new MetaDatoRepository(ctx);
            MaestroFijoRepository maestroFijoRepository = new MaestroFijoRepository(ctx);
            _procedimientoRepository = new ProcedimientoRepository(ctx);
            ExpedienteRepository expedienteRepository = new ExpedienteRepository(ctx);
            _entidadService = new EntidadService(ctx, entidadRepository, metaDatoRepository, _procedimientoRepository);
            _maestroFijoService = new MaestroFijoService(ctx, maestroFijoRepository, expedienteRepository);
        }

        [WebMethod]
        public Sut.Web.WebService.Resultado RegistrarProcedimiento(Sut.Web.WebService.Procedimiento newProc)
        {
            WebService.Resultado result = new WebService.Resultado(true);

            Entidad entidad = _entidadService.GetOne(newProc.CodEntidad);
            if (entidad == null) result.Mensajes.Add(string.Format("No existe Entidad con código : {0}", newProc.CodEntidad));
            if (newProc.Calificacion < 0 || newProc.Calificacion > 3) result.Mensajes.Add(string.Format("La Calificación no puede ser {0}", newProc.Calificacion));
            if (newProc.TipoProcedimiento < 0 || newProc.TipoProcedimiento > 2) result.Mensajes.Add(string.Format("El tipo de procedimiento no puede ser {0}", newProc.TipoProcedimiento));

            if (result.Mensajes.Count == 0)
            {
                Procedimiento proc = new Procedimiento();
                proc.TipoProcedimiento = (TipoProcedimiento)Enum.Parse(typeof(TipoProcedimiento), newProc.TipoProcedimiento.ToString());
                proc.CodigoACR = newProc.CodProcACR;
                proc.Codigo = "S/C";
                proc.Denominacion = newProc.Denominacion;
                proc.Operacion = OperacionExpediente.Nuevo;
                proc.Calificacion = (CalificacionProcedimiento)Enum.Parse(typeof(CalificacionProcedimiento), newProc.Calificacion.ToString());
                proc.TipoAtencionId = 10;
                proc.BaseLegalTexto = newProc.BaseLegal;
                proc.Renovacio = newProc.Renovacio;
                proc.Plazorenovacion = newProc.Plazorenovacion;
                proc.Objetivo = newProc.Objetivo;
                proc.FrecuenciaRenovacion = newProc.FrecuenciaRenovacion;
                proc.Requisito = new List<Requisito>();
                proc.PlazoAtencion = newProc.PlazoAtencion;
                proc.UserCreacion = "ACR";
                proc.UserModificacion = "ACR";
                proc.FecCreacion = DateTime.Now;
                proc.FecModificacion = DateTime.Now;

                proc.TablaAsme = new List<TablaAsme>()
                {
                    new TablaAsme() {
                        Codigo = "0000-01",
                        Descripcion = "-",
                        UserCreacion = "ACR",
                        UserModificacion = "ACR",
                        FecCreacion = DateTime.Now,
                        FecModificacion = DateTime.Now
                    }
                };

                proc.BaseLegal = new BaseLegal();
                proc.ProcedimientoDatoAdicional = new List<ProcedimientoDatoAdicional>();
                proc.ProcedimientoSede = new List<ProcedimientoSede>();
                proc.PasoSeguir = new List<PasoSeguir>();

                proc.Expediente = null;
                proc.UndOrgResponsable = null;

                foreach (var req in newProc.Requisito)
                {
                    proc.Requisito.Add(new Requisito()
                    {
                        Nombre = req.Denominacion,
                        BaseLegalTexto = req.BaseLegal,
                        BaseLegal = new BaseLegal()
                    });
                }

                MaestroFijo fijo = _maestroFijoService.GetOneByEntidad(entidad.EntidadId);
                if (fijo != null)
                {
                    if (!string.IsNullOrEmpty(fijo.Telefono)) proc.Telefono = fijo.Telefono;
                    if (!string.IsNullOrEmpty(fijo.Anexo)) proc.Anexo = fijo.Anexo;
                    if (!string.IsNullOrEmpty(fijo.Correo)) proc.Correo = fijo.Correo;

                    if (fijo.MaestroFijoSede != null)
                        if (fijo.MaestroFijoSede.Count() > 0)
                            fijo.MaestroFijoSede.ForEach(sede => proc.ProcedimientoSede.Add(new ProcedimientoSede()
                            {
                                SedeId = sede.SedeId
                            }));

                    if (fijo.MaestroFijoDatoAdicional != null)
                        if (fijo.MaestroFijoDatoAdicional.Count() > 0)
                            fijo.MaestroFijoDatoAdicional.ForEach(da => proc.ProcedimientoDatoAdicional.Add(new ProcedimientoDatoAdicional()
                            {
                                MetaDatoId = da.MetaDatoId,
                                Comentario = da.Comentario
                            }));

                    if (fijo.MaestroFijoPasoSeguir != null)
                        if (fijo.MaestroFijoPasoSeguir.Count() > 0)
                            fijo.MaestroFijoPasoSeguir.ForEach(p => proc.PasoSeguir.Add(new PasoSeguir()
                            {
                                PasoSeguirId = p.PasoSeguirId,
                                Descripcion = p.Descripcion
                            }));
                }

                List<string> mensajes = _procedimientoRepository.ImportarProcedimiento(proc, entidad.EntidadId);
                if (mensajes.Count() > 0) result.Mensajes.AddRange(mensajes);
            }


            result.Exito = result.Mensajes.Count == 0;
            return result;
        }
    }
}

namespace Sut.Web.WebService
{
    public class Procedimiento
    {
        public Procedimiento()
        {
            this.Requisito = new List<Requisito>();
        }

        public string CodEntidad { get; set; }
        public string CodProcACR { get; set; }
        public string Denominacion { get; set; }
        public string Descripcion { get; set; }
        public string Objetivo { get; set; }
        public byte Calificacion { get; set; }
        public byte TipoProcedimiento { get; set; }
        public int PlazoAtencion { get; set; }
        public Renovacio Renovacio { get; set; }
        public short FrecuenciaRenovacion { get; set; }
        public string BaseLegal { get; set; }
        public Plazorenovacion Plazorenovacion { get; set; }

        public List<Requisito> Requisito { get; set; }
    }
    public class Requisito
    {
        public string Denominacion { get; set; }
        public string BaseLegal { get; set; }
    }
    public class Resultado
    {
        public Resultado()
        {
            this.Exito = false;
            this.Mensajes = new List<string>();
        }

        public Resultado(bool Exito)
        {
            this.Exito = Exito;
            this.Mensajes = new List<string>();
        }

        public bool Exito { get; set; }
        public List<string> Mensajes { get; set; }
    }
}
