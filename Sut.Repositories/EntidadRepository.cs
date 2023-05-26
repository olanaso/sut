using Sut.IRepositories;
using Sut.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sut.Entities;
using System.Data.Entity;
using Sut.Repositories.Configuracion;
using Dapper;
using Sut.Repositories.Procedimientos;
using System.Data;

namespace Sut.Repositories
{
    public class EntidadRepository : BaseRepository<Entidad>, IEntidadRepository
    {
        public EntidadRepository(IContext context) : base(context) { }

        public List<Entidad> GetByTipoTupa(TipoTupa tipo)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var t = (Int32)tipo;

                return ctx.Entidad
                        .Include(x => x.NivelGobierno)
                        .Include(x => x.Sector)
                        .Include(x => x.Provincia)
                        .Include("Provincia.Departamento")
                        .Where(x => (Int32)x.TipoTupa == t)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Entidad> lista_entidades(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idEntidad = conexion.Query<Entidad>(
                                        ProcedimientoAuditoria.ListarEntidades,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idEntidad.ToList();
            }
        }

        public List<Entidad> lista_entidadesActividad(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.CalendarioActividadesId, pEntidadId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idEntidad = conexion.Query<Entidad>(
                                        ProcedimientoAuditoria.ListarEntidadesActividad,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idEntidad.ToList();
            }
        }

 
        public List<Entidad> lista_entidadesAsignado(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idEntidad = conexion.Query<Entidad>(
                                        ProcedimientoAuditoria.ListarEntidadesAsignadas,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idEntidad.ToList();
            }
        }

        public List<Entidad> lista_entidadesAsignadoActividad(long pCalendarioActividadesId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.CalendarioActividadesId, pCalendarioActividadesId); 

                var _idEntidad = conexion.Query<Entidad>(
                                        ProcedimientoAuditoria.ListarEntidadesAsignadasActividad,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idEntidad.ToList();
            }
        }

        


        public new Entidad GetOne(System.Linq.Expressions.Expression<Func<Entidad, bool>> predicate)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Entidad
                        .Include(x => x.Provincia)
                        .SingleOrDefault(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Entidad GetOne(string Codigo)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Entidad
                        .SingleOrDefault(x => x.Codigo == Codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Entidad GetOneProvincia(long ProvinciaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Entidad
                        .Include(x => x.Provincia)
                        .SingleOrDefault(x => x.ProvinciaId == ProvinciaId && x.EstadoProvincia == NivelPadre.Padre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Entidad GetOneMin(long SectorID)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Entidad
                        .Include(x => x.Provincia)
                        .SingleOrDefault(x => x.SectorId == SectorID && x.EstadoMinistrio==1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Entidad GetOne(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Entidad
                        .Include(x => x.Provincia)
                        .SingleOrDefault(x => x.EntidadId == EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Guardar(Entidad obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.EntidadId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int MaxCorrelativo()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Entidad
                        .Where(x => x.TipoTupa == TipoTupa.Normal)
                        .Max(x => x.Correlativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
