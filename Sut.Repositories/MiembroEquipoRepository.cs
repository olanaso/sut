using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using Sut.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories
{
    public class MiembroEquipoRepository : BaseRepository<MiembroEquipo>, IMiembroEquipoRepository
    {
        private readonly ILogService<MiembroEquipoRepository> _log;

        public MiembroEquipoRepository(IContext context) : base(context)
        {
            _log = new LogService<MiembroEquipoRepository>();
        }

        public MiembroEquipo GetOne(string NroDocumento)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.MiembroEquipo
                        .Include(x => x.Entidad)
                           .Include(x => x.ArchivoAdjunto)
                        .AsNoTracking()
                        .FirstOrDefault(x => x.NroDocumento == NroDocumento && x.Estado== EstadoMiembroEquipo.Activo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MiembroEquipo GetOne(long MiembroEquipoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.MiembroEquipo
                        .Include(x => x.Entidad)
                           .Include(x => x.ArchivoAdjunto)
                        .FirstOrDefault(x => x.MiembroEquipoId == MiembroEquipoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new List<MiembroEquipo> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.MiembroEquipo
                        .Include(x => x.Entidad)
                        .Include(x => x.RolEquipo)
                           .Include(x => x.ArchivoAdjunto)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new void Save(MiembroEquipo obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.MiembroEquipoId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MiembroEquipo> GetByEntidad(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.MiembroEquipo
                        .Include(x => x.Entidad)
                        .Where(x => x.EntidadId == EntidadId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
