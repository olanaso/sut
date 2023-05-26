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
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories
{
    public class CalendarioActividadesRepository : BaseRepository<CalendarioActividades>, ICalendarioActividadesRepository
    {
        public CalendarioActividadesRepository(IContext context) : base(context) { }

        public CalendarioActividades GetOne(long CalendarioActividadesId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.CalendarioActividades
                        .SingleOrDefault(x => x.CalendarioActividadesId == CalendarioActividadesId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        //public List<CalendarioActividades> GetAll(long EntidadId)
        //{
        //    try
        //    {
        //        SutContext ctx = Context.GetContext() as SutContext;

        //        return ctx.CalendarioActividades
        //                .Where(x => x.CalendarioActividadesId == EntidadId)
        //                .ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<CalendarioActividades> GetAll(long pExpedienteId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                //parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);

                var _idAuditoria = conexion.Query<CalendarioActividades>(
                                        ProcedimientoAuditoria.ListaCalendario,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }






        public void Save(CalendarioActividades obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.CalendarioActividadesId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(long CalendarioActividadesId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vCalendarioActividadesId = ctx.CalendarioActividades.Where(x => x.CalendarioActividadesId == CalendarioActividadesId);

                foreach (CalendarioActividades o in vCalendarioActividadesId)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         
    }
}
