using Sut.Context;
using Sut.Entities;
using Sut.Extensions;
using Sut.IRepositories;
using Sut.Log;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sut.Security;

namespace Sut.Repositories
{
    public class ProcedimientoRepository : BaseRepository<Procedimiento>, IProcedimientoRepository
    {
        private readonly ILogService<ProcedimientoRepository> _log;

        public ProcedimientoRepository(IContext context) : base(context)
        {
            _log = new LogService<ProcedimientoRepository>();
        }

        public Procedimiento GetOne(long ProcedimientoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Procedimiento
                        .Include(x => x.Expediente)
                        .Include(x => x.UndOrgResponsable)
                        .Include(x => x.UndOrgApelacion)
                        .Include(x => x.UndOrgReconsideracion)
                        .Include(x => x.UndOrgOtros)
                        .Include("ProcedimientoSede.Sede.Distrito.Provincia.Departamento")
                        .Include("ProcedimientoSede.Sede")
                        .Include("ProcedimientoSede.Sede.SedeUnidadOrganica.UnidadOrganica")
                        .Include("NotaCiudadano.TipoNota")
                        .Include("TablaAsme.Actividad.ActividadRecurso")
                        .Include("TablaAsme.TablaAsmeReproduccion")
                        .Include(x => x.PasoSeguir)
                        .Include(x => x.TablaAsme)
                        .Include(x => x.BaseLegal)
                        .Include(x => x.NotaCiudadano)
                        .Include(x => x.Requisito)
                        .Include(x => x.ProcedimientoCargos)
                        .Include(x => x.ProcedimientoCargosApe)
                        .Include(x => x.ProcedimientoCargosOtros)
                        .Include("Requisito.BaseLegal")
                        .Include("Requisito.RequisitoFormulario")
                        .Include(x => x.ProcedimientoDatoAdicional)
                        .Include("ProcedimientoSede.UndOrgRecepcionDocumentos")
                        .FirstOrDefault(x => x.ProcedimientoId == ProcedimientoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Procedimiento GetOneTablaAsme(long ProcedimientoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Procedimiento
                        .Include(x => x.TablaAsme)
                        .Include("TablaAsme.Actividad")
                        .FirstOrDefault(x => x.ProcedimientoId == ProcedimientoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Procedimiento GetOneCount(string codigo, long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Procedimiento
                        .FirstOrDefault(x => x.ExpedienteId == ExpedienteId && x.Codigo == codigo && x.Estado != 3);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> duplicadocodigo()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Procedimiento.Where(x => x.CodigoCorto != null).OrderBy(x => x.CodigoCorto).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> duplicadocodigolargo()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Procedimiento.Where(x => x.Codigo != null).OrderBy(x => x.Codigo).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Procedimiento> codigocorto(string codigocorto)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Procedimiento.Where(x => x.CodigoCorto == codigocorto).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Procedimiento> codvalor(string codvalor)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Procedimiento.Where(x => x.Codigo == codvalor).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Denominacion(long ProcedimientoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Procedimiento.FirstOrDefault(x => x.ProcedimientoId == ProcedimientoId).Denominacion.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> GetByExpediente2(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                //var lst2 = ctx.Procedimiento.Where(x => x.ExpedienteId == ExpedienteId && x.Estado != 3);

                List<Procedimiento> listpro = new List<Procedimiento>();

                //
                var procedimientos = ctx.Procedimiento.Where(x => x.ExpedienteId == ExpedienteId && x.Estado != 3);



                var list = ctx.Procedimiento
                         //.Include("Asignacion")
                         .Include("UndOrgResponsable")
                         .Include("UndOrgReconsideracion")
                         .Include("UndOrgApelacion")
                         .Include(x => x.ProcedimientoDatoAdicional)
                          .Include("ProcedimientoDatoAdicional")
                         .Include("TablaAsme")
                         .Include(x => x.ProcedimientoSede)
                         .Include(x => x.Requisito)
                         .Include("Requisito.RequisitoFormulario")
                         .Include("ProcedimientoSede.UndOrgRecepcionDocumentos")
                         .Where(x => x.ExpedienteId == ExpedienteId && x.Estado != 3);



                return ctx.Procedimiento
                        //.Include("Asignacion")
                        .Include("UndOrgResponsable")
                        .Include("UndOrgReconsideracion")
                        .Include("UndOrgApelacion")
                        .Include(x => x.ProcedimientoDatoAdicional)
                         .Include("ProcedimientoDatoAdicional")
                        .Include("TablaAsme")
                        .Include(x => x.ProcedimientoSede)
                        .Include(x => x.Requisito)
                        .Include("Requisito.RequisitoFormulario")
                        .Include("ProcedimientoSede.UndOrgRecepcionDocumentos")
                        .Where(x => x.ExpedienteId == ExpedienteId && x.Estado != 3)
                        //.OrderBy(x => x.Numero)
                        .ToList();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> GetByExpediente(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                /*
                   var list = ctx.Procedimiento
                        .Include("Asignacion")
                        .Include("UndOrgResponsable")
                        .Include("UndOrgReconsideracion")
                        .Include("UndOrgApelacion")
                        .Include(x => x.ProcedimientoDatoAdicional)
                         .Include("ProcedimientoDatoAdicional")
                        .Include("TablaAsme")
                        .Include(x => x.ProcedimientoSede)
                        .Include(x => x.Requisito)
                        .Include("Requisito.RequisitoFormulario")
                        .Include("ProcedimientoSede.UndOrgRecepcionDocumentos")
                        .Where(x => x.ExpedienteId == ExpedienteId && x.Estado != 3);

                return ctx.Procedimiento
                        .Include("Asignacion")
                        .Include("UndOrgResponsable")
                        .Include("UndOrgReconsideracion")
                        .Include("UndOrgApelacion")
                        .Include(x => x.ProcedimientoDatoAdicional)
                         .Include("ProcedimientoDatoAdicional")
                        .Include("TablaAsme")
                        .Include(x => x.ProcedimientoSede)
                        .Include(x => x.Requisito)
                        .Include("Requisito.RequisitoFormulario")
                        .Include("ProcedimientoSede.UndOrgRecepcionDocumentos")
                        .Where(x => x.ExpedienteId == ExpedienteId && x.Estado!=3  )
                        //.OrderBy(x => x.Numero)
                        .ToList();
                 */

                // Obtén los procedimientos
                var procedimientos = ctx.Procedimiento
                                        .Where(x => x.ExpedienteId == ExpedienteId && x.Estado != 3)
                                        .ToList();

                // Obtén los IDs
                var responsablesIds = procedimientos.Select(p => p.UndOrgResponsableId).ToList();
                var reconsideracionIds = procedimientos.Select(p => p.UndOrgReconsideracionId).ToList();
                var apelacionIds = procedimientos.Select(p => p.UndOrgApelacionId).ToList();

                // Obtén los UndOrgResponsable
                var undOrgResponsables = ctx.UnidadOrganica
                                            .Where(x => responsablesIds.Contains(x.UnidadOrganicaId))
                                            .ToList();

                // Obtén los UndOrgReconsideracion
                var undOrgReconsideraciones = ctx.UnidadOrganica
                                                .Where(x => reconsideracionIds.Contains(x.UnidadOrganicaId))
                                                .ToList();

                // Obtén los UndOrgApelacion
                var undOrgApelacions = ctx.UnidadOrganica
                                          .Where(x => apelacionIds.Contains(x.UnidadOrganicaId))
                                          .ToList();


                // Obtén los ProcedimientoDatoAdicional
                var procedimientoIds = procedimientos.Select(p => p.ProcedimientoId).ToList();

                var procedimientoDatoAdicionals = ctx.ProcedimientoDatoAdicional
                                                      .Where(x => procedimientoIds.Contains(x.ProcedimientoId))
                                                      .ToList();

                // Obtén los TablaAsme
                var tablaAsmes = ctx.TablaAsme
                                     .Where(x => procedimientoIds.Contains(x.ProcedimientoId))
                                     .ToList();


                // Obtén los ProcedimientoSede
                var procedimientoSedes = ctx.ProcedimientoSede
                                           .Where(x => procedimientoIds.Contains(x.ProcedimientoId))
                                           .ToList();

                // Obtén los Requisito
                var requisitos = ctx.Requisito
                                     .Where(x => procedimientoIds.Contains(x.ProcedimientoId))
                                     .ToList();

                var requisitosIds = requisitos.Select(p => p.RequisitoId).ToList();

                // Obtén los RequisitoFormulario
                var requisitoFormularios = ctx.RequisitoFormulario
                                             .Where(x => requisitosIds.Contains(x.RequisitoId))
                                             .ToList();


                //Vinculando los requisitos con requisito formulario
                foreach (var requisito in requisitos)
                {
                    requisito.RequisitoFormulario = requisitoFormularios.Where(x => x.RequisitoId == requisito.RequisitoId).ToList();
                }



                // Luego, realiza un 'join' manual en la aplicación para combinar los resultados:
                foreach (var procedimiento in procedimientos)
                {
                    //Cargando Unidad organica
                    procedimiento.UndOrgResponsable = (UnidadOrganica)undOrgResponsables.FirstOrDefault(x => x.UnidadOrganicaId == procedimiento.UndOrgResponsableId);
                    //Cargando UndOrgReconsideracion
                    procedimiento.UndOrgReconsideracion = (UnidadOrganica)undOrgReconsideraciones.FirstOrDefault(x => x.UnidadOrganicaId == procedimiento.UndOrgReconsideracionId);
                    //Cargando UndOrgReconsideracion
                    procedimiento.UndOrgApelacion = (UnidadOrganica)undOrgApelacions.FirstOrDefault(x => x.UnidadOrganicaId == procedimiento.UndOrgApelacionId);
                    //Cargando ProcedimientoDatoAdicional
                    procedimiento.ProcedimientoDatoAdicional = (List<ProcedimientoDatoAdicional>)procedimientoDatoAdicionals.Where(x => x.ProcedimientoId == procedimiento.ProcedimientoId).ToList();
                    //Cargando Tabla asme
                    procedimiento.TablaAsme = (List<TablaAsme>)tablaAsmes.Where(x => x.ProcedimientoId == procedimiento.ProcedimientoId).ToList();
                    //Cargando ProcedimientoSede
                    procedimiento.ProcedimientoSede = (List<ProcedimientoSede>)procedimientoSedes.Where(x => x.ProcedimientoId == procedimiento.ProcedimientoId).ToList();
                    //Carga requisitos
                    procedimiento.Requisito = (List<Requisito>)requisitos.Where(x => x.ProcedimientoId == procedimiento.ProcedimientoId).ToList();

                }

                // Ahora 'procedimientos' debería tener todos los datos cargados manualmente




                return procedimientos;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> GetByExpedientesinEliminar(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Procedimiento
                        .Include("Asignacion")
                        .Include("UndOrgResponsable")
                        .Include("UndOrgReconsideracion")
                        .Include("UndOrgApelacion")
                        .Include(x => x.ProcedimientoDatoAdicional)
                         .Include("ProcedimientoDatoAdicional")
                        .Include("TablaAsme")
                        .Include(x => x.ProcedimientoSede)
                        .Include(x => x.Requisito)
                        .Include("Requisito.RequisitoFormulario")
                        .Include("ProcedimientoSede.UndOrgRecepcionDocumentos")
                        .Where(x => x.ExpedienteId == ExpedienteId && x.Estado != 3 && x.Operacion != OperacionExpediente.Eliminacion)
                        //.OrderBy(x => x.Numero)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Procedimiento> GetByExpedienteACR(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Procedimiento
                        .Include("Asignacion")
                        .Include("UndOrgResponsable")
                        .Include("UndOrgReconsideracion")
                        .Include("UndOrgApelacion")
                        .Include(x => x.ProcedimientoDatoAdicional)
                         .Include("ProcedimientoDatoAdicional")
                        .Include("TablaAsme")
                        .Include(x => x.ProcedimientoSede)
                        .Include(x => x.Requisito)
                        .Include("Requisito.RequisitoFormulario")
                        .Include("ProcedimientoSede.UndOrgRecepcionDocumentos")
                        .Where(x => x.ExpedienteId == ExpedienteId && x.Estado != 3 && x.CodigoCorto != null)
                        //.OrderBy(x => x.Numero)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> GetByExpedienteNumero(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

               /* var lista = ctx.Procedimiento
                        .Include("Asignacion")
                        .Include("UndOrgResponsable")
                        .Include("UndOrgReconsideracion")
                        .Include("UndOrgApelacion")
                        .Include(x => x.ProcedimientoDatoAdicional)
                        .Include("ProcedimientoDatoAdicional")
                        .Include("TablaAsme")
                        .Include(x => x.ProcedimientoSede)
                        .Include(x => x.Requisito)
                        .Include("Requisito.RequisitoFormulario")
                        .Include("ProcedimientoSede.UndOrgRecepcionDocumentos")
                        .Where(x => x.ExpedienteId == ExpedienteId && x.Estado != 3 && x.Operacion != OperacionExpediente.Eliminacion && x.CodigoCorto != null)
                        .OrderBy(x => x.Numero)
                        .ToList();*/



                // Obtén los procedimientos
                var procedimientos = ctx.Procedimiento
                                        .Where(x => x.ExpedienteId == ExpedienteId && x.Estado != 3 && x.Operacion != OperacionExpediente.Eliminacion && x.CodigoCorto != null)
                                        .OrderBy(x => x.Numero)
                                        .ToList();

                // Obtén los ProcedimientoDatoAdicional
                var procedimientoIds = procedimientos.Select(p => p.ProcedimientoId).ToList();

                //Listado de asignaciones
                var Asignaciones = ctx.Asignacion.Where(x => procedimientoIds.Contains(x.ProcedimientoId))
                                                            .ToList();

                // Obtén los IDs
                var responsablesIds = procedimientos.Select(p => p.UndOrgResponsableId).ToList();
                var reconsideracionIds = procedimientos.Select(p => p.UndOrgReconsideracionId).ToList();
                var apelacionIds = procedimientos.Select(p => p.UndOrgApelacionId).ToList();

                // Obtén los UndOrgResponsable
                var undOrgResponsables = ctx.UnidadOrganica
                                            .Where(x => responsablesIds.Contains(x.UnidadOrganicaId))
                                            .ToList();

                // Obtén los UndOrgReconsideracion
                var undOrgReconsideraciones = ctx.UnidadOrganica
                                                .Where(x => reconsideracionIds.Contains(x.UnidadOrganicaId))
                                                .ToList();

                // Obtén los UndOrgApelacion
                var undOrgApelacions = ctx.UnidadOrganica
                                          .Where(x => apelacionIds.Contains(x.UnidadOrganicaId))
                                          .ToList();


                // Obtén los ProcedimientoDatoAdicional


                var procedimientoDatoAdicionals = ctx.ProcedimientoDatoAdicional
                                                      .Where(x => procedimientoIds.Contains(x.ProcedimientoId))
                                                      .ToList();

                // Obtén los TablaAsme
                var tablaAsmes = ctx.TablaAsme
                                     .Where(x => procedimientoIds.Contains(x.ProcedimientoId))
                                     .ToList();


                // Obtén los ProcedimientoSede
                var procedimientoSedes = ctx.ProcedimientoSede
                                           .Where(x => procedimientoIds.Contains(x.ProcedimientoId))
                                           .ToList();

                // Obtén los Requisito
                var requisitos = ctx.Requisito
                                     .Where(x => procedimientoIds.Contains(x.ProcedimientoId))
                                     .ToList();

                var requisitosIds = requisitos.Select(p => p.RequisitoId).ToList();

                // Obtén los RequisitoFormulario
                var requisitoFormularios = ctx.RequisitoFormulario
                                             .Where(x => requisitosIds.Contains(x.RequisitoId))
                                             .ToList();


                //Vinculando los requisitos con requisito formulario
                foreach (var requisito in requisitos)
                {
                    requisito.RequisitoFormulario = requisitoFormularios.Where(x => x.RequisitoId == requisito.RequisitoId).ToList();
                }



                // Luego, realiza un 'join' manual en la aplicación para combinar los resultados:
                foreach (var procedimiento in procedimientos)
                {
                    //Cargando Unidad organica
                    procedimiento.UndOrgResponsable = (UnidadOrganica)undOrgResponsables.FirstOrDefault(x => x.UnidadOrganicaId == procedimiento.UndOrgResponsableId);
                    //Cargando UndOrgReconsideracion
                    procedimiento.UndOrgReconsideracion = (UnidadOrganica)undOrgReconsideraciones.FirstOrDefault(x => x.UnidadOrganicaId == procedimiento.UndOrgReconsideracionId);
                    //Cargando UndOrgReconsideracion
                    procedimiento.UndOrgApelacion = (UnidadOrganica)undOrgApelacions.FirstOrDefault(x => x.UnidadOrganicaId == procedimiento.UndOrgApelacionId);
                    //Cargando ProcedimientoDatoAdicional
                    procedimiento.ProcedimientoDatoAdicional = (List<ProcedimientoDatoAdicional>)procedimientoDatoAdicionals.Where(x => x.ProcedimientoId == procedimiento.ProcedimientoId).ToList();
                    //Cargando Tabla asme
                    procedimiento.Asignacion = (List<Asignacion>)Asignaciones.Where(x => x.ProcedimientoId == procedimiento.ProcedimientoId).ToList();
                    //Cargando Tabla asme
                    procedimiento.TablaAsme = (List<TablaAsme>)tablaAsmes.Where(x => x.ProcedimientoId == procedimiento.ProcedimientoId).ToList();
                    //Cargando ProcedimientoSede
                    procedimiento.ProcedimientoSede = (List<ProcedimientoSede>)procedimientoSedes.Where(x => x.ProcedimientoId == procedimiento.ProcedimientoId).ToList();
                    //Carga requisitos
                    procedimiento.Requisito = (List<Requisito>)requisitos.Where(x => x.ProcedimientoId == procedimiento.ProcedimientoId).ToList();

                }

                // Ahora 'procedimientos' debería tener todos los datos cargados manualmente




                return procedimientos;



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Procedimiento> GetByExpedienteNumeroEli(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

               /* return ctx.Procedimiento
                        .Include("Asignacion")
                        .Include("UndOrgResponsable")
                        .Include("UndOrgReconsideracion")
                        .Include("UndOrgApelacion")
                        .Include(x => x.ProcedimientoDatoAdicional)
                         .Include("ProcedimientoDatoAdicional")
                        .Include("TablaAsme")
                        .Include(x => x.ProcedimientoSede)
                        .Include(x => x.Requisito)
                        .Include("Requisito.RequisitoFormulario")
                        .Include("ProcedimientoSede.UndOrgRecepcionDocumentos")
                        .Where(x => x.ExpedienteId == ExpedienteId && x.Estado == 3 || x.Operacion == OperacionExpediente.Eliminacion)
                        .ToList();*/


                // Obtén los procedimientos
                var procedimientos = ctx.Procedimiento
                                        .Where(x => x.ExpedienteId == ExpedienteId && x.Estado == 3 || x.Operacion == OperacionExpediente.Eliminacion)
                                        .ToList();

                // Obtén los ProcedimientoDatoAdicional
                var procedimientoIds = procedimientos.Select(p => p.ProcedimientoId).ToList();

                //Listado de asignaciones
                var Asignaciones = ctx.Asignacion.Where(x => procedimientoIds.Contains(x.ProcedimientoId))
                                                            .ToList();

                // Obtén los IDs
                var responsablesIds = procedimientos.Select(p => p.UndOrgResponsableId).ToList();
                var reconsideracionIds = procedimientos.Select(p => p.UndOrgReconsideracionId).ToList();
                var apelacionIds = procedimientos.Select(p => p.UndOrgApelacionId).ToList();

                // Obtén los UndOrgResponsable
                var undOrgResponsables = ctx.UnidadOrganica
                                            .Where(x => responsablesIds.Contains(x.UnidadOrganicaId))
                                            .ToList();

                // Obtén los UndOrgReconsideracion
                var undOrgReconsideraciones = ctx.UnidadOrganica
                                                .Where(x => reconsideracionIds.Contains(x.UnidadOrganicaId))
                                                .ToList();

                // Obtén los UndOrgApelacion
                var undOrgApelacions = ctx.UnidadOrganica
                                          .Where(x => apelacionIds.Contains(x.UnidadOrganicaId))
                                          .ToList();


                // Obtén los ProcedimientoDatoAdicional


                var procedimientoDatoAdicionals = ctx.ProcedimientoDatoAdicional
                                                      .Where(x => procedimientoIds.Contains(x.ProcedimientoId))
                                                      .ToList();

                // Obtén los TablaAsme
                var tablaAsmes = ctx.TablaAsme
                                     .Where(x => procedimientoIds.Contains(x.ProcedimientoId))
                                     .ToList();


                // Obtén los ProcedimientoSede
                var procedimientoSedes = ctx.ProcedimientoSede
                                           .Where(x => procedimientoIds.Contains(x.ProcedimientoId))
                                           .ToList();

                // Obtén los Requisito
                var requisitos = ctx.Requisito
                                     .Where(x => procedimientoIds.Contains(x.ProcedimientoId))
                                     .ToList();

                var requisitosIds = requisitos.Select(p => p.RequisitoId).ToList();

                // Obtén los RequisitoFormulario
                var requisitoFormularios = ctx.RequisitoFormulario
                                             .Where(x => requisitosIds.Contains(x.RequisitoId))
                                             .ToList();


                //Vinculando los requisitos con requisito formulario
                foreach (var requisito in requisitos)
                {
                    requisito.RequisitoFormulario = requisitoFormularios.Where(x => x.RequisitoId == requisito.RequisitoId).ToList();
                }



                // Luego, realiza un 'join' manual en la aplicación para combinar los resultados:
                foreach (var procedimiento in procedimientos)
                {
                    //Cargando Unidad organica
                    procedimiento.UndOrgResponsable = (UnidadOrganica)undOrgResponsables.FirstOrDefault(x => x.UnidadOrganicaId == procedimiento.UndOrgResponsableId);
                    //Cargando UndOrgReconsideracion
                    procedimiento.UndOrgReconsideracion = (UnidadOrganica)undOrgReconsideraciones.FirstOrDefault(x => x.UnidadOrganicaId == procedimiento.UndOrgReconsideracionId);
                    //Cargando UndOrgReconsideracion
                    procedimiento.UndOrgApelacion = (UnidadOrganica)undOrgApelacions.FirstOrDefault(x => x.UnidadOrganicaId == procedimiento.UndOrgApelacionId);
                    //Cargando ProcedimientoDatoAdicional
                    procedimiento.ProcedimientoDatoAdicional = (List<ProcedimientoDatoAdicional>)procedimientoDatoAdicionals.Where(x => x.ProcedimientoId == procedimiento.ProcedimientoId).ToList();
                    //Cargando Tabla asme
                    procedimiento.Asignacion = (List<Asignacion>)Asignaciones.Where(x => x.ProcedimientoId == procedimiento.ProcedimientoId).ToList();
                    //Cargando Tabla asme
                    procedimiento.TablaAsme = (List<TablaAsme>)tablaAsmes.Where(x => x.ProcedimientoId == procedimiento.ProcedimientoId).ToList();
                    //Cargando ProcedimientoSede
                    procedimiento.ProcedimientoSede = (List<ProcedimientoSede>)procedimientoSedes.Where(x => x.ProcedimientoId == procedimiento.ProcedimientoId).ToList();
                    //Carga requisitos
                    procedimiento.Requisito = (List<Requisito>)requisitos.Where(x => x.ProcedimientoId == procedimiento.ProcedimientoId).ToList();

                }
                return procedimientos;
                // Ahora 'procedimientos' debería tener todos los datos cargados manualmente
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Procedimiento> GetByExpedienteFiltro(long ExpedienteId, Filtros fill)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Procedimiento
                        .Include("Asignacion")
                        .Include("UndOrgResponsable")
                        .Include("UndOrgReconsideracion")
                        .Include("UndOrgApelacion")
                        .Include(x => x.ProcedimientoDatoAdicional)
                         .Include("ProcedimientoDatoAdicional")
                        .Include("TablaAsme")
                        .Include(x => x.ProcedimientoSede)
                        .Include(x => x.Requisito)
                        .Include("Requisito.RequisitoFormulario")
                        .Include("ProcedimientoSede.UndOrgRecepcionDocumentos")
                        .Where(x => x.ExpedienteId == ExpedienteId && x.Estado != 3 && x.Operacion != OperacionExpediente.Eliminacion)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<TablaAsme> GetByExpedienteFiltroAsme(long ExpedienteId, long tablaAsmeId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.TablaAsme
                        .Include(x => x.Procedimiento)
                        .Where(x => x.Procedimiento.ExpedienteId == ExpedienteId && x.Procedimiento.Estado != 3 && x.Procedimiento.Operacion != OperacionExpediente.Eliminacion && x.Procedimiento.Operacion != OperacionExpediente.Ninguna && x.TablaAsmeId != tablaAsmeId && x.Actividad.Count() != 0)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Save(Procedimiento obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                if (!obj.Es_Copia)
                    if (obj.ProcedimientoId > 0)
                    {
                        var oldSede = ctx.ProcedimientoSede.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                        foreach (ProcedimientoSede sede in obj.ProcedimientoSede)
                            ctx.Entry(sede).State = oldSede.Count(x => x.SedeId == sede.SedeId) > 0 ? EntityState.Modified : EntityState.Added;
                        //ctx.Entry(sede).State = EntityState.Detached;
                        oldSede.ToList()
                            .ForEach(x =>
                            {
                                if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                            });

                        var oldNota = ctx.NotaCiudadano.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                        foreach (NotaCiudadano nota in obj.NotaCiudadano)
                            ctx.Entry(nota).State = oldNota.Count(x => x.NotaCiudadanoId == nota.NotaCiudadanoId) > 0 ? EntityState.Modified : EntityState.Added;

                        oldNota.ToList()
                            .ForEach(x =>
                            {
                                if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                            });

                        var oldPasoSeguir = ctx.PasoSeguir.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                        foreach (PasoSeguir paso in obj.PasoSeguir)
                            ctx.Entry(paso).State = oldPasoSeguir.Count(x => x.PasoSeguirId == paso.PasoSeguirId) > 0 ? EntityState.Modified : EntityState.Added;

                        oldPasoSeguir.ToList()
                            .ForEach(x =>
                            {
                                if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                            });

                        var oldDatoAdicional = ctx.ProcedimientoDatoAdicional.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                        if (obj.ProcedimientoDatoAdicional != null)
                        {
                            string sc = "1";
                            foreach (ProcedimientoDatoAdicional da in obj.ProcedimientoDatoAdicional)
                                ctx.Entry(da).State = oldDatoAdicional.Count(x => x.MetaDatoId == da.MetaDatoId) > 0 ? EntityState.Modified : EntityState.Added;

                            oldDatoAdicional.ToList()
                                .ForEach(x =>
                                {
                                    if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                                });
                        }
                        var oldReq = ctx.Requisito
                                        .Include(x => x.RequisitoFormulario)
                                        .Where(x => x.ProcedimientoId == obj.ProcedimientoId)
                                        .AsNoTracking()
                                        .ToList();

                        foreach (Requisito nReq in obj.Requisito)
                        {
                            if (nReq.RequisitoId != 0)
                            {
                                var listForm = oldReq.Single(x => x.RequisitoId == nReq.RequisitoId).RequisitoFormulario;
                                foreach (RequisitoFormulario nForm in nReq.RequisitoFormulario)
                                {
                                    ctx.Entry(nForm).State = listForm.Count(x => x.FormularioId == nForm.FormularioId) > 0 ? EntityState.Modified : EntityState.Added;
                                }

                                foreach (RequisitoFormulario oForm in listForm.Where(x => nReq.RequisitoFormulario.Count(f => f.FormularioId == x.FormularioId) == 0))
                                {
                                    oForm.Requisito = null;
                                    ctx.Entry(oForm).State = EntityState.Deleted;
                                }

                                ctx.Entry(nReq).State = EntityState.Modified;
                            }
                            else
                            {
                                nReq.BaseLegal = new BaseLegal();
                                ctx.Entry(nReq).State = EntityState.Added;
                            }

                        }

                        oldReq.Where(x => obj.Requisito.Count(r => r.RequisitoId == x.RequisitoId) == 0)
                            .ToList()
                            .ForEach(x =>
                            {
                                ctx.Entry(x).State = EntityState.Deleted;
                            });

                        var oldTabla = ctx.TablaAsme.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                        //AsNoTracking().Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                        foreach (TablaAsme tabla in obj.TablaAsme)

                            ctx.Entry(tabla).State = oldTabla.Count(x => x.TablaAsmeId == tabla.TablaAsmeId) > 0 ? EntityState.Modified : EntityState.Added;


                        oldTabla.ToList()
                               .ForEach(x =>
                               {
                                   if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                               });

                        /***cargos***/

                        if (obj.ProcedimientoCargos != null)
                        {
                            var oldTablaCargo = ctx.ProcedimientoCargos.Where(x => x.ProcedimientoId == obj.ProcedimientoId);

                            //AsNoTracking().Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                            foreach (ProcedimientoCargos tablacargos in obj.ProcedimientoCargos)

                                ctx.Entry(tablacargos).State = oldTablaCargo.Count(x => x.ProcedimientoCargosID == tablacargos.ProcedimientoCargosID) > 0 ? EntityState.Modified : EntityState.Added;


                            oldTablaCargo.ToList()
                                   .ForEach(x =>
                                   {
                                       if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                                   });
                        }
                        /***cargosape***/
                        if (obj.ProcedimientoCargosApe != null)
                        {
                            var oldTablaCargoApe = ctx.ProcedimientoCargosApe.Where(x => x.ProcedimientoId == obj.ProcedimientoId);

                            //AsNoTracking().Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                            foreach (ProcedimientoCargosApe tablacargosApe in obj.ProcedimientoCargosApe)

                                ctx.Entry(tablacargosApe).State = oldTablaCargoApe.Count(x => x.ProcedimientoCargosApeID == tablacargosApe.ProcedimientoCargosApeID) > 0 ? EntityState.Modified : EntityState.Added;


                            oldTablaCargoApe.ToList()
                                   .ForEach(x =>
                                   {
                                       if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                                   });
                        }
                        /***Fin cargos***/


                        /***cargosotros***/
                        if (obj.ProcedimientoCargosOtros != null)
                        {
                            var oldTablaCargoOtros = ctx.ProcedimientoCargosOtros.Where(x => x.ProcedimientoId == obj.ProcedimientoId);

                            //AsNoTracking().Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                            foreach (ProcedimientoCargosOtros tablacargosOtros in obj.ProcedimientoCargosOtros)

                                ctx.Entry(tablacargosOtros).State = oldTablaCargoOtros.Count(x => x.ProcedimientoCargosOtrosID == tablacargosOtros.ProcedimientoCargosOtrosID) > 0 ? EntityState.Modified : EntityState.Added;


                            oldTablaCargoOtros.ToList()
                                   .ForEach(x =>
                                   {
                                       if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                                   });
                        }
                        /***Fin cargos***/


                        var oldBL = ctx.BaseLegalNorma.Where(x => x.BaseLegalId == obj.BaseLegalId);
                        foreach (BaseLegalNorma bln in obj.BaseLegal.BaseLegalNorma)
                            ctx.Entry(bln).State = oldBL.Count(x => x.BaseLegalNormaId == bln.BaseLegalNormaId) > 0 ? EntityState.Modified : EntityState.Added;
                        oldBL.ToList()
                            .ForEach(x =>
                            {
                                if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                            });
                    }

                //if (obj.Es_Copia)
                //    ctx.Entry(obj).State = EntityState.Detached;
                //else

                if (obj.ProcedimientoId > 0)
                {
                    ctx.Entry(obj).State = EntityState.Modified;
                }
                else
                {
                    ctx.Entry(obj).State = EntityState.Added;
                }

                //ctx.Entry(obj).State = obj.ProcedimientoId > 0 ? EntityState.Modified : EntityState.Added;

            }
            catch (Exception ex)
            {
                if (obj.Es_Copia)
                {
                    Procedimiento pr = GetOne(x => x.ProcedimientoId == obj.ProcedimientoId);
                    pr.DatosGeneralesTerminado = true;
                    pr.SustTecnicoTerminado = false;
                    pr.SustLegalTerminado = true;
                    SaveOnlyProcedimiento(pr);
                }
                DisplayTrackedEntities();
                throw ex;
            }
        }

        public void Savemodalidad(Procedimiento obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                if (!obj.Es_Copia)
                    if (obj.ProcedimientoId > 0)
                    {

                        var oldTabla = ctx.TablaAsme.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                        foreach (TablaAsme tabla in obj.TablaAsme)

                            ctx.Entry(tabla).State = oldTabla.Count(x => x.TablaAsmeId == tabla.TablaAsmeId) > 0 ? EntityState.Modified : EntityState.Added;


                        oldTabla.ToList()
                               .ForEach(x =>
                               {
                                   if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                               });

                    }


                if (obj.ProcedimientoId > 0)
                {
                    ctx.Entry(obj).State = EntityState.Modified;
                }
                else
                {
                    ctx.Entry(obj).State = EntityState.Added;
                }

                //ctx.Entry(obj).State = obj.ProcedimientoId > 0 ? EntityState.Modified : EntityState.Added;

            }
            catch (Exception ex)
            {
                if (obj.Es_Copia)
                {
                    Procedimiento pr = GetOne(x => x.ProcedimientoId == obj.ProcedimientoId);
                    pr.DatosGeneralesTerminado = true;
                    pr.SustTecnicoTerminado = false;
                    pr.SustLegalTerminado = true;
                    SaveOnlyProcedimiento(pr);
                }
                DisplayTrackedEntities();
                throw ex;
            }
        }

        public void SaveInformacionCiudadano(Procedimiento obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                if (obj.ProcedimientoId > 0)
                {


                    var oldSede = ctx.ProcedimientoSede.Where(x => x.ProcedimientoId == obj.ProcedimientoId).AsNoTracking();
                    foreach (ProcedimientoSede sede in obj.ProcedimientoSede)
                        ctx.Entry(sede).State = EntityState.Added;
                    oldSede.Where(x => x.ProcedimientoId == obj.ProcedimientoId).ToList()
                        .ForEach(x =>
                        {
                            if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                        });

                    var oldUndOrgSedeProcedimiento = ctx.UndOrgRecepcionDocumentos.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                    foreach (ProcedimientoSede sede in obj.ProcedimientoSede)
                        if (sede.UndOrgRecepcionDocumentos != null)
                            foreach (UndOrgRecepcionDocumentos r in sede.UndOrgRecepcionDocumentos)
                                ctx.Entry(r).State = EntityState.Added;
                    oldUndOrgSedeProcedimiento.ToList()
                        .ForEach(x =>
                        {
                            if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                        });


                    var oldNota = ctx.NotaCiudadano.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                    foreach (NotaCiudadano nota in obj.NotaCiudadano)
                        ctx.Entry(nota).State = EntityState.Added;
                    oldNota.Where(x => x.ProcedimientoId == obj.ProcedimientoId).ToList()
                        .ForEach(x =>
                        {
                            if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                        });

                    var oldPasoSeguir = ctx.PasoSeguir.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                    foreach (PasoSeguir paso in obj.PasoSeguir)
                    {
                        var r = oldPasoSeguir.Where(x => x.PasoSeguirId == paso.PasoSeguirId).Count() > 0 ? EntityState.Modified : EntityState.Added;
                        //ctx.Entry(paso).State = EntityState.Added;
                    }

                    oldPasoSeguir.ToList()
                        .ForEach(x =>
                        {

                            if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                        });

                    var oldDatoAdicional = ctx.ProcedimientoDatoAdicional.Where(x => x.ProcedimientoId == obj.ProcedimientoId);

                    if (oldDatoAdicional.Count() != 0)
                    {
                        foreach (ProcedimientoDatoAdicional da in obj.ProcedimientoDatoAdicional)
                            ctx.Entry(da).State = EntityState.Added;
                        oldDatoAdicional.ToList()
                            .ForEach(x =>
                            {
                                if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                            });
                    }



                    var oldTabla = ctx.TablaAsme.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                    foreach (TablaAsme tabla in obj.TablaAsme)
                        ctx.Entry(tabla).State = oldTabla.Count(x => x.TablaAsmeId == tabla.TablaAsmeId) > 0 ? EntityState.Modified : EntityState.Added;
                    oldTabla.ToList()
                        .ForEach(x =>
                        {
                            if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                        });

                    //var oldBL = ctx.BaseLegalNorma.Where(x => x.BaseLegalId == obj.BaseLegalId);
                    //foreach (BaseLegalNorma bln in obj.BaseLegal.BaseLegalNorma)
                    //    //ctx.Entry(bln).State = oldBL.Count(x => x.BaseLegalNormaId == bln.BaseLegalNormaId) > 0 ? EntityState.Modified : EntityState.Added;
                    //    ctx.Entry(bln).State = EntityState.Added;
                    //oldBL.ToList()
                    //    .ForEach(x =>
                    //    {
                    //        if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                    //    });
                }


                if (obj.Es_Copia)
                    ctx.Entry(obj).State = EntityState.Detached;
                else
                    ctx.Entry(obj).State = obj.ProcedimientoId > 0 ? EntityState.Modified : EntityState.Added;

            }
            catch (Exception ex)
            {

                if (obj.Es_Copia)
                {
                    Procedimiento pr = GetOne(x => x.ProcedimientoId == obj.ProcedimientoId);
                    pr.DatosGeneralesTerminado = true;
                    pr.SustTecnicoTerminado = false;
                    pr.SustLegalTerminado = true;
                    SaveOnlyProcedimiento(pr);
                }


                DisplayTrackedEntities();
                throw ex;
            }
        }

        public void SaveTablaAsme(Procedimiento obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                if (obj.ProcedimientoId > 0)
                {

                    var oldTabla = ctx.TablaAsme.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                    foreach (TablaAsme tabla in obj.TablaAsme)
                        ctx.Entry(tabla).State = oldTabla.Count(x => x.TablaAsmeId == tabla.TablaAsmeId) > 0 ? EntityState.Modified : EntityState.Added;
                    oldTabla.ToList()
                        .ForEach(x =>
                        {
                            if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                        });

                }

                ctx.Entry(obj).State = obj.ProcedimientoId > 0 ? EntityState.Modified : EntityState.Added;

            }
            catch (Exception ex)
            {

                DisplayTrackedEntities();
                throw ex;
            }
        }

        public void SaveInformacionBasica(Procedimiento obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;


                var oldSede = ctx.ProcedimientoSede.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                foreach (ProcedimientoSede sede in obj.ProcedimientoSede)
                    ctx.Entry(sede).State = EntityState.Unchanged;


                var oldNota = ctx.NotaCiudadano.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                foreach (NotaCiudadano nota in obj.NotaCiudadano)
                    ctx.Entry(nota).State = EntityState.Unchanged;


                var oldPasoSeguir = ctx.PasoSeguir.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                foreach (PasoSeguir paso in obj.PasoSeguir)
                    ctx.Entry(paso).State = EntityState.Unchanged;


                var oldDatoAdicional = ctx.ProcedimientoDatoAdicional.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                foreach (ProcedimientoDatoAdicional da in obj.ProcedimientoDatoAdicional)
                    ctx.Entry(da).State = EntityState.Unchanged;



                var oldReq = ctx.Requisito
                                .Include(x => x.RequisitoFormulario)
                                .Where(x => x.ProcedimientoId == obj.ProcedimientoId)
                                .AsNoTracking()
                                .ToList();

                foreach (Requisito nReq in obj.Requisito)
                {
                    if (nReq.RequisitoId != 0)
                    {
                        var listForm = oldReq.Single(x => x.RequisitoId == nReq.RequisitoId).RequisitoFormulario;
                        foreach (RequisitoFormulario nForm in nReq.RequisitoFormulario)
                        {
                            ctx.Entry(nForm).State = listForm.Count(x => x.FormularioId == nForm.FormularioId) > 0 ? EntityState.Modified : EntityState.Added;
                        }

                        foreach (RequisitoFormulario oForm in listForm.Where(x => nReq.RequisitoFormulario.Count(f => f.FormularioId == x.FormularioId) == 0))
                        {
                            oForm.Requisito = null;
                            ctx.Entry(oForm).State = EntityState.Deleted;
                        }

                        ctx.Entry(nReq).State = EntityState.Modified;
                    }
                    else
                    {
                        nReq.BaseLegal = new BaseLegal();
                        ctx.Entry(nReq).State = EntityState.Added;
                    }

                }

                oldReq.Where(x => obj.Requisito.Count(r => r.RequisitoId == x.RequisitoId) == 0)
                    .ToList()
                    .ForEach(x =>
                    {
                        ctx.Entry(x).State = EntityState.Deleted;
                    });

                var oldTabla = ctx.TablaAsme.Where(x => x.ProcedimientoId == obj.ProcedimientoId);
                foreach (TablaAsme tabla in obj.TablaAsme)
                    ctx.Entry(tabla).State = oldTabla.Count(x => x.TablaAsmeId == tabla.TablaAsmeId) > 0 ? EntityState.Modified : EntityState.Added;
                oldTabla.ToList()
                    .ForEach(x =>
                    {
                        if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                    });

                var oldBL = ctx.BaseLegalNorma.Where(x => x.BaseLegalId == obj.BaseLegalId);
                foreach (BaseLegalNorma bln in obj.BaseLegal.BaseLegalNorma)
                    ctx.Entry(bln).State = oldBL.Count(x => x.BaseLegalNormaId == bln.BaseLegalNormaId) > 0 ? EntityState.Modified : EntityState.Added;
                oldBL.ToList()
                    .ForEach(x =>
                    {
                        if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                    });


                ctx.Entry(obj).State = obj.ProcedimientoId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                DisplayTrackedEntities();
                throw ex;
            }
        }

        public List<string> CopiarDatosProcedimiento(long ProcedimientoOrigenId, long ProcedimientoDestinoId, List<DatoCopia> lstCopia, string CodigoCopia, string CodigoCortoCopia)
        {
            {
                List<string> mensajes = new List<string>();
                try
                {
                    SutContext ctx = Context.GetContext() as SutContext;
                    Procedimiento pOrigen = GetNoTracking(new List<long>() { ProcedimientoOrigenId }).First();
                    Procedimiento pDestino = GetOne(ProcedimientoDestinoId);

                    //ctx.Entry(pDestino).State = EntityState.Modified;
                    //pDestino = new Procedimiento();

                    pDestino.CodigoCorto = CodigoCortoCopia;
                    pDestino.Codigo = CodigoCopia;
                    pDestino.Denominacion = "Copia desde el procedimiento con codigo " + pOrigen.CodigoCorto;
                    pDestino.ExpedienteId = pOrigen.ExpedienteId;


                    pDestino.Calificacion = pOrigen.Calificacion;
                    pDestino.PlazoAtencion = pOrigen.PlazoAtencion;
                    pDestino.Objetivo = string.Empty;

                    pDestino.CargoReconsideracion = string.Empty;
                    pDestino.CargoApelacion = string.Empty;
                    pDestino.UndOrgReconsideracionId = null;
                    pDestino.UndOrgApelacionId = null;
                    pDestino.PzoReconPresent = 0;
                    pDestino.PzoApelPresent = 0;
                    pDestino.PzoReconResol = 0;
                    pDestino.PzoApelResol = 0;
                    pDestino.PlazoAtencion = 0;
                    pDestino.Calificacion = 0;
                    pDestino.SustTecnicoTerminado = false;
                    pDestino.DatosGeneralesTerminado = false;


                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.InfoAlCiudadano))
                    {

                        if (pDestino.ProcedimientoDatoAdicional == null) pDestino.ProcedimientoDatoAdicional = new List<ProcedimientoDatoAdicional>();
                        foreach (ProcedimientoDatoAdicional da in pOrigen.ProcedimientoDatoAdicional)
                        {
                            if (pDestino.ProcedimientoDatoAdicional.Count(x => x.MetaDatoId == da.MetaDatoId) == 0)
                            {
                                pDestino.ProcedimientoDatoAdicional.Add(new ProcedimientoDatoAdicional()
                                {
                                    MetaDatoId = da.MetaDatoId,
                                    Comentario = da.Comentario,
                                    ProcedimientoId = pDestino.ProcedimientoId
                                });
                            }
                        }
                        if (pDestino.ProcedimientoSede == null) pDestino.ProcedimientoSede = new List<ProcedimientoSede>();
                        foreach (ProcedimientoSede sede in pOrigen.ProcedimientoSede)
                        {
                            if (pDestino.ProcedimientoSede.Count(x => x.SedeId == sede.SedeId) == 0)
                            {
                                pDestino.ProcedimientoSede.Add(new ProcedimientoSede()
                                {
                                    SedeId = sede.SedeId,
                                    ProcedimientoId = pDestino.ProcedimientoId
                                });
                            }
                        }
                        if (pDestino.ProcedimientoSede.Count() > 0)
                        {
                            foreach (ProcedimientoSede s in pDestino.ProcedimientoSede)
                            {
                                var obj = pOrigen.ProcedimientoSede.Where(x => x.SedeId == s.SedeId).FirstOrDefault();

                                if (obj != null)
                                {
                                    if (obj.UndOrgRecepcionDocumentos != null)
                                        s.UndOrgRecepcionDocumentos = obj.UndOrgRecepcionDocumentos;
                                }
                                else
                                {
                                    s.UndOrgRecepcionDocumentos = new List<UndOrgRecepcionDocumentos>();
                                }
                            }
                        }

                        pDestino.ProcedimientoSede.ForEach(r => r.ProcedimientoId = pDestino.ProcedimientoId);
                        pDestino.ProcedimientoSede.ForEach(r => r.UndOrgRecepcionDocumentos.ForEach(t => t.ProcedimientoId = pDestino.ProcedimientoId));


                        if (pDestino.NotaCiudadano == null) pDestino.NotaCiudadano = new List<NotaCiudadano>();
                        foreach (NotaCiudadano nc in pOrigen.NotaCiudadano)
                        {
                            pDestino.NotaCiudadano.Add(new NotaCiudadano()
                            {
                                NotaCiudadanoId = pDestino.NotaCiudadano.Count() + 1,
                                Nota = nc.Nota,
                                TipoNotaId = nc.TipoNotaId,
                                ProcedimientoId = pDestino.ProcedimientoId
                            });
                        }
                        if (pDestino.PasoSeguir == null) pDestino.PasoSeguir = new List<PasoSeguir>();
                        foreach (PasoSeguir ps in pOrigen.PasoSeguir)
                        {
                            pDestino.PasoSeguir.Add(new PasoSeguir()
                            {
                                PasoSeguirId = pDestino.PasoSeguir.Count() + 1,
                                Descripcion = ps.Descripcion,
                                ProcedimientoId = pDestino.ProcedimientoId
                            });
                        }

                        pDestino.ProcedimientoDatoAdicional.ForEach(x => ctx.Entry(x).State = EntityState.Added);
                        pDestino.ProcedimientoSede.ForEach(x => ctx.Entry(x).State = EntityState.Added);
                        pDestino.ProcedimientoSede.ForEach(x => x.UndOrgRecepcionDocumentos.ForEach(y => ctx.Entry(y).State = EntityState.Added));
                        pDestino.NotaCiudadano.ForEach(x => ctx.Entry(x).State = EntityState.Added);
                        pDestino.PasoSeguir.ForEach(x => ctx.Entry(x).State = EntityState.Added);


                        pDestino.UndOrgPresentDocumentacionId = pOrigen.UndOrgPresentDocumentacionId;
                        pDestino.Telefono = pOrigen.Telefono;
                        pDestino.Anexo = pOrigen.Anexo;
                        pDestino.Correo = pOrigen.Correo;
                        pDestino.EsGratuito = pOrigen.EsGratuito;
                    }
                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.InfoBasica))
                    {
                        pDestino.Calificacion = pOrigen.Calificacion;
                        pDestino.PlazoAtencion = pOrigen.PlazoAtencion;
                        pDestino.SustTecCalificacion = pOrigen.SustTecCalificacion;
                        pDestino.Objetivo = pOrigen.Objetivo;

                        pDestino.CargoReconsideracion = pOrigen.CargoReconsideracion;
                        pDestino.CargoApelacion = pOrigen.CargoApelacion;
                        pDestino.UndOrgReconsideracionId = pOrigen.UndOrgReconsideracionId;
                        pDestino.UndOrgApelacionId = pOrigen.UndOrgApelacionId;
                        pDestino.PzoReconPresent = pOrigen.PzoReconPresent;
                        pDestino.PzoApelPresent = pOrigen.PzoApelPresent;
                        pDestino.PzoReconResol = pOrigen.PzoReconResol;
                        pDestino.PzoApelResol = pOrigen.PzoApelResol;
                    }

                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.TipoAtencion))
                    {

                        for (int i = 0; i < pOrigen.TablaAsme.Count(); i++)
                        {

                            TablaAsme tabla = pOrigen.TablaAsme[i];

                            if (tabla.Codigo == null)
                            {
                                tabla.Codigo = "-";
                            }


                            if (tabla.Codigo.Equals("-"))
                            {
                                List<Actividad> lstAct = new List<Actividad>();
                                foreach (Actividad t in tabla.Actividad)
                                {
                                    Actividad act = new Actividad();

                                    act.Descripcion = t.Descripcion;
                                    act.Duracion = t.Duracion;
                                    act.Orden = t.Orden;
                                    act.TipoActividad = t.TipoActividad;
                                    act.TipoValor = t.TipoValor;
                                    act.UnidadOrganicaId = t.UnidadOrganicaId;
                                    if (t.ActividadRecurso != null)
                                    {
                                        act.ActividadRecurso = t.ActividadRecurso.Select(x => new ActividadRecurso()
                                        {
                                            Cantidad = x.Cantidad,
                                            RecursoId = x.RecursoId
                                        }).ToList();
                                    }

                                    lstAct.Add(act);
                                    //if (t.ActividadRecurso != null)
                                    //{
                                    //    List<ActividadRecurso> list = t.ActividadRecurso;
                                    //    foreach (ActividadRecurso ar in list)
                                    //    {
                                    //        act.ActividadRecurso.Add(new ActividadRecurso()
                                    //        {
                                    //            Cantidad = ar.Cantidad,
                                    //            RecursoId = ar.RecursoId
                                    //        });
                                    //    }
                                    //}
                                }

                                pDestino.TablaAsme.Add(new TablaAsme()
                                {
                                    ProcedimientoId = pDestino.ProcedimientoId,
                                    Codigo = tabla.Codigo,
                                    Descripcion = tabla.Descripcion,
                                    Prestaciones = tabla.Prestaciones,
                                    Personal = tabla.Personal,
                                    MaterialFungible = tabla.MaterialFungible,
                                    ServicioIdentificable = tabla.ServicioIdentificable,
                                    MaterialNoFungible = tabla.MaterialNoFungible,
                                    ServicioTerceros = tabla.ServicioTerceros,
                                    Depreciacion = tabla.Depreciacion,
                                    Fijos = tabla.Fijos,
                                    CostoUnitario = tabla.CostoUnitario,
                                    Subvencion = tabla.Subvencion,
                                    DerechoTramitacion = tabla.DerechoTramitacion,
                                    EsGratuito = tabla.EsGratuito,
                                    Terminado = tabla.Terminado,
                                    //ComentarioSeccionId = tabla.ComentarioSeccionId,
                                    //Comentarios = tabla.Comentarios,
                                    UserCreacion = null,
                                    FecCreacion = null,
                                    Actividad = lstAct,
                                    AsmeActual = tabla.AsmeActual,
                                    PctSubvencion = tabla.PctSubvencion,
                                    UITID = tabla.UITID

                                });

                            }
                            else
                            {
                                pDestino.TablaAsme[0].Descripcion = tabla.Descripcion;
                                pDestino.TablaAsme[0].Prestaciones = tabla.Prestaciones;
                                pDestino.TablaAsme[0].Personal = tabla.Personal;
                                pDestino.TablaAsme[0].MaterialFungible = tabla.MaterialFungible;
                                pDestino.TablaAsme[0].ServicioIdentificable = tabla.ServicioIdentificable;
                                pDestino.TablaAsme[0].MaterialNoFungible = tabla.MaterialNoFungible;
                                pDestino.TablaAsme[0].ServicioTerceros = tabla.ServicioTerceros;
                                pDestino.TablaAsme[0].Depreciacion = tabla.Depreciacion;
                                pDestino.TablaAsme[0].Fijos = tabla.Fijos;
                                pDestino.TablaAsme[0].CostoUnitario = tabla.CostoUnitario;
                                pDestino.TablaAsme[0].Subvencion = tabla.Subvencion;
                                pDestino.TablaAsme[0].DerechoTramitacion = tabla.DerechoTramitacion;
                                pDestino.TablaAsme[0].EsGratuito = tabla.EsGratuito;
                                pDestino.TablaAsme[0].Terminado = tabla.Terminado;
                                //pDestino.TablaAsme[0].ComentarioSeccionId = tabla.ComentarioSeccionId;
                                //pDestino.TablaAsme[0].Comentarios = tabla.Comentarios;
                                pDestino.TablaAsme[0].UserCreacion = null;
                                pDestino.TablaAsme[0].FecCreacion = null;
                                pDestino.TablaAsme[0].AsmeActual = tabla.AsmeActual;
                                pDestino.TablaAsme[0].PctSubvencion = tabla.PctSubvencion;
                                pDestino.TablaAsme[0].UITID = tabla.UITID;

                                pDestino.TablaAsme[0].Actividad = new List<Actividad>();

                                if (tabla.Actividad != null)
                                {
                                    foreach (Actividad a in tabla.Actividad)
                                    {
                                        pDestino.TablaAsme[0].Actividad.Add(new Actividad()
                                        {
                                            Descripcion = a.Descripcion,
                                            Duracion = a.Duracion,
                                            Orden = a.Orden,
                                            TipoActividad = a.TipoActividad,
                                            TipoValor = a.TipoValor,
                                            UnidadOrganicaId = a.UnidadOrganicaId,
                                            ActividadRecurso = a.ActividadRecurso == null ? new List<ActividadRecurso>() : a.ActividadRecurso.Select(x => new ActividadRecurso()
                                            {
                                                Cantidad = x.Cantidad,
                                                RecursoId = x.RecursoId
                                            }).ToList()
                                        });
                                    }
                                }


                                ctx.Entry(pDestino.TablaAsme[0]).State = EntityState.Modified;
                            }
                        }

                        pDestino.TablaAsme
                                       .ForEach(x =>
                                       {
                                           if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                                       });

                    }

                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.Requisito))
                    {
                        if (pDestino.Requisito == null) pDestino.Requisito = new List<Requisito>();

                        for (int i = 0; i < pOrigen.Requisito.Count(); i++)
                        {
                            Requisito req = new Requisito();

                            req.Descripcion = pOrigen.Requisito[i].Descripcion;
                            req.CadenaTramite = pOrigen.Requisito[i].CadenaTramite;
                            req.Nombre = pOrigen.Requisito[i].Nombre;
                            req.SustentoLegal = pOrigen.Requisito[i].SustentoLegal;
                            req.SustentoTecnico = pOrigen.Requisito[i].SustentoTecnico;
                            req.ProcedimientoId = ProcedimientoDestinoId;
                            req.TipoRequisito = pOrigen.Requisito[i].TipoRequisito;
                            req.RecNum = pOrigen.Requisito[i].RecNum;

                            if (pOrigen.Requisito[i].BaseLegal != null)
                            {

                                if (pOrigen.Requisito[i].BaseLegal.BaseLegalNorma != null)
                                {
                                    req.BaseLegal = new BaseLegal();
                                    req.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>();

                                    for (int x = 0; x < pOrigen.Requisito[i].BaseLegal.BaseLegalNorma.Count(); x++)
                                    {
                                        BaseLegalNorma bln = new BaseLegalNorma();

                                        bln.NormaId = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].NormaId;
                                        bln.ArchivoAdjuntoId = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].ArchivoAdjuntoId;
                                        bln.Articulo = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].Articulo;
                                        bln.Descripcion = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].Descripcion;
                                        bln.FechaPublicacion = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].FechaPublicacion;
                                        bln.Numero = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].Numero;
                                        bln.TipoBaseLegal = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].TipoBaseLegal;
                                        bln.TipoNormaId = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].TipoNormaId;
                                        bln.Url = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].Url;
                                        bln.EstadoACR = "0";

                                        req.BaseLegal.BaseLegalNorma.Add(bln);
                                    }
                                }
                            }

                            pDestino.Requisito.Add(req);

                            int idx = pDestino.Requisito.Count();
                            pDestino.Requisito[idx - 1].RequisitoFormulario = new List<RequisitoFormulario>();


                            if (pOrigen.Requisito[i].RequisitoFormulario != null)
                            {
                                if (pOrigen.Requisito[i].RequisitoFormulario.Count() > 0)
                                {
                                    for (int x = 0; x < pOrigen.Requisito[i].RequisitoFormulario.Count(); x++)
                                    {
                                        RequisitoFormulario rf = new RequisitoFormulario();
                                        rf.Nombre = pOrigen.Requisito[i].RequisitoFormulario[x].Nombre;
                                        rf.Url = pOrigen.Requisito[i].RequisitoFormulario[x].Url;
                                        rf.ArchivoAdjuntoId = pOrigen.Requisito[i].RequisitoFormulario[x].ArchivoAdjuntoId;
                                        rf.FormularioId = pOrigen.Requisito[i].RequisitoFormulario[x].FormularioId;
                                        pDestino.Requisito[idx - 1].RequisitoFormulario.Add(rf);
                                    }
                                }
                            }
                        }


                        pDestino.Requisito.ForEach(x =>
                        {
                            x.ProcedimientoId = pDestino.ProcedimientoId;
                            ctx.Entry(x).State = EntityState.Added;
                            x.BaseLegal.BaseLegalNorma.ForEach(y => ctx.Entry(y).State = EntityState.Added);
                            x.RequisitoFormulario.ForEach(y => ctx.Entry(y).State = EntityState.Added);
                        });
                    }
                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.BaseLegal))
                    {
                        if (pDestino.BaseLegal == null) pDestino.BaseLegal = new BaseLegal();
                        if (pDestino.BaseLegal.BaseLegalNorma == null) pDestino.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>();



                        if (pOrigen.BaseLegal != null)
                        {
                            if (pOrigen.BaseLegal.BaseLegalNorma != null)
                            {

                                //ctx.Entry(pDestino.BaseLegal).State = EntityState.Added;

                                for (int x = 0; x < pOrigen.BaseLegal.BaseLegalNorma.Count(); x++)
                                {
                                    BaseLegalNorma bln = new BaseLegalNorma();

                                    bln.NormaId = pOrigen.BaseLegal.BaseLegalNorma[x].NormaId;
                                    bln.ArchivoAdjuntoId = pOrigen.BaseLegal.BaseLegalNorma[x].ArchivoAdjuntoId;
                                    bln.Articulo = pOrigen.BaseLegal.BaseLegalNorma[x].Articulo;
                                    bln.Descripcion = pOrigen.BaseLegal.BaseLegalNorma[x].Descripcion;
                                    bln.FechaPublicacion = pOrigen.BaseLegal.BaseLegalNorma[x].FechaPublicacion;
                                    bln.Numero = pOrigen.BaseLegal.BaseLegalNorma[x].Numero;
                                    bln.TipoBaseLegal = pOrigen.BaseLegal.BaseLegalNorma[x].TipoBaseLegal;
                                    bln.TipoNormaId = pOrigen.BaseLegal.BaseLegalNorma[x].TipoNormaId;
                                    bln.Url = pOrigen.BaseLegal.BaseLegalNorma[x].Url;
                                    bln.BaseLegalId = pDestino.BaseLegal.BaseLegalId;
                                    bln.EstadoACR = pOrigen.BaseLegal.BaseLegalNorma[x].EstadoACR;
                                    bln.BaseLegalNormaId = x + 2;

                                    pDestino.BaseLegal.BaseLegalNorma.Add(bln);

                                    //ctx.Entry(pDestino.BaseLegal.BaseLegalNorma).State = EntityState.Added;
                                }
                            }
                        }

                        pDestino.BaseLegal.BaseLegalNorma.ForEach(x =>
                        {
                            ctx.Entry(x).State = EntityState.Added;
                            x.BaseLegal.BaseLegalNorma.ForEach(y => ctx.Entry(y).State = EntityState.Added);
                        });
                    }
                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.DatosGeneralesYSTL))
                    {
                        if (!lstCopia.Contains(DatoCopia.Todo) && !lstCopia.Contains(DatoCopia.InfoBasica))
                        {
                            pDestino.Calificacion = pOrigen.Calificacion;
                            pDestino.PlazoAtencion = pOrigen.PlazoAtencion;

                            pDestino.CargoReconsideracion = pOrigen.CargoReconsideracion;
                            pDestino.CargoApelacion = pOrigen.CargoApelacion;
                            pDestino.UndOrgReconsideracionId = pOrigen.UndOrgReconsideracionId;
                            pDestino.UndOrgApelacionId = pOrigen.UndOrgApelacionId;
                            pDestino.PzoReconPresent = pOrigen.PzoReconPresent;
                            pDestino.PzoApelPresent = pOrigen.PzoApelPresent;
                            pDestino.PzoReconResol = pOrigen.PzoReconResol;
                            pDestino.PzoApelResol = pOrigen.PzoApelResol;
                        }
                        if (!lstCopia.Contains(DatoCopia.Todo) && !lstCopia.Contains(DatoCopia.InfoAlCiudadano))
                        {
                            pDestino.UndOrgPresentDocumentacionId = pOrigen.UndOrgPresentDocumentacionId;
                            pDestino.Telefono = pOrigen.Telefono;
                            pDestino.Anexo = pOrigen.Anexo;
                            pDestino.Correo = pOrigen.Correo;

                            if (pDestino.ProcedimientoDatoAdicional == null) pDestino.ProcedimientoDatoAdicional = new List<ProcedimientoDatoAdicional>();
                            foreach (ProcedimientoDatoAdicional da in pOrigen.ProcedimientoDatoAdicional)
                            {
                                if (pDestino.ProcedimientoDatoAdicional.Count(x => x.MetaDatoId == da.MetaDatoId) == 0)
                                {
                                    pDestino.ProcedimientoDatoAdicional.Add(new ProcedimientoDatoAdicional()
                                    {
                                        MetaDatoId = da.MetaDatoId,
                                        Comentario = da.Comentario
                                    });
                                }
                            }
                            if (pDestino.ProcedimientoSede == null) pDestino.ProcedimientoSede = new List<ProcedimientoSede>();
                            foreach (ProcedimientoSede sede in pOrigen.ProcedimientoSede)
                            {
                                if (pDestino.ProcedimientoSede.Count(x => x.SedeId == sede.SedeId) == 0)
                                {
                                    pDestino.ProcedimientoSede.Add(new ProcedimientoSede()
                                    {
                                        SedeId = sede.SedeId
                                    });
                                }
                            }
                            if (pDestino.NotaCiudadano == null) pDestino.NotaCiudadano = new List<NotaCiudadano>();
                            foreach (NotaCiudadano nc in pOrigen.NotaCiudadano)
                            {
                                pDestino.NotaCiudadano.Add(new NotaCiudadano()
                                {
                                    NotaCiudadanoId = pDestino.NotaCiudadano.Count() + 1,
                                    Nota = nc.Nota,
                                    TipoNotaId = nc.TipoNotaId
                                });
                            }
                            if (pDestino.PasoSeguir == null) pDestino.PasoSeguir = new List<PasoSeguir>();
                            foreach (PasoSeguir ps in pOrigen.PasoSeguir)
                            {
                                pDestino.PasoSeguir.Add(new PasoSeguir()
                                {
                                    PasoSeguirId = pDestino.PasoSeguir.Count() + 1,
                                    Descripcion = ps.Descripcion
                                });
                            }
                        }
                    }
                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.SustentoCalificacion))
                    {
                        pDestino.SustTecCalificacion = pOrigen.SustTecCalificacion;
                    }
                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.SustentoRequisito))
                    {
                        if (!lstCopia.Contains(DatoCopia.Todo) && !lstCopia.Contains(DatoCopia.Requisito))
                        {
                            if (pDestino.Requisito == null) pDestino.Requisito = new List<Requisito>();
                            foreach (Requisito req in pOrigen.Requisito)
                            {
                                pDestino.Requisito.Add(new Requisito()
                                {
                                    Descripcion = req.Descripcion,
                                    CadenaTramite = req.CadenaTramite,
                                    Nombre = req.Nombre,
                                    SustentoLegal = req.SustentoLegal,
                                    SustentoTecnico = req.SustentoTecnico,
                                    BaseLegal = new BaseLegal()
                                    {
                                        BaseLegalNorma = new List<BaseLegalNorma>()
                                    }
                                });
                                foreach (BaseLegalNorma bl in req.BaseLegal.BaseLegalNorma)
                                {
                                    req.BaseLegal.BaseLegalNorma.Add(new BaseLegalNorma()
                                    {
                                        NormaId = bl.NormaId,
                                        ArchivoAdjuntoId = bl.ArchivoAdjuntoId,
                                        Articulo = bl.Articulo,
                                        Descripcion = bl.Descripcion,
                                        FechaPublicacion = bl.FechaPublicacion,
                                        Numero = bl.Numero,
                                        TipoBaseLegal = bl.TipoBaseLegal,
                                        TipoNormaId = bl.TipoNormaId,
                                        EstadoACR = bl.EstadoACR,
                                        Url = bl.Url
                                    });
                                }
                            }
                            pDestino.Requisito.ForEach(x =>
                            {
                                x.ProcedimientoId = pDestino.ProcedimientoId;
                                ctx.Entry(x).State = EntityState.Added;
                                x.BaseLegal.BaseLegalNorma.ForEach(y => ctx.Entry(y).State = EntityState.Added);
                            });
                        }
                    }
                    //if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.TablaASME))
                    //{
                    //    if (pDestino.TablaAsme[0].Actividad == null) pDestino.TablaAsme[0].Actividad = new List<Actividad>();

                    //    if (pOrigen.TablaAsme[0].Actividad != null)
                    //        foreach (Actividad a in pOrigen.TablaAsme[0].Actividad)
                    //        {
                    //            pDestino.TablaAsme[0].Actividad.Add(new Actividad()
                    //            {
                    //                Descripcion = a.Descripcion,
                    //                Duracion = a.Duracion,
                    //                Orden = pOrigen.TablaAsme[0].Actividad.Count() + 1,
                    //                TipoActividad = a.TipoActividad,
                    //                TipoValor = a.TipoValor,
                    //                UnidadOrganicaId = a.UnidadOrganicaId,
                    //                ActividadRecurso = a.ActividadRecurso.Select(x => new ActividadRecurso()
                    //                {
                    //                    Cantidad = x.Cantidad,
                    //                    RecursoId = x.RecursoId
                    //                }).ToList()
                    //            });

                    //        }
                    //}
                    ctx.Entry(pDestino).State = EntityState.Modified;
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                    mensajes.Add(ex.Message);
                }
                return mensajes;
            }
        }


        public List<string> CopiarDatosProcedimientoEli(long ProcedimientoOrigenId, long ProcedimientoDestinoId, List<DatoCopia> lstCopia, string CodigoCopia, string CodigoCortoCopia)
        {
            {
                List<string> mensajes = new List<string>();
                try
                {
                    SutContext ctx = Context.GetContext() as SutContext;
                    Procedimiento pOrigen = GetNoTracking(new List<long>() { ProcedimientoOrigenId }).First();
                    Procedimiento pDestino = GetOne(ProcedimientoDestinoId);

                    //ctx.Entry(pDestino).State = EntityState.Modified;
                    //pDestino = new Procedimiento();

                    pDestino.CodigoCorto = CodigoCortoCopia;
                    pDestino.Codigo = CodigoCopia;
                    pDestino.Denominacion = "Copia desde el procedimiento con codigo " + pOrigen.CodigoCorto;
                    //pDestino.ExpedienteId = pOrigen.ExpedienteId;


                    pDestino.Calificacion = pOrigen.Calificacion;
                    pDestino.PlazoAtencion = pOrigen.PlazoAtencion;
                    pDestino.Objetivo = string.Empty;

                    pDestino.CargoReconsideracion = string.Empty;
                    pDestino.CargoApelacion = string.Empty;
                    pDestino.UndOrgReconsideracionId = null;
                    pDestino.UndOrgApelacionId = null;
                    pDestino.PzoReconPresent = 0;
                    pDestino.PzoApelPresent = 0;
                    pDestino.PzoReconResol = 0;
                    pDestino.PzoApelResol = 0;
                    pDestino.PlazoAtencion = 0;
                    pDestino.Calificacion = 0;
                    pDestino.SustTecnicoTerminado = false;
                    pDestino.DatosGeneralesTerminado = false;
                    pDestino.Estado = 0;


                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.InfoAlCiudadano))
                    {

                        if (pDestino.ProcedimientoDatoAdicional == null) pDestino.ProcedimientoDatoAdicional = new List<ProcedimientoDatoAdicional>();
                        foreach (ProcedimientoDatoAdicional da in pOrigen.ProcedimientoDatoAdicional)
                        {
                            if (pDestino.ProcedimientoDatoAdicional.Count(x => x.MetaDatoId == da.MetaDatoId) == 0)
                            {
                                pDestino.ProcedimientoDatoAdicional.Add(new ProcedimientoDatoAdicional()
                                {
                                    MetaDatoId = da.MetaDatoId,
                                    Comentario = da.Comentario,
                                    ProcedimientoId = pDestino.ProcedimientoId
                                });
                            }
                        }
                        if (pDestino.ProcedimientoSede == null) pDestino.ProcedimientoSede = new List<ProcedimientoSede>();
                        foreach (ProcedimientoSede sede in pOrigen.ProcedimientoSede)
                        {
                            if (pDestino.ProcedimientoSede.Count(x => x.SedeId == sede.SedeId) == 0)
                            {
                                pDestino.ProcedimientoSede.Add(new ProcedimientoSede()
                                {
                                    SedeId = sede.SedeId,
                                    ProcedimientoId = pDestino.ProcedimientoId
                                });
                            }
                        }
                        if (pDestino.ProcedimientoSede.Count() > 0)
                        {
                            foreach (ProcedimientoSede s in pDestino.ProcedimientoSede)
                            {
                                var obj = pOrigen.ProcedimientoSede.Where(x => x.SedeId == s.SedeId).FirstOrDefault();

                                if (obj != null)
                                {
                                    if (obj.UndOrgRecepcionDocumentos != null)
                                        s.UndOrgRecepcionDocumentos = obj.UndOrgRecepcionDocumentos;
                                }
                                else
                                {
                                    s.UndOrgRecepcionDocumentos = new List<UndOrgRecepcionDocumentos>();
                                }
                            }
                        }

                        pDestino.ProcedimientoSede.ForEach(r => r.ProcedimientoId = pDestino.ProcedimientoId);
                        pDestino.ProcedimientoSede.ForEach(r => r.UndOrgRecepcionDocumentos.ForEach(t => t.ProcedimientoId = pDestino.ProcedimientoId));


                        if (pDestino.NotaCiudadano == null) pDestino.NotaCiudadano = new List<NotaCiudadano>();
                        foreach (NotaCiudadano nc in pOrigen.NotaCiudadano)
                        {
                            pDestino.NotaCiudadano.Add(new NotaCiudadano()
                            {
                                NotaCiudadanoId = pDestino.NotaCiudadano.Count() + 1,
                                Nota = nc.Nota,
                                TipoNotaId = nc.TipoNotaId,
                                ProcedimientoId = pDestino.ProcedimientoId
                            });
                        }
                        if (pDestino.PasoSeguir == null) pDestino.PasoSeguir = new List<PasoSeguir>();
                        foreach (PasoSeguir ps in pOrigen.PasoSeguir)
                        {
                            pDestino.PasoSeguir.Add(new PasoSeguir()
                            {
                                PasoSeguirId = pDestino.PasoSeguir.Count() + 1,
                                Descripcion = ps.Descripcion,
                                ProcedimientoId = pDestino.ProcedimientoId
                            });
                        }

                        pDestino.ProcedimientoDatoAdicional.ForEach(x => ctx.Entry(x).State = EntityState.Added);
                        pDestino.ProcedimientoSede.ForEach(x => ctx.Entry(x).State = EntityState.Added);
                        pDestino.ProcedimientoSede.ForEach(x => x.UndOrgRecepcionDocumentos.ForEach(y => ctx.Entry(y).State = EntityState.Added));
                        pDestino.NotaCiudadano.ForEach(x => ctx.Entry(x).State = EntityState.Added);
                        pDestino.PasoSeguir.ForEach(x => ctx.Entry(x).State = EntityState.Added);


                        pDestino.UndOrgPresentDocumentacionId = pOrigen.UndOrgPresentDocumentacionId;
                        pDestino.Telefono = pOrigen.Telefono;
                        pDestino.Anexo = pOrigen.Anexo;
                        pDestino.Correo = pOrigen.Correo;
                        pDestino.EsGratuito = pOrigen.EsGratuito;
                    }
                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.InfoBasica))
                    {
                        pDestino.Calificacion = pOrigen.Calificacion;
                        pDestino.PlazoAtencion = pOrigen.PlazoAtencion;
                        pDestino.SustTecCalificacion = pOrigen.SustTecCalificacion;
                        pDestino.Objetivo = pOrigen.Objetivo;

                        pDestino.CargoReconsideracion = pOrigen.CargoReconsideracion;
                        pDestino.CargoApelacion = pOrigen.CargoApelacion;
                        pDestino.UndOrgReconsideracionId = pOrigen.UndOrgReconsideracionId;
                        pDestino.UndOrgApelacionId = pOrigen.UndOrgApelacionId;
                        pDestino.PzoReconPresent = pOrigen.PzoReconPresent;
                        pDestino.PzoApelPresent = pOrigen.PzoApelPresent;
                        pDestino.PzoReconResol = pOrigen.PzoReconResol;
                        pDestino.PzoApelResol = pOrigen.PzoApelResol;
                    }

                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.TipoAtencion))
                    {

                        for (int i = 0; i < pOrigen.TablaAsme.Count(); i++)
                        {

                            TablaAsme tabla = pOrigen.TablaAsme[i];

                            if (tabla.Codigo == null)
                            {
                                tabla.Codigo = "-";
                            }


                            if (tabla.Codigo.Equals("-"))
                            {
                                List<Actividad> lstAct = new List<Actividad>();
                                foreach (Actividad t in tabla.Actividad)
                                {
                                    Actividad act = new Actividad();

                                    act.Descripcion = t.Descripcion;
                                    act.Duracion = t.Duracion;
                                    act.Orden = t.Orden;
                                    act.TipoActividad = t.TipoActividad;
                                    act.TipoValor = t.TipoValor;
                                    act.UnidadOrganicaId = t.UnidadOrganicaId;
                                    if (t.ActividadRecurso != null)
                                    {
                                        act.ActividadRecurso = t.ActividadRecurso.Select(x => new ActividadRecurso()
                                        {
                                            Cantidad = x.Cantidad,
                                            RecursoId = x.RecursoId
                                        }).ToList();
                                    }

                                    lstAct.Add(act);
                                    //if (t.ActividadRecurso != null)
                                    //{
                                    //    List<ActividadRecurso> list = t.ActividadRecurso;
                                    //    foreach (ActividadRecurso ar in list)
                                    //    {
                                    //        act.ActividadRecurso.Add(new ActividadRecurso()
                                    //        {
                                    //            Cantidad = ar.Cantidad,
                                    //            RecursoId = ar.RecursoId
                                    //        });
                                    //    }
                                    //}
                                }

                                pDestino.TablaAsme.Add(new TablaAsme()
                                {
                                    ProcedimientoId = pDestino.ProcedimientoId,
                                    Codigo = tabla.Codigo,
                                    Descripcion = tabla.Descripcion,
                                    Prestaciones = tabla.Prestaciones,
                                    Personal = tabla.Personal,
                                    MaterialFungible = tabla.MaterialFungible,
                                    ServicioIdentificable = tabla.ServicioIdentificable,
                                    MaterialNoFungible = tabla.MaterialNoFungible,
                                    ServicioTerceros = tabla.ServicioTerceros,
                                    Depreciacion = tabla.Depreciacion,
                                    Fijos = tabla.Fijos,
                                    CostoUnitario = tabla.CostoUnitario,
                                    Subvencion = tabla.Subvencion,
                                    DerechoTramitacion = tabla.DerechoTramitacion,
                                    EsGratuito = tabla.EsGratuito,
                                    Terminado = tabla.Terminado,
                                    //ComentarioSeccionId = tabla.ComentarioSeccionId,
                                    //Comentarios = tabla.Comentarios,
                                    UserCreacion = null,
                                    FecCreacion = null,
                                    Actividad = lstAct,
                                    AsmeActual = tabla.AsmeActual,
                                    PctSubvencion = tabla.PctSubvencion,
                                    UITID = tabla.UITID

                                });

                            }
                            else
                            {
                                pDestino.TablaAsme[0].Descripcion = tabla.Descripcion;
                                pDestino.TablaAsme[0].Prestaciones = tabla.Prestaciones;
                                pDestino.TablaAsme[0].Personal = tabla.Personal;
                                pDestino.TablaAsme[0].MaterialFungible = tabla.MaterialFungible;
                                pDestino.TablaAsme[0].ServicioIdentificable = tabla.ServicioIdentificable;
                                pDestino.TablaAsme[0].MaterialNoFungible = tabla.MaterialNoFungible;
                                pDestino.TablaAsme[0].ServicioTerceros = tabla.ServicioTerceros;
                                pDestino.TablaAsme[0].Depreciacion = tabla.Depreciacion;
                                pDestino.TablaAsme[0].Fijos = tabla.Fijos;
                                pDestino.TablaAsme[0].CostoUnitario = tabla.CostoUnitario;
                                pDestino.TablaAsme[0].Subvencion = tabla.Subvencion;
                                pDestino.TablaAsme[0].DerechoTramitacion = tabla.DerechoTramitacion;
                                pDestino.TablaAsme[0].EsGratuito = tabla.EsGratuito;
                                pDestino.TablaAsme[0].Terminado = tabla.Terminado;
                                //pDestino.TablaAsme[0].ComentarioSeccionId = tabla.ComentarioSeccionId;
                                //pDestino.TablaAsme[0].Comentarios = tabla.Comentarios;
                                pDestino.TablaAsme[0].UserCreacion = null;
                                pDestino.TablaAsme[0].FecCreacion = null;
                                pDestino.TablaAsme[0].AsmeActual = tabla.AsmeActual;
                                pDestino.TablaAsme[0].PctSubvencion = tabla.PctSubvencion;
                                pDestino.TablaAsme[0].UITID = tabla.UITID;

                                pDestino.TablaAsme[0].Actividad = new List<Actividad>();

                                if (tabla.Actividad != null)
                                {
                                    foreach (Actividad a in tabla.Actividad)
                                    {
                                        pDestino.TablaAsme[0].Actividad.Add(new Actividad()
                                        {
                                            Descripcion = a.Descripcion,
                                            Duracion = a.Duracion,
                                            Orden = a.Orden,
                                            TipoActividad = a.TipoActividad,
                                            TipoValor = a.TipoValor,
                                            UnidadOrganicaId = a.UnidadOrganicaId,
                                            ActividadRecurso = a.ActividadRecurso == null ? new List<ActividadRecurso>() : a.ActividadRecurso.Select(x => new ActividadRecurso()
                                            {
                                                Cantidad = x.Cantidad,
                                                RecursoId = x.RecursoId
                                            }).ToList()
                                        });
                                    }
                                }


                                ctx.Entry(pDestino.TablaAsme[0]).State = EntityState.Modified;
                            }
                        }

                        pDestino.TablaAsme
                                       .ForEach(x =>
                                       {
                                           if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                                       });

                    }

                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.Requisito))
                    {
                        if (pDestino.Requisito == null) pDestino.Requisito = new List<Requisito>();

                        for (int i = 0; i < pOrigen.Requisito.Count(); i++)
                        {
                            Requisito req = new Requisito();

                            req.Descripcion = pOrigen.Requisito[i].Descripcion;
                            req.CadenaTramite = pOrigen.Requisito[i].CadenaTramite;
                            req.Nombre = pOrigen.Requisito[i].Nombre;
                            req.SustentoLegal = pOrigen.Requisito[i].SustentoLegal;
                            req.SustentoTecnico = pOrigen.Requisito[i].SustentoTecnico;
                            req.ProcedimientoId = ProcedimientoDestinoId;
                            req.TipoRequisito = pOrigen.Requisito[i].TipoRequisito;
                            req.RecNum = pOrigen.Requisito[i].RecNum;

                            if (pOrigen.Requisito[i].BaseLegal != null)
                            {

                                if (pOrigen.Requisito[i].BaseLegal.BaseLegalNorma != null)
                                {
                                    req.BaseLegal = new BaseLegal();
                                    req.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>();

                                    for (int x = 0; x < pOrigen.Requisito[i].BaseLegal.BaseLegalNorma.Count(); x++)
                                    {
                                        BaseLegalNorma bln = new BaseLegalNorma();

                                        bln.NormaId = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].NormaId;
                                        bln.ArchivoAdjuntoId = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].ArchivoAdjuntoId;
                                        bln.Articulo = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].Articulo;
                                        bln.Descripcion = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].Descripcion;
                                        bln.FechaPublicacion = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].FechaPublicacion;
                                        bln.Numero = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].Numero;
                                        bln.TipoBaseLegal = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].TipoBaseLegal;
                                        bln.TipoNormaId = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].TipoNormaId;
                                        bln.Url = pOrigen.Requisito[i].BaseLegal.BaseLegalNorma[x].Url;
                                        bln.EstadoACR = "0";

                                        req.BaseLegal.BaseLegalNorma.Add(bln);
                                    }
                                }
                            }

                            pDestino.Requisito.Add(req);

                            int idx = pDestino.Requisito.Count();
                            pDestino.Requisito[idx - 1].RequisitoFormulario = new List<RequisitoFormulario>();


                            if (pOrigen.Requisito[i].RequisitoFormulario != null)
                            {
                                if (pOrigen.Requisito[i].RequisitoFormulario.Count() > 0)
                                {
                                    for (int x = 0; x < pOrigen.Requisito[i].RequisitoFormulario.Count(); x++)
                                    {
                                        RequisitoFormulario rf = new RequisitoFormulario();
                                        rf.Nombre = pOrigen.Requisito[i].RequisitoFormulario[x].Nombre;
                                        rf.Url = pOrigen.Requisito[i].RequisitoFormulario[x].Url;
                                        rf.ArchivoAdjuntoId = pOrigen.Requisito[i].RequisitoFormulario[x].ArchivoAdjuntoId;
                                        rf.FormularioId = pOrigen.Requisito[i].RequisitoFormulario[x].FormularioId;
                                        pDestino.Requisito[idx - 1].RequisitoFormulario.Add(rf);
                                    }
                                }
                            }
                        }


                        pDestino.Requisito.ForEach(x =>
                        {
                            x.ProcedimientoId = pDestino.ProcedimientoId;
                            ctx.Entry(x).State = EntityState.Added;
                            x.BaseLegal.BaseLegalNorma.ForEach(y => ctx.Entry(y).State = EntityState.Added);
                            x.RequisitoFormulario.ForEach(y => ctx.Entry(y).State = EntityState.Added);
                        });
                    }
                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.BaseLegal))
                    {
                        if (pDestino.BaseLegal == null) pDestino.BaseLegal = new BaseLegal();
                        if (pDestino.BaseLegal.BaseLegalNorma == null) pDestino.BaseLegal.BaseLegalNorma = new List<BaseLegalNorma>();



                        if (pOrigen.BaseLegal != null)
                        {
                            if (pOrigen.BaseLegal.BaseLegalNorma != null)
                            {

                                //ctx.Entry(pDestino.BaseLegal).State = EntityState.Added;

                                for (int x = 0; x < pOrigen.BaseLegal.BaseLegalNorma.Count(); x++)
                                {
                                    BaseLegalNorma bln = new BaseLegalNorma();

                                    bln.NormaId = pOrigen.BaseLegal.BaseLegalNorma[x].NormaId;
                                    bln.ArchivoAdjuntoId = pOrigen.BaseLegal.BaseLegalNorma[x].ArchivoAdjuntoId;
                                    bln.Articulo = pOrigen.BaseLegal.BaseLegalNorma[x].Articulo;
                                    bln.Descripcion = pOrigen.BaseLegal.BaseLegalNorma[x].Descripcion;
                                    bln.FechaPublicacion = pOrigen.BaseLegal.BaseLegalNorma[x].FechaPublicacion;
                                    bln.Numero = pOrigen.BaseLegal.BaseLegalNorma[x].Numero;
                                    bln.TipoBaseLegal = pOrigen.BaseLegal.BaseLegalNorma[x].TipoBaseLegal;
                                    bln.TipoNormaId = pOrigen.BaseLegal.BaseLegalNorma[x].TipoNormaId;
                                    bln.Url = pOrigen.BaseLegal.BaseLegalNorma[x].Url;
                                    bln.BaseLegalId = pDestino.BaseLegal.BaseLegalId;
                                    bln.EstadoACR = pOrigen.BaseLegal.BaseLegalNorma[x].EstadoACR;
                                    bln.BaseLegalNormaId = x + 1;

                                    pDestino.BaseLegal.BaseLegalNorma.Add(bln);

                                    //ctx.Entry(pDestino.BaseLegal.BaseLegalNorma).State = EntityState.Added;
                                }
                            }
                        }

                        pDestino.BaseLegal.BaseLegalNorma.ForEach(x =>
                        {
                            ctx.Entry(x).State = EntityState.Added;
                            x.BaseLegal.BaseLegalNorma.ForEach(y => ctx.Entry(y).State = EntityState.Added);
                        });
                    }
                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.DatosGeneralesYSTL))
                    {
                        if (!lstCopia.Contains(DatoCopia.Todo) && !lstCopia.Contains(DatoCopia.InfoBasica))
                        {
                            pDestino.Calificacion = pOrigen.Calificacion;
                            pDestino.PlazoAtencion = pOrigen.PlazoAtencion;

                            pDestino.CargoReconsideracion = pOrigen.CargoReconsideracion;
                            pDestino.CargoApelacion = pOrigen.CargoApelacion;
                            pDestino.UndOrgReconsideracionId = pOrigen.UndOrgReconsideracionId;
                            pDestino.UndOrgApelacionId = pOrigen.UndOrgApelacionId;
                            pDestino.PzoReconPresent = pOrigen.PzoReconPresent;
                            pDestino.PzoApelPresent = pOrigen.PzoApelPresent;
                            pDestino.PzoReconResol = pOrigen.PzoReconResol;
                            pDestino.PzoApelResol = pOrigen.PzoApelResol;
                        }
                        if (!lstCopia.Contains(DatoCopia.Todo) && !lstCopia.Contains(DatoCopia.InfoAlCiudadano))
                        {
                            pDestino.UndOrgPresentDocumentacionId = pOrigen.UndOrgPresentDocumentacionId;
                            pDestino.Telefono = pOrigen.Telefono;
                            pDestino.Anexo = pOrigen.Anexo;
                            pDestino.Correo = pOrigen.Correo;

                            if (pDestino.ProcedimientoDatoAdicional == null) pDestino.ProcedimientoDatoAdicional = new List<ProcedimientoDatoAdicional>();
                            foreach (ProcedimientoDatoAdicional da in pOrigen.ProcedimientoDatoAdicional)
                            {
                                if (pDestino.ProcedimientoDatoAdicional.Count(x => x.MetaDatoId == da.MetaDatoId) == 0)
                                {
                                    pDestino.ProcedimientoDatoAdicional.Add(new ProcedimientoDatoAdicional()
                                    {
                                        MetaDatoId = da.MetaDatoId,
                                        Comentario = da.Comentario
                                    });
                                }
                            }
                            if (pDestino.ProcedimientoSede == null) pDestino.ProcedimientoSede = new List<ProcedimientoSede>();
                            foreach (ProcedimientoSede sede in pOrigen.ProcedimientoSede)
                            {
                                if (pDestino.ProcedimientoSede.Count(x => x.SedeId == sede.SedeId) == 0)
                                {
                                    pDestino.ProcedimientoSede.Add(new ProcedimientoSede()
                                    {
                                        SedeId = sede.SedeId
                                    });
                                }
                            }
                            if (pDestino.NotaCiudadano == null) pDestino.NotaCiudadano = new List<NotaCiudadano>();
                            foreach (NotaCiudadano nc in pOrigen.NotaCiudadano)
                            {
                                pDestino.NotaCiudadano.Add(new NotaCiudadano()
                                {
                                    NotaCiudadanoId = pDestino.NotaCiudadano.Count() + 1,
                                    Nota = nc.Nota,
                                    TipoNotaId = nc.TipoNotaId
                                });
                            }
                            if (pDestino.PasoSeguir == null) pDestino.PasoSeguir = new List<PasoSeguir>();
                            foreach (PasoSeguir ps in pOrigen.PasoSeguir)
                            {
                                pDestino.PasoSeguir.Add(new PasoSeguir()
                                {
                                    PasoSeguirId = pDestino.PasoSeguir.Count() + 1,
                                    Descripcion = ps.Descripcion
                                });
                            }
                        }
                    }
                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.SustentoCalificacion))
                    {
                        pDestino.SustTecCalificacion = pOrigen.SustTecCalificacion;
                    }
                    if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.SustentoRequisito))
                    {
                        if (!lstCopia.Contains(DatoCopia.Todo) && !lstCopia.Contains(DatoCopia.Requisito))
                        {
                            if (pDestino.Requisito == null) pDestino.Requisito = new List<Requisito>();
                            foreach (Requisito req in pOrigen.Requisito)
                            {
                                pDestino.Requisito.Add(new Requisito()
                                {
                                    Descripcion = req.Descripcion,
                                    CadenaTramite = req.CadenaTramite,
                                    Nombre = req.Nombre,
                                    SustentoLegal = req.SustentoLegal,
                                    SustentoTecnico = req.SustentoTecnico,
                                    BaseLegal = new BaseLegal()
                                    {
                                        BaseLegalNorma = new List<BaseLegalNorma>()
                                    }
                                });
                                foreach (BaseLegalNorma bl in req.BaseLegal.BaseLegalNorma)
                                {
                                    req.BaseLegal.BaseLegalNorma.Add(new BaseLegalNorma()
                                    {
                                        NormaId = bl.NormaId,
                                        ArchivoAdjuntoId = bl.ArchivoAdjuntoId,
                                        Articulo = bl.Articulo,
                                        Descripcion = bl.Descripcion,
                                        FechaPublicacion = bl.FechaPublicacion,
                                        Numero = bl.Numero,
                                        TipoBaseLegal = bl.TipoBaseLegal,
                                        TipoNormaId = bl.TipoNormaId,
                                        EstadoACR = bl.EstadoACR,
                                        Url = bl.Url
                                    });
                                }
                            }
                            pDestino.Requisito.ForEach(x =>
                            {
                                x.ProcedimientoId = pDestino.ProcedimientoId;
                                ctx.Entry(x).State = EntityState.Added;
                                x.BaseLegal.BaseLegalNorma.ForEach(y => ctx.Entry(y).State = EntityState.Added);
                            });
                        }
                    }
                    //if (lstCopia.Contains(DatoCopia.Todo) || lstCopia.Contains(DatoCopia.TablaASME))
                    //{
                    //    if (pDestino.TablaAsme[0].Actividad == null) pDestino.TablaAsme[0].Actividad = new List<Actividad>();

                    //    if (pOrigen.TablaAsme[0].Actividad != null)
                    //        foreach (Actividad a in pOrigen.TablaAsme[0].Actividad)
                    //        {
                    //            pDestino.TablaAsme[0].Actividad.Add(new Actividad()
                    //            {
                    //                Descripcion = a.Descripcion,
                    //                Duracion = a.Duracion,
                    //                Orden = pOrigen.TablaAsme[0].Actividad.Count() + 1,
                    //                TipoActividad = a.TipoActividad,
                    //                TipoValor = a.TipoValor,
                    //                UnidadOrganicaId = a.UnidadOrganicaId,
                    //                ActividadRecurso = a.ActividadRecurso.Select(x => new ActividadRecurso()
                    //                {
                    //                    Cantidad = x.Cantidad,
                    //                    RecursoId = x.RecursoId
                    //                }).ToList()
                    //            });

                    //        }
                    //}
                    ctx.Entry(pDestino).State = EntityState.Modified;
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                    mensajes.Add(ex.Message);
                }
                return mensajes;
            }
        }



        public List<string> CopiarDatosProcedimientoTablaASME(long ProcedimientoOrigenId, long ProcedimientoDestinoId, long TablaAsmeIdSeleccionado, long TablaAsmeIdCopiar)
        {

            List<string> mensajes = new List<string>();
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                Procedimiento pOrigen = GetNoTracking(new List<long>() { ProcedimientoOrigenId }).First();
                Procedimiento pDestino = GetOneTablaAsme(ProcedimientoDestinoId);

                int valor = 0;
                for (int i = 0; i < pOrigen.TablaAsme.Count(); i++)
                {
                    TablaAsme tabla = pOrigen.TablaAsme[i];
                    if (tabla.TablaAsmeId == TablaAsmeIdCopiar)
                    {
                        pDestino.TablaAsme[0].TablaAsmeId = TablaAsmeIdSeleccionado;
                        pDestino.TablaAsme[0].Actividad = new List<Actividad>();

                        if (tabla.Actividad != null)
                        {
                            foreach (Actividad a in tabla.Actividad)
                            {
                                pDestino.TablaAsme[0].Actividad.Add(new Actividad()
                                {
                                    Descripcion = a.Descripcion,
                                    Duracion = a.Duracion,
                                    Orden = a.Orden,
                                    TipoActividad = a.TipoActividad,
                                    TipoValor = a.TipoValor,
                                    UnidadOrganicaId = a.UnidadOrganicaId,
                                    ActividadRecurso = a.ActividadRecurso == null ? new List<ActividadRecurso>() : a.ActividadRecurso.Select(x => new ActividadRecurso()
                                    {
                                        Cantidad = x.Cantidad,
                                        RecursoId = x.RecursoId
                                    }).ToList()
                                });
                            }
                        }
                        valor = i;
                    }
                    ctx.Entry(pDestino.TablaAsme[0]).State = EntityState.Modified;
                }

                pDestino.TablaAsme[valor].Actividad
                               .ForEach(x =>
                               {
                                   if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                               });



                ctx.Entry(pDestino).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                mensajes.Add(ex.Message);
            }
            return mensajes;
        }

        private void DisplayTrackedEntities()
        {
            SutContext ctx = Context.GetContext() as SutContext;
            foreach (var entry in ctx.ChangeTracker.Entries())
            {
                _log.Info(string.Format("Entity Name: {0}", entry.Entity.GetType().FullName));
                _log.Info(string.Format("Status: {0}", entry.State));
                foreach (var p in entry.CurrentValues.PropertyNames)
                {
                    _log.Info(string.Format("{0} = {1}", p, (entry.CurrentValues[p] ?? "").ToString()));
                }
                _log.Info("--");
            }
            _log.Info("/////////////////////////////////////");
        }

        public void SaveOnlyProcedimiento(Procedimiento obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.ProcedimientoId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> GetByExpedienteNoTracking(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var ver = ctx.Procedimiento
                         .Include(x => x.ProcedimientoSede)
                         .Include(x => x.NotaCiudadano)
                         .Include(x => x.ProcedimientoDatoAdicional)
                         .Include(x => x.ProcedimientoSede)
                         .Include(x => x.ProcedimientoCategoria)
                         .Include("ProcedimientoSede.UndOrgRecepcionDocumentos")
                         .Include(x => x.NotaCiudadano)
                         .Include(x => x.PasoSeguir)
                         .Include("TablaAsme.Actividad.ActividadRecurso")
                         .Include("TablaAsme.TablaAsmeReproduccion")
                         .Include("BaseLegal.BaseLegalNorma")
                         .Include("Requisito.BaseLegal.BaseLegalNorma")
                         .Include("Requisito.RequisitoFormulario")
                         .Where(x => x.ExpedienteId == ExpedienteId
                                 && x.Operacion != OperacionExpediente.Eliminacion && x.Estado != 3)
                         .AsNoTracking();

                return ctx.Procedimiento
                        .Include(x => x.ProcedimientoSede)
                        .Include(x => x.NotaCiudadano)
                        .Include(x => x.ProcedimientoDatoAdicional)
                        .Include(x => x.ProcedimientoSede)
                        .Include(x => x.ProcedimientoCategoria)
                        .Include("ProcedimientoSede.UndOrgRecepcionDocumentos")
                        .Include(x => x.NotaCiudadano)
                        .Include(x => x.PasoSeguir)
                        .Include("TablaAsme.Actividad.ActividadRecurso")
                        .Include("TablaAsme.TablaAsmeReproduccion")
                        .Include("BaseLegal.BaseLegalNorma")
                        .Include("Requisito.BaseLegal.BaseLegalNorma")
                        .Include("Requisito.RequisitoFormulario")
                        .Where(x => x.ExpedienteId == ExpedienteId
                                && x.Operacion != OperacionExpediente.Eliminacion && x.Estado != 3)
                        .AsNoTracking()
                        .ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Procedimiento> GetNoTracking(List<long> ids)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Procedimiento
                        .Include(x => x.ProcedimientoSede)
                        .Include(x => x.NotaCiudadano)
                        .Include(x => x.PasoSeguir)
                        .Include(x => x.ProcedimientoDatoAdicional)
                        .Include("TablaAsme.Actividad.ActividadRecurso")
                        .Include("BaseLegal.BaseLegalNorma")
                        .Include("Requisito.BaseLegal.BaseLegalNorma")
                        .Include("Requisito.RequisitoFormulario")
                        .Include("ProcedimientoSede.UndOrgRecepcionDocumentos")
                        .Include("TablaAsme.TablaAsmeReproduccion")
                        .Where(x => ids.Contains(x.ProcedimientoId))
                        .AsNoTracking()
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> GetByTipoTupa(TipoTupa tipo)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var t = (Int32)tipo;

                var asda = ctx.Procedimiento
                           .Include("Expediente.Entidad")
                           .Where(x => (Int32)x.Tupa.TipoTupa == t
                                   && x.Tupa.EstadoTupa == EstadoTupa.Vigente && x.Estado != 3 && x.Operacion != OperacionExpediente.Eliminacion)
                           .OrderBy(x => x.Expediente.Entidad.Nombre)
                           .ThenBy(x => x.Denominacion);

                return ctx.Procedimiento
                        .Include("Expediente.Entidad")
                        .Where(x => (Int32)x.Tupa.TipoTupa == t
                                && x.Tupa.EstadoTupa == EstadoTupa.Vigente && x.Estado != 3 && x.Operacion != OperacionExpediente.Eliminacion)
                        .OrderBy(x => x.Expediente.Entidad.Nombre)
                        .ThenBy(x => x.Denominacion)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> GetByTipoTupaEstantar(TipoTupa tipo)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var t = (Int32)tipo;



                return ctx.Procedimiento
                        .Include("Expediente.Entidad")
                        .Where(x => (Int32)x.Tupa.TipoTupa == t
                                && x.Tupa.EstadoTupa == EstadoTupa.Vigente && x.Estado != 3 && x.Operacion != OperacionExpediente.Eliminacion && x.TipoProcedimiento == TipoProcedimiento.Estandar)
                        .OrderBy(x => x.Expediente.Entidad.Nombre)
                        .ThenBy(x => x.Denominacion)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> GetByTipoTupaEstantarDelete(long expedienteId, long tipo)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;


                return ctx.Procedimiento
                        .Include("Expediente.Entidad")
                        .Where(x => x.Expediente.Entidad.EntidadId == tipo
                               && (x.Expediente.EstadoExpediente == EstadoExpediente.Anulado || x.Expediente.EstadoExpediente == EstadoExpediente.Publicado || x.Expediente.EstadoExpediente == EstadoExpediente.EnProceso && x.ExpedienteId != expedienteId)
                                )
                        .OrderBy(x => x.Expediente.Entidad.Nombre)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Procedimiento> GetByTipoTupaxidDelete(long expedienteId, long tipo, long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                string gg = ctx.Procedimiento
                        .Include("Expediente.Entidad")
                        .Where(x => x.Expediente.Entidad.EntidadId == tipo
                               && x.Expediente.ExpedienteId == ExpedienteId
                               && (x.Expediente.EstadoExpediente == EstadoExpediente.Anulado || x.Expediente.EstadoExpediente == EstadoExpediente.Publicado || x.Expediente.EstadoExpediente == EstadoExpediente.EnProceso && x.ExpedienteId != expedienteId)
                                )
                        .OrderBy(x => x.Expediente.Entidad.Nombre)
                        .ToString();


                return ctx.Procedimiento
                        .Include("Expediente.Entidad")
                        .Where(x => x.Expediente.Entidad.EntidadId == tipo
                               && x.Expediente.ExpedienteId == ExpedienteId
                               && (x.Expediente.EstadoExpediente == EstadoExpediente.Anulado || x.Expediente.EstadoExpediente == EstadoExpediente.Publicado || x.Expediente.EstadoExpediente == EstadoExpediente.EnProceso && x.ExpedienteId != expedienteId)
                                )
                        .OrderBy(x => x.Expediente.Entidad.Nombre)
                        .ToList();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> GetByTipoTupaxid(TipoTupa tipo, long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var t = (Int32)tipo;


                return ctx.Procedimiento
                        .Include("Expediente.Entidad")
                        .Where(x => (Int32)x.Tupa.TipoTupa == t && x.ExpedienteId == ExpedienteId
                                && x.Tupa.EstadoTupa == EstadoTupa.Vigente && x.Estado != 3 && x.Operacion != OperacionExpediente.Eliminacion)
                        .OrderBy(x => x.Expediente.Entidad.Nombre)
                        .ThenBy(x => x.Denominacion)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Procedimiento> GetProcByEntidad(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Procedimiento
                        .Where(x => x.Expediente.EntidadId == EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> ImportarProcedimiento(Procedimiento obj, long EntidadId)
        {
            List<string> mensajes = new List<string>();
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var expedientes = ctx.Expediente.Where(x => x.EntidadId == EntidadId);
                var exp = expedientes.SingleOrDefault(x => x.EstadoExpediente == EstadoExpediente.EnProceso || x.EstadoExpediente == EstadoExpediente.Observado);

                if (exp == null) mensajes.Add("La entidad no cuenta con un expediente en proceso");
                else
                {
                    obj.ExpedienteId = exp.ExpedienteId;

                    if (ctx.Procedimiento.Count(x => x.CodigoACR == obj.CodigoACR) > 0) mensajes.Add(string.Format("Ya existe el procedimiento con código {0}", obj.CodigoACR));
                    else
                    {
                        Save(obj);
                        ctx.SaveChanges();
                    }
                }
                return mensajes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<string> ImportarProcedimientoACREXANTE(Procedimiento obj, long EntidadId)
        {
            List<string> mensajes = new List<string>();
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var expedientes = ctx.Expediente.Where(x => x.EntidadId == EntidadId);
                var exp = expedientes.SingleOrDefault(x => x.TipoExpediente != TipoExpediente.CargaInicial && (x.EstadoExpediente == EstadoExpediente.EnProceso || x.EstadoExpediente == EstadoExpediente.Observado));

                if (exp == null) mensajes.Add("0");
                else
                {
                    obj.ExpedienteId = exp.ExpedienteId;

                    if (ctx.Procedimiento.Count(x => x.CodigoACR == obj.CodigoACR) > 0) mensajes.Add(string.Format("Ya existe el procedimiento con código {0}", obj.CodigoACR));
                    else
                    {
                        Save(obj);
                        ctx.SaveChanges();
                    }
                }
                return mensajes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<string> ImportarProcedimientoACR(Procedimiento obj, long EntidadId)
        {
            List<string> mensajes = new List<string>();
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var expedientes = ctx.Expediente.Where(x => x.EntidadId == EntidadId);
                var exp = expedientes.SingleOrDefault(x => x.TipoExpediente != TipoExpediente.CargaInicial && (x.EstadoExpediente == EstadoExpediente.EnProceso || x.EstadoExpediente == EstadoExpediente.Observado));

                if (exp == null) mensajes.Add("0");
                else
                {
                    obj.ExpedienteId = exp.ExpedienteId;

                    if (ctx.Procedimiento.Count(x => x.CodigoACR == obj.CodigoACR && x.TipoACR == obj.TipoACR && x.Operacion != OperacionExpediente.Eliminacion && x.Estado != 3) > 0) mensajes.Add(string.Format("Ya existe el procedimiento con código {0}", obj.CodigoACR));
                    else
                    {
                        Save(obj);
                        ctx.SaveChanges();
                    }
                }
                return mensajes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
