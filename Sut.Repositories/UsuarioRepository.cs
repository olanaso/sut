using Dapper;
using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using Sut.Log;
using Sut.Repositories.Configuracion;
using Sut.Repositories.Procedimientos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly ILogService<UsuarioRepository> _log;

        public UsuarioRepository(IContext context) : base(context) {
            _log = new LogService<UsuarioRepository>();
        }

        public Usuario GetOne2(string NroDocumento,int  TipoDoc)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Usuario
                        .Include(x => x.Entidad)
                        .Include(x => x.MiembroEquipo)
                        .AsNoTracking()
                        .FirstOrDefault(x => x.NroDocumento == NroDocumento && x.Estado==EstadoUsuario.Activo && x.MiembroEquipo.TipoDoc== TipoDoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario GetOne(string NroDocumento)
        {
            try
            {
                    SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Usuario
                        .Include(x => x.Entidad)
                        .Include(x => x.MiembroEquipo)
                        .AsNoTracking()
                        .FirstOrDefault(x => x.NroDocumento == NroDocumento && x.Estado == EstadoUsuario.Activo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario GetOnerol(string NroDocumento, Rol rol)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Usuario
                        .Include(x => x.Entidad)
                        .Include(x => x.MiembroEquipo)
                        .AsNoTracking()
                        .FirstOrDefault(x => x.NroDocumento == NroDocumento && x.Estado == EstadoUsuario.Activo && x.Rol == rol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public Usuario GetOne(long UsuarioId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Usuario
                        .Include(x => x.Entidad)
                        .FirstOrDefault(x => x.UsuarioId == UsuarioId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario GetOnecompleto(long UsuarioId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Usuario
                         .Include(x => x.Entidad)
                        .Include(x => x.MiembroEquipo)
                        .Include("MiembroEquipo.RolEquipo")
                        .FirstOrDefault(x => x.UsuarioId == UsuarioId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new List<Usuario> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Usuario
                        .Include(x => x.Entidad)
                        .Include(x => x.MiembroEquipo)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> GetByEntidad(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Usuario
                        .Include(x => x.Entidad)
                        .Include(x => x.MiembroEquipo)
                        .Include("MiembroEquipo.RolEquipo")
                        .Where(x => x.EntidadId == EntidadId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UsuarioRol> GetByEntidadROL(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                //var ASDAS = ctx.UsuarioRol
                //   .Include(x => x.Usuario)
                //   .Where(x => x.EntidadId == EntidadId && (x.Rol == Rol.Evaluador || x.Rol == Rol.Ratificador));



                return ctx.UsuarioRol
                   .Include(x => x.Usuario)
                   .Include("Usuario.MiembroEquipo")
                   .Where(x => x.EntidadId == EntidadId && x.Usuario.Estado==EstadoUsuario.Activo && (x.Rol == Rol.Evaluador || x.Rol == Rol.Ratificador))
                    .ToList();

                //return ctx.Usuario
                //     .Include(x => x.Usuario) 
                //     .Where(x => x.EntidadId == EntidadId && (x.Rol == Rol.Evaluador || x.Rol == Rol.Ratificador))
                //     .ToList();

                //return ctx.Usuario
                //        .Include(x => x.Entidad)
                //        .Include(x => x.MiembroEquipo) 
                //        .Include("MiembroEquipo.RolEquipo") 
                //        .Include("Usuario.UsuarioRol")
                //        .Where(x => x.EntidadId == EntidadId)
                //        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public new void Save(Usuario obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.UsuarioId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new void SaveRoles(Usuario obj)
        {
            try
            {
                using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
                {
                    var parameters = new DynamicParameters();

                    parameters.Add("@iosp", obj.iosp);
                    parameters.Add("@RolId", obj.RolId);
                    parameters.Add("@UsuarioId", obj.UsuarioId);
                    parameters.Add("@EntidadId", obj.EntidadId);
               

                    var _idAuditoria = conexion.Query<string>(
                                            ProcedimientoAuditoria.Actualizar_RolUsuario,
                                            parameters,
                                            commandTimeout: 0,
                                            commandType: CommandType.StoredProcedure);
                    string resultado = "";

                  
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
