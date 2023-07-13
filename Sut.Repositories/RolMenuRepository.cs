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
    public class RolMenuRepository : BaseRepository<RolMenu>, IRolMenuRepository
    {
        private readonly ILogService<RolMenuRepository> _log;

        public RolMenuRepository(IContext context) : base(context) {
            _log = new LogService<RolMenuRepository>();
        }
         
        public List<RolMenu> GetByRolMenu(long rolId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.RolMenu 
                        .Where(x => x.RolId == rolId)
                        .OrderBy(x => x.Orden)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RolMenu> GetByRolMenuid(long rolId, long menuId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.RolMenu
                        .Where(x => x.RolId == rolId && x.MenuId == menuId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        public void Guardarrolmenu(List<RolMenu> lstRolMenu)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                //RolMenu obj = new RolMenu();
                //foreach (var asme in lstRolMenu)
                //{
                     
                //    obj.MenuId = asme.MenuId;
                //    obj.RolId = asme.RolId;
                //    obj.Ver = asme.Ver;
                //    obj.Nuevo = asme.Nuevo;
                //    obj.Editar = asme.Editar;
                //    obj.Guardar = asme.Guardar;
                //    obj.Eliminar = asme.Eliminar;
                //    obj.Descargar = asme.Descargar;
                //    obj.Agregar = asme.Agregar;
                //    obj.Copiar = asme.Copiar;
                //    obj.Terminar = asme.Terminar;
                //    obj.Importar = asme.Importar;
                //    obj.Procesar = asme.Procesar;
                //    obj.Anular = asme.Anular;
                //    obj.Activar = asme.Activar;
                //    obj.Generar = asme.Generar;
                //    obj.Reemplazar = asme.Reemplazar;
                //    obj.Presentar = asme.Presentar;
                //    obj.Sustentar = asme.Sustentar;
                //    obj.Observar = asme.Observar;
                //    obj.Publicar = asme.Publicar;

              
                //    ctx.Entry(obj).State = EntityState.Added;
                //}
                ctx.Entry(lstRolMenu).State = EntityState.Added;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new void Save(RolMenu obj)
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


        public void Eliminar(long rolId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var rol = ctx.RolMenu.Where(x => x.RolId == rolId);

                foreach (RolMenu o in rol)
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
