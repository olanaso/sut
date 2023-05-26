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
    public class IncentivosCorteService : IIncentivosCorteService
    {
        private readonly ILogService<IncentivosCorteService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIncentivosCorteRepository _IncentivosCorteRepository;

        public IncentivosCorteService(IUnitOfWork unitOfWork,
                            IIncentivosCorteRepository IncentivosCorteRepository)
        {
            _logger = new LogService<IncentivosCorteService>();
            _unitOfWork = unitOfWork;
            _IncentivosCorteRepository = IncentivosCorteRepository;
        }



        public List<IncentivosCorte> GetByIncentivosCorte(IncentivosCorte filtro, long EntidadId,int pageIndex, int pageSize, Int32 tipoperiodo, ref int totalRows)
        {
            try
            {
                List<IncentivosCorte> query = new List<IncentivosCorte>();

                if (tipoperiodo == 1)
                {
                     query = _IncentivosCorteRepository.GetAllPeriodo1(EntidadId);
                } else if (tipoperiodo == 2)
                {
                    query = _IncentivosCorteRepository.GetAllPeriodo2(EntidadId);
                }
                else
                { 
                     query = _IncentivosCorteRepository.GetAllPeriodoTodo(EntidadId);
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

        public List<IncentivosCorte> GetByImformacionIncentivosCorte(long EntidadId, int Tipoperiodo, Rol Roles )
        {
            try
            {
                List<IncentivosCorte> query = new List<IncentivosCorte>();


                if (Roles == Rol.Administrador) { 

                    if (Tipoperiodo == 0)
                    {
                        query = _IncentivosCorteRepository.GetAllPeriodorepAdmin(EntidadId);
                    }
                    else if (Tipoperiodo == 1)
                    {
                        query = _IncentivosCorteRepository.GetAllPeriodo1RepAdmin(EntidadId);
                    }
                    else if (Tipoperiodo == 2)
                    {
                        query = _IncentivosCorteRepository.GetAllPeriodo2RepAdmin(EntidadId);
                    }
                }
                else 
                {
                    if (Tipoperiodo == 0)
                    {
                        query = _IncentivosCorteRepository.GetAllPeriodoTodo(EntidadId);
                    }
                    else if (Tipoperiodo == 1)
                    {
                        query = _IncentivosCorteRepository.GetAllPeriodo1(EntidadId);
                    }
                    else if (Tipoperiodo == 2)
                    {
                        query = _IncentivosCorteRepository.GetAllPeriodo2(EntidadId);
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


        public List<IncentivosCorte> GetByIncentivosCorteBajoMedio(long EntidadId, int tipoperiodo)
        {
            try
            {
                List<IncentivosCorte> query = new List<IncentivosCorte>();
                if (tipoperiodo == 1)
                {
                    query = _IncentivosCorteRepository.GetByIncentivosCorteBajoMedio(EntidadId);
                }
                else if (tipoperiodo == 2)
                {
                    query = _IncentivosCorteRepository.GetByIncentivosCorteBajoMedio2(EntidadId);
                } 

                var result = query.ToList();

                return result;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IncentivosCorte> GetByIncentivosCorteAltoMuyAlto(long EntidadId, int tipoperiodo)
        {
            try
            { 
                List<IncentivosCorte> query = new List<IncentivosCorte>();

                if (tipoperiodo == 1)
                {
                    query = _IncentivosCorteRepository.GetByIncentivosCorteAltoMuyAlto(EntidadId);
                }
                else if (tipoperiodo == 2)
                {
                    query = _IncentivosCorteRepository.GetByIncentivosCorteAltoMuyAlto2(EntidadId);
                }



                var result = query.ToList();

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }










        public IncentivosCorte GetByone(long UsuarioId)
        {
            try
            {
                return _IncentivosCorteRepository.GetByone(UsuarioId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IncentivosCorte> listaByIncentivosCorte()
        {
            try
            {
                return _IncentivosCorteRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        public void Save(IncentivosCorte obj)
        {
            try
            {
                _IncentivosCorteRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
