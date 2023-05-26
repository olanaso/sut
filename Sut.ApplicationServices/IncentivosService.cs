using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using Sut.Log;
using Sut.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.ApplicationServices
{
    public class IncentivosService : IIncentivosService
    {
        private readonly ILogService<IncentivosService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIncentivosRepository _IncentivosRepository;

        public IncentivosService(IUnitOfWork unitOfWork,
                            IIncentivosRepository IncentivosRepository)
        {
            _logger = new LogService<IncentivosService>();
            _unitOfWork = unitOfWork;
            _IncentivosRepository = IncentivosRepository;
        }



        public List<Incentivos> GetByIncentivos(Incentivos filtro, long EntidadId,int pageIndex, int pageSize, Int32 tipoperiodo, ref int totalRows)
        {
            try
            {
                List<Incentivos> query = new List<Incentivos>();

                if (tipoperiodo == 1)
                {
                     query = _IncentivosRepository.GetAllPeriodo1(EntidadId);
                } else if (tipoperiodo == 2)
                {
                    query = _IncentivosRepository.GetAllPeriodo2(EntidadId);
                }
                else
                { 
                     query = _IncentivosRepository.GetAllPeriodoTodo(EntidadId);
                }


                totalRows = query.Count();
                var result = query.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Incentivos> GetByImformacionIncentivos(long EntidadId, int Tipoperiodo, Rol Roles )
        {
            try
            {
                List<Incentivos> query = new List<Incentivos>();


                if (Roles == Rol.Administrador) { 

                    if (Tipoperiodo == 0)
                    {
                        query = _IncentivosRepository.GetAllPeriodorepAdmin(EntidadId);
                    }
                    else if (Tipoperiodo == 1)
                    {
                        query = _IncentivosRepository.GetAllPeriodo1RepAdmin(EntidadId);
                    }
                    else if (Tipoperiodo == 2)
                    {
                        query = _IncentivosRepository.GetAllPeriodo2RepAdmin(EntidadId);
                    }
                }
                else 
                {
                    if (Tipoperiodo == 0)
                    {
                        query = _IncentivosRepository.GetAllPeriodoTodo(EntidadId);
                    }
                    else if (Tipoperiodo == 1)
                    {
                        query = _IncentivosRepository.GetAllPeriodo1(EntidadId);
                    }
                    else if (Tipoperiodo == 2)
                    {
                        query = _IncentivosRepository.GetAllPeriodo2(EntidadId);
                    }

                }


                var result = query.ToList();

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Incentivos> GetByIncentivosBajoMedio(long EntidadId, int tipoperiodo)
        {
            try
            {
                List<Incentivos> query = new List<Incentivos>();
                if (tipoperiodo == 1)
                {
                    query = _IncentivosRepository.GetByIncentivosBajoMedio(EntidadId);
                }
                else if (tipoperiodo == 2)
                {
                    query = _IncentivosRepository.GetByIncentivosBajoMedio2(EntidadId);
                } 

                var result = query.ToList();

                return result;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Incentivos> GetByIncentivosAltoMuyAlto(long EntidadId, int tipoperiodo)
        {
            try
            { 
                List<Incentivos> query = new List<Incentivos>();

                if (tipoperiodo == 1)
                {
                    query = _IncentivosRepository.GetByIncentivosAltoMuyAlto(EntidadId);
                }
                else if (tipoperiodo == 2)
                {
                    query = _IncentivosRepository.GetByIncentivosAltoMuyAlto2(EntidadId);
                }



                var result = query.ToList();

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }










        public Incentivos GetByone(long UsuarioId)
        {
            try
            {
                return _IncentivosRepository.GetByone(UsuarioId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Incentivos> listaByIncentivos()
        {
            try
            {
                return _IncentivosRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        public void Save(Incentivos obj)
        {
            try
            {
                _IncentivosRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
