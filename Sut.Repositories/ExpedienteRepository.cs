using Dapper;
using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using Sut.Repositories.Configuracion;
using Sut.Repositories.Procedimientos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Sut.Repositories
{
    public class ExpedienteRepository : BaseRepository<Expediente>, IExpedienteRepository
    {
        public ExpedienteRepository(IContext context) : base(context) { }
        

       public Expediente GetOneEntidad(long ExpedienteId, long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Expediente
                        .Include(x => x.Entidad)
                        .SingleOrDefault(x => x.ExpedienteId == ExpedienteId && x.EntidadId== EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Expediente GetOne(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Expediente
                        .Include(x => x.Entidad)
                        .SingleOrDefault(x => x.ExpedienteId == ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Expediente> GetByEntidad(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Expediente 
                        .Where(x => x.EntidadId == EntidadId && x.Estado==1)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Expediente> GetByEntidadCompleto()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Expediente
                        .Where(x => x.Estado == 1)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Procedimiento> GetByEstados(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Procedimiento
                        .Where(x => x.ExpedienteId == ExpedienteId && x.Estado != 3 && x.Operacion != OperacionExpediente.Eliminacion && x.Operacion != OperacionExpediente.Ninguna)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public List<Expediente> GetByEntidadMax(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Expediente
                        .Where(x => x.EntidadId == EntidadId)
                        .OrderByDescending(x=>x.ExpedienteId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Expediente GetOneActivo(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Expediente
                        .Include(x => x.Entidad)
                        .SingleOrDefault(x => x.EntidadId == EntidadId && x.EstadoExpediente==EstadoExpediente.EnProceso);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<Expediente> GetByEntidadAnulado(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Expediente
                        .Where(x => x.EntidadId == EntidadId && x.EstadoExpediente != EstadoExpediente.Anulado)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Expediente> GetByEntidadRatificador(long? ProvinciaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                

                return ctx.Expediente 
                        .Include(x => x.Entidad)
                        //.Where(x =>  x.ProvinciaId == ProvinciaId && x.FecPresentacion != null)
                        .Where(x => x.ProvinciaId == ProvinciaId && x.EstadoExpediente == EstadoExpediente.EnProceso && x.Estado == 1 && x.Entidad.SectorId==80)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Expediente> GetAllLikePaginTodo()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;



                return ctx.Expediente 
                        .Include(x => x.Entidad)
                        .Include("Provincia")
                        .Include("Provincia.Departamento")
                        .OrderByDescending(x => x.EntidadId)
                        .OrderByDescending(x => x.ExpedienteId)
                        .OrderByDescending(x => x.FecCreacion) 
                        .Where(x=>x.Estado == 1)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Expediente> GetAllLikePaginTodo(long LDepartamentoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Expediente
                         .Include(x => x.Entidad)
                         .Include("Provincia")
                         .Include("Provincia.Departamento")
                         .OrderByDescending(x => x.EntidadId)
                         .OrderByDescending(x => x.ExpedienteId)
                         .OrderByDescending(x => x.FecCreacion)
                         .Where(x => x.Estado == 1 && x.Entidad.Provincia.Departamento.DepartamentoId == LDepartamentoId)
                         .ToList();
                //.Include(Entidad => Entidad.Provincia)
                //.Include(Entidad => Entidad.Provincia.Departamento)
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Expediente> GetAllLikePaginAsigando()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;



                return ctx.Expediente
                        .Include(x => x.Entidad) 
                        .OrderByDescending(x => x.EntidadId)
                        .OrderByDescending(x => x.ExpedienteId)
                        .OrderByDescending(x => x.FecCreacion)
                        .Where(x => x.Estado == 1)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        

        public List<Expediente> GetAllLikePaginSOLICITUDWORD()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext; 

                return ctx.Expediente
                        .Include(x => x.Entidad) 
                        .Where(x => x.Estado == 1 && x.EstadoReporteWord==1)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public List<Procedimiento> GetAllLikePaginTodoConfigurar()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                

                return ctx.Procedimiento
                        .Include(x => x.Expediente)
                        .Include("Expediente.Entidad")
                         .Where(x => x.CodigoACR != "0" && x.CodigoCorto !=null && x.Estado != 3)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> GetAllLikePaginTodoConfigurarProce()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;


                return ctx.Procedimiento
                        .Include(x => x.Expediente)
                        .Include("Expediente.Entidad")
                         .Where(x => x.CodigoCorto != null && x.Estado!=3 && x.Operacion!= OperacionExpediente.Eliminacion)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Requisito> GetAllLikePaginTodorequisitos(long ProcedimientoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;


                return ctx.Requisito
                         .Where(x => x.ProcedimientoId == ProcedimientoId )
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Expediente> GetByEntidadFiscalizador(EstadoExpediente estadoExpediente)
        {
            //try
            //{
            //    SutContext ctx = Context.GetContext() as SutContext;


            //    var der = ctx.Expediente
            //           .Include(x => x.Entidad)
            //           .Where(x => x.EstadoExpediente == estadoExpediente && x.Estado == 1);

            //    return ctx.Expediente
            //            .Include(x => x.Entidad) 
            //            .Where(x => x.EstadoExpediente == estadoExpediente && x.Estado==1 )
            //            .ToList();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.estadoExpediente, estadoExpediente); 

                var _idExpediente = conexion.Query<Expediente>(
                                        ProcedimientoAuditoria.Listaexpfiscalizador,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idExpediente.ToList();
            }
        }

        public List<Expediente> GetByEntidadEvaluador(long SectorId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext; 

                    return ctx.Expediente
                        .Include(x => x.Entidad) 
                        //.Where(x => x.SectorId == SectorId && x.Entidad.CodPadre!="2")
                        //.Where(x => x.SectorId == SectorId && x.EstadoExpediente != EstadoExpediente.EnProceso && x.EstadoExpediente != EstadoExpediente.Anulado && x.EstadoExpediente != EstadoExpediente.Publicado)
                        .Where(x => x.SectorId == SectorId && x.EstadoExpediente == EstadoExpediente.EnProceso && x.Estado == 1)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Expediente> GetAllByRptActividad(long pExpedienteId, int pTipoRep)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.TipoRep, pTipoRep);

                var _idExpediente = conexion.Query<Expediente>(
                                        ProcedimientoAuditoria.ActividadesListar,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idExpediente.ToList();
            }
        }



        public List<Expediente> GetAllLikePaginEvaluadorMEFPCM(EstadoExpediente estadoExpediente)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Expediente
                        .Include(x => x.Entidad) 
                        .Where(x => x.EstadoEvaluadorMinisterio == estadoExpediente && x.Estado == 1 && (x.Entidad.CodPadre == "1"|| x.Entidad.CodPadre == "2") )
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Expediente> GetAllBy(System.Linq.Expressions.Expression<Func<Expediente, bool>> predicate)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Expediente 
                        .Include(x => x.Entidad)
                        .Where(predicate)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Expediente obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.ExpedienteId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProcesaCostos(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                //ctx.AnexoPersonal.RemoveRange(ctx.AnexoPersonal.Where(x => x.ExpedienteId == ExpedienteId));
                //ctx.AnexoIdentificable.RemoveRange(ctx.AnexoIdentificable.Where(x => x.ExpedienteId == ExpedienteId));
                //ctx.AnexoNoIdentificable.RemoveRange(ctx.AnexoNoIdentificable.Where(x => x.ExpedienteId == ExpedienteId));

                // Calculo del Costo de Personal

                var costPersonal = ctx.RecursoCosto
                                    .Include(x => x.Recurso)
                                    .Where(x => x.ExpedienteId == ExpedienteId
                                            && x.Recurso.TipoRecurso == TipoRecurso.Personal  );
                var personal = ctx.ActividadRecurso
                                .Include("Actividad.UnidadOrganica")
                                .Include("Recurso")
                                .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                        && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                        && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Ninguna 
                                        && x.Actividad.TipoActividad != TipoActividad.Espera
                                        && x.Recurso.TipoRecurso == TipoRecurso.Personal && x.Actividad.TablaAsme.Procedimiento.Estado != 3
                                       //&& (
                                       //        (x.Actividad.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                                       //        && x.Actividad.TablaAsme.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar)
                                       //    ||
                                       //        (x.Actividad.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                                       //        && x.Actividad.TablaAsme.Procedimiento.TipoProcedimiento == TipoProcedimiento.Estandar)
                                       //    )
                                       );

                var anexo12 = personal.Join(costPersonal,
                                       p => p.Actividad.UnidadOrganicaId + "_" + p.RecursoId,
                                       c => c.UnidadOrganicaId + "_" + c.RecursoId,
                                       (p, c) => new { Personal = p, Actividad = p.Actividad, Costo = c, UnidadOrganica = p.Actividad.UnidadOrganica, Recurso = p.Recurso })
                                       ;
                var anexo1 = personal.Join(costPersonal,
                                        p => p.Actividad.UnidadOrganicaId + "_" + p.RecursoId,
                                        c => c.UnidadOrganicaId + "_" + c.RecursoId,
                                        (p, c) => new { Personal = p, Actividad = p.Actividad, Costo = c, UnidadOrganica = p.Actividad.UnidadOrganica, Recurso = p.Recurso })
                                        .ToList();

                List<AnexoPersonal> anexoPersonal = new List<AnexoPersonal>();
                decimal nume = 0;
                decimal nume2 = 0;
                for (int i = 0; i < anexo1.Count(); i++)
                {
                    anexoPersonal.Add(new AnexoPersonal()
                    {
                        AnexoPersonalId = i + 1,
                        ExpedienteId = ExpedienteId,
                        TablaAsmeId = anexo1[i].Actividad.TablaAsmeId,
                        UnidadOrganica = anexo1[i].UnidadOrganica.Nombre,
                        Nro = anexo1[i].Actividad.Orden.Value,
                        Actividad = anexo1[i].Actividad.Descripcion,
                        Cargo = anexo1[i].Recurso.Nombre,
                        EscalaIngreso = "",
                        Cantidad = anexo1[i].Personal.Cantidad,
                        Duracion = anexo1[i].Actividad.Duracion,
                        DuracionTotal = anexo1[i].Personal.Cantidad * anexo1[i].Actividad.Duracion,
                        CostoMinuto = anexo1[i].Costo.CostoAnual / (14400 * 12),
                        CostoTotal = (Math.Truncate((anexo1[i].Personal.Cantidad) * 100) / 100)*(Math.Truncate((anexo1[i].Actividad.Duracion) * 100) / 100) * Math.Truncate((anexo1[i].Costo.CostoAnual / (14400 * 12)) * 100) / 100
                        
                        //CostoTotal = anexo1[i].Personal.Cantidad * anexo1[i].Actividad.Duracion * anexo1[i].Costo.CostoAnual / (14400 * 12)
                    });

                    //nume2 = Math.Truncate(("ingresar texto") * 100) / 100;
                    //nume = Decimal.Truncate(anexo1[i].Personal.Cantidad * anexo1[i].Actividad.Duracion * anexo1[i].Costo.CostoAnual / (14400 * 12)) + nume;
                }

                ctx.AnexoPersonal.AddRange(anexoPersonal);

                // Calculo del Costo Identificable

                var costIdentificable = ctx.RecursoCosto
                                        .Include(x => x.Recurso)
                                        .Where(x => x.ExpedienteId == ExpedienteId
                                                && (x.Recurso.TipoRecurso == TipoRecurso.MaterialFungible
                                                    || x.Recurso.TipoRecurso == TipoRecurso.ServicioIdentificable));
                var recIdent = ctx.ActividadRecurso
                                .Include("Actividad.UnidadOrganica")
                                .Include("Recurso.UnidadMedida")
                                .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                        && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                        && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Ninguna
                                        && x.Actividad.TipoActividad != TipoActividad.Espera && x.Actividad.TablaAsme.Procedimiento.Estado != 3
                                        && (x.Recurso.TipoRecurso == TipoRecurso.MaterialFungible || x.Recurso.TipoRecurso == TipoRecurso.ServicioIdentificable)
                                        //&& (
                                        //        (x.Actividad.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                                        //        && x.Actividad.TablaAsme.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar)
                                        //    ||
                                        //        (x.Actividad.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                                        //        && x.Actividad.TablaAsme.Procedimiento.TipoProcedimiento == TipoProcedimiento.Estandar)
                                        //    )
                                        );
                var anexo23 = recIdent.Join(costIdentificable,
                                            r => r.RecursoId,
                                            c => c.RecursoId,
                                            (r, c) => new { ActividadRecurso = r, Actividad = r.Actividad, Costo = c, UnidadOrganica = r.Actividad.UnidadOrganica, Recurso = r.Recurso, UnidadMedida = r.Recurso.UnidadMedida })
                                            .ToList();

                List<AnexoIdentificable> anexoIdentificable = new List<AnexoIdentificable>();

                for (int i = 0; i < anexo23.Count(); i++)
                {
                    anexoIdentificable.Add(new AnexoIdentificable() {
                        AnexoIdentificableId = i + 1,
                        ExpedienteId = ExpedienteId,
                        TablaAsmeId = anexo23[i].Actividad.TablaAsmeId,
                        TipoRecurso = anexo23[i].Recurso.TipoRecurso,
                        TipoRecursoNom = anexo23[i].Recurso.TipoRecurso.ToString(),
                        UnidadOrganica = anexo23[i].UnidadOrganica.Nombre,
                        Nro = anexo23[i].Actividad.Orden.Value,
                        Actividad = anexo23[i].Actividad.Descripcion,
                        Codigo = anexo23[i].Recurso.Codigo,
                        Nombre = anexo23[i].Recurso.Nombre,
                        UnidadMedida = anexo23[i].UnidadMedida.Abreviatura,
                        Cantidad = anexo23[i].ActividadRecurso.Cantidad,
                        CostoUnitario = anexo23[i].Costo.CostoUnitario,
                        CostoTotal = anexo23[i].ActividadRecurso.Cantidad * anexo23[i].Costo.CostoUnitario
                    });
                }

                ctx.AnexoIdentificable.AddRange(anexoIdentificable);

                // Calculo del Costo No Identificable
                var costoAnual = ctx.RecursoCosto
                                    .Include("Inductor")
                                    .Where(x => x.ExpedienteId == ExpedienteId
                                            && (int)x.Recurso.TipoRecurso > 3)
                                    .ToList();
                var factor = ctx.FactorDedicacion
                            .Where(x => x.ExpedienteId == ExpedienteId)
                            .ToList();
                var valInductor = ctx.InductorValor
                                    .Where(x => x.ExpedienteId == ExpedienteId)
                                    .ToList();
                var inductor = costoAnual
                                .Select(x => x.Inductor)
                                .Distinct()
                                .ToList()
                                .Select(x => {
                                    x.ValorTotal = valInductor.Where(v => v.InductorId == x.InductorId)
                                                    .Sum(v => v.Valor);
                                    return x;
                                }).ToList();

                var recNoIdent = ctx.ActividadRecurso
                                .Include("Recurso.UnidadMedida")
                                .Include("Actividad.UnidadOrganica")
                                .Include("Actividad.TablaAsme")
                                .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                        && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                        && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Ninguna
                                        && x.Actividad.TipoActividad != TipoActividad.Espera && x.Actividad.TablaAsme.Procedimiento.Estado !=3
                                        &&  (int)x.Recurso.TipoRecurso > 3
                                        //&& (
                                        //        (x.Actividad.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                                        //        && x.Actividad.TablaAsme.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar)
                                        //    ||
                                        //        (x.Actividad.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                                        //        && x.Actividad.TablaAsme.Procedimiento.TipoProcedimiento == TipoProcedimiento.Estandar)
                                        //    )
                                        )
                                .ToList();
                var asignacion = recNoIdent.
                                GroupBy(x => new { Recurso = x.Recurso, UnidadOrganica = x.Actividad.UnidadOrganica },
                                (k, g) => new { Recurso = k.Recurso, UnidadOrganica = k.UnidadOrganica, DuracionTotal = g.Sum(a => a.Actividad.Duracion * a.Actividad.TablaAsme.Prestaciones)})
                                .ToList();
                var proceso1 = asignacion
                                .Join(costoAnual,
                                    p => p.Recurso.RecursoId,
                                    c => c.RecursoId,
                                    (p, c) => new { Recurso = p.Recurso, UnidadOrganica = p.UnidadOrganica, DuracionTotal = p.DuracionTotal, RecursoCosto = c })
                                .Join(inductor,
                                    p => p.RecursoCosto.InductorId,
                                    i => i.InductorId,
                                    (p, i) => new { Recurso = p.Recurso, UnidadOrganica = p.UnidadOrganica, DuracionTotal = p.DuracionTotal, RecursoCosto = p.RecursoCosto, Inductor = i })
                                .Join(valInductor,
                                    p => new { p.Inductor.InductorId, p.UnidadOrganica.UnidadOrganicaId },
                                    v => new { v.InductorId, v.UnidadOrganicaId },
                                    (p, v) => new { Recurso = p.Recurso, UnidadOrganica = p.UnidadOrganica, DuracionTotal = p.DuracionTotal, RecursoCosto = p.RecursoCosto, Inductor = p.Inductor, ValorInductor = v.Valor })
                                .Join(factor,
                                    p => new { p.UnidadOrganica.UnidadOrganicaId, p.Recurso.RecursoId },
                                    f => new { f.UnidadOrganicaId, f.RecursoId },
                                    (p, f) => new { Recurso = p.Recurso, UnidadOrganica = p.UnidadOrganica, DuracionTotal = p.DuracionTotal, RecursoCosto = p.RecursoCosto, Inductor = p.Inductor, ValorInductor = p.ValorInductor, Factor = ( f.ValorTupa / 100) })

                                    //(p, f) => new { Recurso = p.Recurso, UnidadOrganica = p.UnidadOrganica, DuracionTotal = p.DuracionTotal, RecursoCosto = p.RecursoCosto, Inductor = p.Inductor, ValorInductor = p.ValorInductor, Factor = (1 - f.ValorTupa / 100) })
                                    .Select(x => new {
                                        Recurso = x.Recurso,
                                        UnidadOrganica = x.UnidadOrganica,
                                        DuracionTotal = x.DuracionTotal,
                                        RecursoCosto = x.RecursoCosto,
                                        Costo = x.RecursoCosto.CostoAnual * (x.ValorInductor / x.Inductor.ValorTotal) * x.Factor
                                    });

                int AnexoNoIdenId = 1;
                var proceso2 = proceso1
                                .Join(recNoIdent,
                                p => new { p.Recurso.RecursoId, p.UnidadOrganica.UnidadOrganicaId },
                                a => new { a.RecursoId, a.Actividad.UnidadOrganicaId },
                                (p, a) => new { Proceso1 = p, Actividad = a.Actividad, ActividadRecurso = a })
                                .Select(x => new
                                {                                   
                                    Recurso = x.Proceso1.Recurso,
                                    UnidadOrganica = x.Proceso1.UnidadOrganica,
                                    Actividad = x.Actividad,
                                    UnidadMedida = x.ActividadRecurso.Recurso.UnidadMedida,
                                    RecursoCosto = x.Proceso1.RecursoCosto,
                                    Costo = x.Proceso1.Costo * x.Actividad.Duracion * x.Actividad.TablaAsme.Prestaciones / x.Proceso1.DuracionTotal,
                                 

                                }).GroupBy(x => new { x.Actividad.TablaAsme.TablaAsmeId, x.Recurso, x.UnidadMedida, x.RecursoCosto },
                                    (k, g) => new AnexoNoIdentificable()
                                    {
                                        ExpedienteId = ExpedienteId,
                                        TablaAsmeId = k.TablaAsmeId,
                                        TipoRecurso = k.Recurso.TipoRecurso,
                                        TipoRecursoNom = k.Recurso.TipoRecurso.ToString(),
                                        Codigo = k.Recurso.Codigo,
                                        Nombre = k.Recurso.Nombre,
                                        UnidadMedida = k.UnidadMedida.Abreviatura,
                                        PctAnual = g.Sum(x => x.Costo) == 0 ? 0 : k.RecursoCosto.CostoUnitario / g.Sum(x => x.Costo),
                                        CostoUnitario = k.RecursoCosto.CostoUnitario, 
                                        CostoTotal = (Math.Truncate((g.Sum(x => x.Costo)) * 100) / 100),
                                        AnexoNoIdentificableId = AnexoNoIdenId++,
                                    }
                                );

                /***verificar toda este procedimiento Inicio **/

                //if (proceso2.Count() < 1500)
                //{
                //for (int i = 0; i < proceso2.Count(); i++)
                //{
                //    proceso2[i].AnexoNoIdentificableId = i + 1;
                //    ctx.AnexoNoIdentificable.Add(proceso2[i]);
                //}
                //}
                ctx.AnexoNoIdentificable.AddRange(proceso2);

                // Resumen de Costos

                var tablasAsme = ctx.TablaAsme.Where(x => x.Procedimiento.ExpedienteId == ExpedienteId
                                                        && x.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                                        && x.Procedimiento.Operacion != OperacionExpediente.Ninguna && x.Procedimiento.Estado != 3
                                                    //&& (
                                                    //    (x.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                                                    //    && x.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar)
                                                    //||
                                                    //    (x.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                                                    //    && x.Procedimiento.TipoProcedimiento == TipoProcedimiento.Estandar)
                                                    //)
                                                    );



                //ctx.Entry(tablasAsmep).State = EntityState.Modified;

                foreach (var t in tablasAsme)
                {
                    t.Personal = anexoPersonal.Where(x => x.TablaAsmeId == t.TablaAsmeId).Sum(x => x.CostoTotal);
                    t.MaterialFungible = (Math.Truncate(anexoIdentificable.Where(x => x.TablaAsmeId == t.TablaAsmeId && x.TipoRecurso == TipoRecurso.MaterialFungible).Sum(x => x.CostoTotal) * 100) / 100) ;
                    t.ServicioIdentificable = (Math.Truncate(anexoIdentificable.Where(x => x.TablaAsmeId == t.TablaAsmeId && x.TipoRecurso == TipoRecurso.ServicioIdentificable).Sum(x => x.CostoTotal) * 100) / 100)  ;
                    t.MaterialNoFungible = (Math.Truncate(proceso2.Where(x => x.TablaAsmeId == t.TablaAsmeId && x.TipoRecurso == TipoRecurso.MaterialNoFungible).Sum(x => x.CostoTotal) / t.Prestaciones * 100) / 100) ;
                    t.ServicioTerceros = (Math.Truncate(proceso2.Where(x => x.TablaAsmeId == t.TablaAsmeId && x.TipoRecurso == TipoRecurso.ServicioTerceros).Sum(x => x.CostoTotal) / t.Prestaciones * 100) / 100);
                    t.Depreciacion = (Math.Truncate(proceso2.Where(x => x.TablaAsmeId == t.TablaAsmeId && x.TipoRecurso == TipoRecurso.Depreciacion).Sum(x => x.CostoTotal) / t.Prestaciones * 100) / 100) ;
                    t.Fijos = (Math.Truncate(proceso2.Where(x => x.TablaAsmeId == t.TablaAsmeId && x.TipoRecurso == TipoRecurso.Fijos).Sum(x => x.CostoTotal) / t.Prestaciones * 100) / 100)  ;
                  
                    t.CostoUnitario = t.Personal + t.MaterialFungible + t.ServicioIdentificable + t.MaterialNoFungible + t.ServicioTerceros + t.Depreciacion + t.Fijos ;

                    if (t.EsGratuito) t.DerechoTramitacion = 0;
                    if (!t.EsSubvencion) t.DerechoTramitacion = t.CostoUnitario; 
                    ctx.Entry(t).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EliminarProcesarCostos(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                ctx.AnexoPersonal.RemoveRange(ctx.AnexoPersonal.Where(x => x.ExpedienteId == ExpedienteId));
                ctx.AnexoIdentificable.RemoveRange(ctx.AnexoIdentificable.Where(x => x.ExpedienteId == ExpedienteId));
                ctx.AnexoNoIdentificable.RemoveRange(ctx.AnexoNoIdentificable.Where(x => x.ExpedienteId == ExpedienteId)); 
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void SaveOnlyExpediente(Expediente obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.ExpedienteId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
