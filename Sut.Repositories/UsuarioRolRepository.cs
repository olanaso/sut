using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using Sut.Log;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories
{
    public class UsuarioRolRepository : BaseRepository<UsuarioRol>, IUsuarioRolRepository
    {
        private readonly ILogService<UsuarioRolRepository> _log;

        public UsuarioRolRepository(IContext context) : base(context) {
            _log = new LogService<UsuarioRolRepository>();
        }



        
        public List<UsuarioRol> GetByUsuarioRol(long UsuarioId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.UsuarioRol
                        .Include(x => x.Usuario)
                        .Where(x => x.UsuarioId == UsuarioId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<UsuarioRol> GetByEntidad(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.UsuarioRol
                         .Include(x => x.Usuario) 
                        //.Include(x => x.MiembroEquipo)
                        .Include("Usuario.MiembroEquipo")
                        .Include("Usuario.MiembroEquipo.RolEquipo")
                        //.Include("Usuario.MiembroEquipo")
                        .Where(x => x.EntidadId == EntidadId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UsuarioRol GetByone(long UsuarioId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.UsuarioRol 
                        .FirstOrDefault(x => x.UsuarioId == UsuarioId &&  x.valor==0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public new void Save(UsuarioRol obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
