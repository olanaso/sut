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
    public class IncentivosRepository : BaseRepository<Incentivos>, IIncentivosRepository
    {
        private readonly ILogService<IncentivosRepository> _log;

        public IncentivosRepository(IContext context) : base(context) {
            _log = new LogService<IncentivosRepository>();
        }



        
        public List<Incentivos> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext; 

                return ctx.Incentivos  
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Incentivos> GetAllPeriodorepAdmin(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                if (EntidadId == 0) 
                {
                    return ctx.Incentivos
                    .Include(x => x.Entidad)
                    .ToList();
                }
                else 
                {
                    return ctx.Incentivos
                    .Include(x => x.Entidad)
                    .Where(x => x.EntidadId == EntidadId)
                    .ToList(); 
                }

              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Incentivos> GetAllPeriodo1RepAdmin(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                //if (EntidadId == 0)
                //{
                //    return ctx.Incentivos
                //    .Include(x => x.Entidad)
                //    .Where(x =>  x.Fec_Notificacion_Licencia_Funcionamiento != null && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                //    .ToList();
                //}
                //else
                //{
                //    return ctx.Incentivos
                //    .Include(x => x.Entidad)
                //    .Where(x => x.EntidadId == EntidadId && x.Fec_Notificacion_Licencia_Funcionamiento != null && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                //    .ToList();
                //}

                if (EntidadId == 0)
                {
                    return ctx.Incentivos
                    .Include(x => x.Entidad)
                    .Where(x =>   (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                    .ToList();
                }
                else
                {
                    return ctx.Incentivos
                    .Include(x => x.Entidad)
                    .Where(x => x.EntidadId == EntidadId   && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                    .ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Incentivos> GetAllPeriodo2RepAdmin(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                //if (EntidadId == 0)
                //{
                //    return ctx.Incentivos
                //    .Include(x => x.Entidad)
                //    .Where(x =>   x.Fec_Emision_Cert_ITSE != null && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                //    .ToList();
                //}
                //else
                //{
                //    return ctx.Incentivos
                //    .Include(x => x.Entidad)
                //    .Where(x => x.EntidadId == EntidadId && x.Fec_Emision_Cert_ITSE != null && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                //    .ToList();

                //}

                if (EntidadId == 0)
                {
                    return ctx.Incentivos
                    .Include(x => x.Entidad)
                    .Where(x =>   (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                    .ToList();
                }
                else
                {
                    return ctx.Incentivos
                    .Include(x => x.Entidad)
                    .Where(x => x.EntidadId == EntidadId   && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                    .ToList();

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         

        public List<Incentivos> GetAllPeriodo1(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;


                //if (EntidadId == 0)
                //{
                //    return ctx.Incentivos
                //   .Include(x => x.Entidad)
                //   .Where(x =>  x.Fec_Notificacion_Licencia_Funcionamiento != null && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                //      .ToList();
                //}
                //else
                //{
                //    return ctx.Incentivos
                //     .Include(x => x.Entidad)
                //     .Where(x => x.EntidadId == EntidadId && x.Fec_Notificacion_Licencia_Funcionamiento != null && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                //        .ToList();
                //}



                if (EntidadId == 0)
                {
                    return ctx.Incentivos
                   .Include(x => x.Entidad)
                   .Where(x =>   (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                      .ToList();
                }
                else
                {
                    return ctx.Incentivos
                     .Include(x => x.Entidad)
                     .Where(x => x.EntidadId == EntidadId  && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                        .ToList();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Incentivos> GetAllPeriodo2(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;


                if (EntidadId == 0)
                {
                    return ctx.Incentivos
                        .Include(x => x.Entidad)
                        .Where(x =>   (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                        .ToList();
                }
                else
                {
                    return ctx.Incentivos
                    .Include(x => x.Entidad)
                    .Where(x => x.EntidadId == EntidadId   && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                    .ToList();
                }

                //if (EntidadId == 0)
                //{
                //    return ctx.Incentivos
                //        .Include(x => x.Entidad)
                //        .Where(x =>   x.Fec_Emision_Cert_ITSE != null && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                //        .ToList();
                //}
                //else
                //{
                //    return ctx.Incentivos
                //    .Include(x => x.Entidad)
                //    .Where(x => x.EntidadId == EntidadId && x.Fec_Emision_Cert_ITSE != null && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                //    .ToList();
                //}


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Incentivos> GetAllPeriodoTodo(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                if (EntidadId == 0) 
                {
                    return ctx.Incentivos
                    .Include(x => x.Entidad) 
                       .ToList();
                }
                else 
                {
                    return ctx.Incentivos
                    .Include(x => x.Entidad)
                    .Where(x => x.EntidadId == EntidadId)
                       .ToList();
                }


               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    
        public List<Incentivos> GetByIncentivosBajoMedio(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Incentivos
                        .Include(x => x.Entidad)
                        .Where(x=> (x.NivelRiesgo==1 || x.NivelRiesgo == 2) && x.EntidadId== EntidadId   && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Incentivos> GetByIncentivosBajoMedio2(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                //return ctx.Incentivos
                //        .Include(x => x.Entidad)
                //        .Where(x => (x.NivelRiesgo == 1 || x.NivelRiesgo == 2) && x.EntidadId == EntidadId && x.Fec_Notificacion_Licencia_Funcionamiento != null && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                //        .ToList();

                return ctx.Incentivos
                     .Include(x => x.Entidad)
                     .Where(x => (x.NivelRiesgo == 1 || x.NivelRiesgo == 2) && x.EntidadId == EntidadId  && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                     .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Incentivos> GetByIncentivosAltoMuyAlto(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                //return ctx.Incentivos
                //      .Include(x => x.Entidad)
                //        .Where(x => (x.NivelRiesgo == 3 || x.NivelRiesgo == 4) && x.EntidadId == EntidadId && x.Fec_Emision_Cert_ITSE != null && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4  || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                //        .ToList();

                return ctx.Incentivos
                   .Include(x => x.Entidad)
                     .Where(x => (x.NivelRiesgo == 3 || x.NivelRiesgo == 4) && x.EntidadId == EntidadId   && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                     .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Incentivos> GetByIncentivosAltoMuyAlto2(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                //return ctx.Incentivos
                //      .Include(x => x.Entidad)
                //        .Where(x => (x.NivelRiesgo == 3 || x.NivelRiesgo == 4) && x.EntidadId == EntidadId && x.Fec_Emision_Cert_ITSE != null && ( x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                //        .ToList();



                return ctx.Incentivos
                     .Include(x => x.Entidad)
                       .Where(x => (x.NivelRiesgo == 3 || x.NivelRiesgo == 4) && x.EntidadId == EntidadId && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                       .ToList();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }



        public Incentivos GetByone(long IncentivosId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Incentivos 
                            .Include(x => x.ArchivoAdjunto)
                              .Include(x => x.Entidad)
                        .FirstOrDefault(x => x.IncentivosId == IncentivosId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public new void Save(Incentivos obj)
        {
            try
            { 
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.IncentivosId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
