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
    public class IncentivosCorteRepository : BaseRepository<IncentivosCorte>, IIncentivosCorteRepository
    {
        private readonly ILogService<IncentivosCorteRepository> _log;

        public IncentivosCorteRepository(IContext context) : base(context) {
            _log = new LogService<IncentivosCorteRepository>();
        }



        
        public List<IncentivosCorte> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext; 

                return ctx.IncentivosCorte  
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<IncentivosCorte> GetAllPeriodorepAdmin(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                if (EntidadId == 0) 
                {
                    return ctx.IncentivosCorte
                    .Include(x => x.Entidad)
                    .ToList();
                }
                else 
                {
                    return ctx.IncentivosCorte
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
        public List<IncentivosCorte> GetAllPeriodo1RepAdmin(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                //if (EntidadId == 0)
                //{
                //    return ctx.IncentivosCorte
                //    .Include(x => x.Entidad)
                //    .Where(x =>  x.Fec_Notificacion_Licencia_Funcionamiento != null && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                //    .ToList();
                //}
                //else
                //{
                //    return ctx.IncentivosCorte
                //    .Include(x => x.Entidad)
                //    .Where(x => x.EntidadId == EntidadId && x.Fec_Notificacion_Licencia_Funcionamiento != null && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                //    .ToList();
                //}

                if (EntidadId == 0)
                {
                    return ctx.IncentivosCorte
                    .Include(x => x.Entidad)
                    .Where(x =>   (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                    .ToList();
                }
                else
                {
                    return ctx.IncentivosCorte
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

        public List<IncentivosCorte> GetAllPeriodo2RepAdmin(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                //if (EntidadId == 0)
                //{
                //    return ctx.IncentivosCorte
                //    .Include(x => x.Entidad)
                //    .Where(x =>   x.Fec_Emision_Cert_ITSE != null && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                //    .ToList();
                //}
                //else
                //{
                //    return ctx.IncentivosCorte
                //    .Include(x => x.Entidad)
                //    .Where(x => x.EntidadId == EntidadId && x.Fec_Emision_Cert_ITSE != null && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                //    .ToList();

                //}

                if (EntidadId == 0)
                {
                    return ctx.IncentivosCorte
                    .Include(x => x.Entidad)
                    .Where(x =>   (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                    .ToList();
                }
                else
                {
                    return ctx.IncentivosCorte
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
         

        public List<IncentivosCorte> GetAllPeriodo1(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;


                //if (EntidadId == 0)
                //{
                //    return ctx.IncentivosCorte
                //   .Include(x => x.Entidad)
                //   .Where(x =>  x.Fec_Notificacion_Licencia_Funcionamiento != null && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                //      .ToList();
                //}
                //else
                //{
                //    return ctx.IncentivosCorte
                //     .Include(x => x.Entidad)
                //     .Where(x => x.EntidadId == EntidadId && x.Fec_Notificacion_Licencia_Funcionamiento != null && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                //        .ToList();
                //}

                if (EntidadId == 0)
                {
                    return ctx.IncentivosCorte
                   .Include(x => x.Entidad)
                   .Where(x =>   (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                      .ToList();
                }
                else
                {
                    return ctx.IncentivosCorte
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

        public List<IncentivosCorte> GetAllPeriodo2(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;




                //if (EntidadId == 0)
                //{
                //    return ctx.IncentivosCorte
                //        .Include(x => x.Entidad)
                //        .Where(x =>   x.Fec_Emision_Cert_ITSE != null && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                //        .ToList();
                //}
                //else
                //{
                //    return ctx.IncentivosCorte
                //    .Include(x => x.Entidad)
                //    .Where(x => x.EntidadId == EntidadId && x.Fec_Emision_Cert_ITSE != null && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                //    .ToList();
                //}


                if (EntidadId == 0)
                {
                    return ctx.IncentivosCorte
                        .Include(x => x.Entidad)
                        .Where(x =>   (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                        .ToList();
                }
                else
                {
                    return ctx.IncentivosCorte
                    .Include(x => x.Entidad)
                    .Where(x => x.EntidadId == EntidadId &&   (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                    .ToList();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IncentivosCorte> GetAllPeriodoTodo(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                if (EntidadId == 0) 
                {
                    return ctx.IncentivosCorte
                    .Include(x => x.Entidad) 
                       .ToList();
                }
                else 
                {
                    return ctx.IncentivosCorte
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


    
        public List<IncentivosCorte> GetByIncentivosCorteBajoMedio(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                //return ctx.IncentivosCorte
                //        .Include(x => x.Entidad)
                //        .Where(x=> (x.NivelRiesgo==1 || x.NivelRiesgo == 2) && x.EntidadId== EntidadId && x.Fec_Notificacion_Licencia_Funcionamiento != null && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                //        .ToList();

                return ctx.IncentivosCorte
                   .Include(x => x.Entidad)
                   .Where(x => (x.NivelRiesgo == 1 || x.NivelRiesgo == 2) && x.EntidadId == EntidadId   && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                   .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IncentivosCorte> GetByIncentivosCorteBajoMedio2(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                //return ctx.IncentivosCorte
                //        .Include(x => x.Entidad)
                //        .Where(x => (x.NivelRiesgo == 1 || x.NivelRiesgo == 2) && x.EntidadId == EntidadId && x.Fec_Notificacion_Licencia_Funcionamiento != null && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                //        .ToList();

                return ctx.IncentivosCorte
                        .Include(x => x.Entidad)
                        .Where(x => (x.NivelRiesgo == 1 || x.NivelRiesgo == 2) && x.EntidadId == EntidadId   && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IncentivosCorte> GetByIncentivosCorteAltoMuyAlto(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                //return ctx.IncentivosCorte
                //      .Include(x => x.Entidad)
                //        .Where(x => (x.NivelRiesgo == 3 || x.NivelRiesgo == 4) && x.EntidadId == EntidadId && x.Fec_Emision_Cert_ITSE != null && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4  || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                //        .ToList();

                return ctx.IncentivosCorte
                  .Include(x => x.Entidad)
                    .Where(x => (x.NivelRiesgo == 3 || x.NivelRiesgo == 4) && x.EntidadId == EntidadId   && (x.Fec_Ing_Sol.Value.Month == 3 || x.Fec_Ing_Sol.Value.Month == 4 || x.Fec_Ing_Sol.Value.Month == 5 || x.Fec_Ing_Sol.Value.Month == 6))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IncentivosCorte> GetByIncentivosCorteAltoMuyAlto2(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                //return ctx.IncentivosCorte
                //      .Include(x => x.Entidad)
                //        .Where(x => (x.NivelRiesgo == 3 || x.NivelRiesgo == 4) && x.EntidadId == EntidadId && x.Fec_Emision_Cert_ITSE != null && ( x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                //        .ToList();

                return ctx.IncentivosCorte
              .Include(x => x.Entidad)
                .Where(x => (x.NivelRiesgo == 3 || x.NivelRiesgo == 4) && x.EntidadId == EntidadId   && (x.Fec_Ing_Sol.Value.Month == 8 || x.Fec_Ing_Sol.Value.Month == 9 || x.Fec_Ing_Sol.Value.Month == 10 || x.Fec_Ing_Sol.Value.Month == 11 || x.Fec_Ing_Sol.Value.Month == 12))
                .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public IncentivosCorte GetByone(long IncentivosCorteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.IncentivosCorte 
                            .Include(x => x.ArchivoAdjunto)
                              .Include(x => x.Entidad)
                        .FirstOrDefault(x => x.IncentivosCorteId == IncentivosCorteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public new void Save(IncentivosCorte obj)
        {
            try
            { 
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.IncentivosCorteId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
