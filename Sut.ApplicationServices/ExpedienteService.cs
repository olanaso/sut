﻿using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IDomainServices;
using Sut.IRepositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sut.ApplicationServices
{
    public class ExpedienteService : IExpedienteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExpedienteRepository _expedienteRepository;
        private readonly IExpedienteDomainService _expedienteDomainService;
        private readonly IProcedimientoRepository _procedimientoRepository;
        private readonly IEntidadRepository _entidadRepository;
        private readonly ITupaRepository _tupaRepository;
        private readonly ITablaAsmeRepository _tablaAsmeRepository;
        private readonly IInductorValorRepository _inductorValorRepository;
        private readonly IInductorRepository _inductorRepository;
        private readonly IActividadRepository _actividadRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuditoriaRepository _auditoriaRepository;
        private readonly IProcedimientoCategoriaService _ProcedimientoCategoriaService;

        public ExpedienteService(IUnitOfWork unitOfWork,
                                    IExpedienteRepository expedienteRepository,
                                    IExpedienteDomainService expedienteDomainService,
                                    IProcedimientoRepository procedimientoRepository,
                                    IEntidadRepository entidadRepository,
                                    ITupaRepository tupaRepository,
                                    ITablaAsmeRepository tablaAsmeRepository,
                                    IInductorValorRepository inductorValorRepository,
                                    IInductorRepository inductorRepository,
                                    IActividadRepository actividadRepository,
                                    IUsuarioRepository usuarioRepository,
                                    IAuditoriaRepository auditoriaRepository,
                                        IProcedimientoCategoriaService procedimientoCategoriaService)
        {
            _unitOfWork = unitOfWork;
            _expedienteRepository = expedienteRepository;
            _expedienteDomainService = expedienteDomainService;
            _procedimientoRepository = procedimientoRepository;
            _entidadRepository = entidadRepository;
            _tupaRepository = tupaRepository;
            _tablaAsmeRepository = tablaAsmeRepository;
            _inductorValorRepository = inductorValorRepository;
            _inductorRepository = inductorRepository;
            _actividadRepository = actividadRepository;
            _usuarioRepository = usuarioRepository;
            _auditoriaRepository = auditoriaRepository;
            _ProcedimientoCategoriaService = procedimientoCategoriaService;
        }
        

         public List<Expediente> GetByEntidadCompleto()
        {
            try
            {
                return _expedienteRepository.GetByEntidadCompleto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Expediente> GetByEntidad(long EntidadId)
        {
            try
            {
                return _expedienteRepository.GetByEntidad(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Procedimiento> GetByEstados(long ExpedienteId)
        {
            try
            {
                return _expedienteRepository.GetByEstados(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        public List<Expediente> GetByEntidadMax(long EntidadId)
        {
            try
            {
                return _expedienteRepository.GetByEntidadMax(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Expediente GetOneActivo(long EntidadId)
        {
            try
            {
                return _expedienteRepository.GetOneActivo(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Expediente> GetByEntidadAnulado(long EntidadId)
        {
            try
            {
                return _expedienteRepository.GetByEntidadAnulado(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Expediente GetOneEntidad(long ExpedienteId, long EntidadId)
        {
            try
            {
                return _expedienteRepository.GetOneEntidad(ExpedienteId, EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Expediente GetOne(long ExpedienteId)
        {
            try
            {
                return _expedienteRepository.GetOne(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, int> GetEstadisticas(long ExpedienteId)
        {
            try
            {
                Dictionary<string, int> dic = new Dictionary<string, int>();
                var procs = _procedimientoRepository
                    .GetAllBy(x => x.ExpedienteId == ExpedienteId && x.Estado !=3)
                    .ToList();

                dic["total"] = procs.Count(x => x.Operacion != OperacionExpediente.Eliminacion);
                //dic["total_dg"] = procs.Count(x => x.Operacion != OperacionExpediente.Eliminacion && x.Operacion != OperacionExpediente.Ninguna);
                dic["total_dg"] = procs.Count(x => x.Operacion != OperacionExpediente.Eliminacion);
                //dic["total_st"] = procs.Count(x => x.TipoProcedimiento != TipoProcedimiento.Estandar && x.Operacion != OperacionExpediente.Ninguna && x.Operacion != OperacionExpediente.Eliminacion);
                //dic["total_st"] = procs.Count(x => x.TipoProcedimiento != TipoProcedimiento.Estandar && x.Operacion != OperacionExpediente.Eliminacion);
                dic["total_st"] = procs.Count(x => x.Operacion != OperacionExpediente.Eliminacion);

                //dic["Informacion_ciudadano"] = procs.Count(x => x.InfCdnoTerminado && x.Operacion != OperacionExpediente.Eliminacion);
                //dic["Informacion_ciudadano_pct"] = dic["Informacion_ciudadano"] * 100 / (dic["total_dg"] == 0 ? 1 : dic["total_dg"]);
                //dic["datos_generales"] = procs.Count(x => x.DatosGeneralesTerminado && x.Operacion != OperacionExpediente.Eliminacion); 
                //dic["datos_generales_pct"] = dic["datos_generales"] * 100 / (dic["total_dg"] == 0 ? 1 : dic["total_dg"]);
                //dic["sustento_tecnico"] = procs.Count(x => x.SustTecnicoTerminado && x.TipoProcedimiento != TipoProcedimiento.Estandar);
                //dic["sustento_tecnico_pct"] = dic["sustento_tecnico"] * 100 / (dic["total_st"] == 0 ? 1 : dic["total_st"]);
                //dic["tabla_asme"] = procs.Count(x => x.TablaAsmeTerminado && x.TipoProcedimiento != TipoProcedimiento.Estandar);
                //dic["tabla_asme_pct"] = dic["tabla_asme"] * 100 / (dic["total_st"] == 0 ? 1 : dic["total_st"]);


                dic["Informacion_ciudadano"] = procs.Count(x => x.InfCdnoTerminado && x.Operacion != OperacionExpediente.Eliminacion);
                dic["Informacion_ciudadano_pct"] = dic["Informacion_ciudadano"] * 100 / (dic["total_dg"] == 0 ? 1 : dic["total_dg"]);
                dic["datos_generales"] = procs.Count(x => x.DatosGeneralesTerminado && x.Operacion != OperacionExpediente.Eliminacion);
                dic["datos_generales_pct"] = dic["datos_generales"] * 100 / (dic["total_dg"] == 0 ? 1 : dic["total_dg"]);
                dic["sustento_tecnico"] = procs.Count(x => x.SustTecnicoTerminado && x.Operacion != OperacionExpediente.Eliminacion);
                dic["sustento_tecnico_pct"] = dic["sustento_tecnico"] * 100 / (dic["total_st"] == 0 ? 1 : dic["total_st"]);
                dic["tabla_asme"] = procs.Count(x => x.TablaAsmeTerminado && x.Operacion != OperacionExpediente.Eliminacion);
                dic["tabla_asme_pct"] = dic["tabla_asme"] * 100 / (dic["total_st"] == 0 ? 1 : dic["total_st"]);

                return dic;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Dictionary<string, int> GetEstadisticasconteo(Procedimiento filtro)
        {
            try
            {
                Dictionary<string, int> dic = new Dictionary<string, int>();


                List<Procedimiento> query = new List<Procedimiento>();


                if (filtro.Ascendente == 1)
                {
                    query = _procedimientoRepository.GetByExpediente(filtro.ExpedienteId).OrderByDescending(x => x.Numero).ToList();

                }
                else
                {
                    query = _procedimientoRepository.GetByExpediente(filtro.ExpedienteId).OrderBy(x => x.Numero).ToList();
                }


                var data = query
                          .Where(x => x.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Denominacion) ? x.Denominacion : filtro.Denominacion).ToUpper()));

                if (filtro.CodigoCorto != null)
                {
                    data = data.Where(x => x.CodigoCorto != null);
                    data = data.Where(x => x.CodigoCorto.ToUpper().Contains((string.IsNullOrEmpty(filtro.CodigoCorto) ? x.CodigoCorto : filtro.CodigoCorto).ToUpper()));

                }



                if (filtro.FiltroOperacion > 0)
                {
                    data = data.Where(x => (short)x.Operacion == filtro.FiltroOperacion);
                }
                else
                {
                    data = data.Where(x => (short)x.Operacion != 3);
                }

                if (filtro.UndOrgResponsable != null)
                {
                    if (filtro.UndOrgResponsable.Nombre != null)
                    {

                        data = data.Where(x => x.UndOrgResponsable.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.UndOrgResponsable.Nombre) ? x.UndOrgResponsable.Nombre : filtro.UndOrgResponsable.Nombre).ToUpper()));
                    }
                }

                if (filtro.FiltroTipoProcedimiento > 0)
                {
                    if (filtro.FiltroTipoProcedimiento == 4)
                    {
                        data = data.Where(x => x.CodigoACR != "0");
                    }
                    else if (filtro.FiltroTipoProcedimiento == 5)
                    {
                        data = data.Where(x => x.CodigoACR == "0");
                    }
                    else
                    {
                        data = data.Where(x => (short)x.TipoProcedimiento == filtro.FiltroTipoProcedimiento);
                    }

                }









                var procs = data.ToList();

                dic["total"] = procs.Count(x => x.Operacion != OperacionExpediente.Eliminacion);
                //dic["total_dg"] = procs.Count(x => x.Operacion != OperacionExpediente.Eliminacion && x.Operacion != OperacionExpediente.Ninguna);
                dic["total_dg"] = procs.Count(x => x.Operacion != OperacionExpediente.Eliminacion);
                //dic["total_st"] = procs.Count(x => x.TipoProcedimiento != TipoProcedimiento.Estandar && x.Operacion != OperacionExpediente.Ninguna && x.Operacion != OperacionExpediente.Eliminacion);
                //dic["total_st"] = procs.Count(x => x.TipoProcedimiento != TipoProcedimiento.Estandar && x.Operacion != OperacionExpediente.Eliminacion);
                dic["total_st"] = procs.Count(x => x.Operacion != OperacionExpediente.Eliminacion);

                //dic["Informacion_ciudadano"] = procs.Count(x => x.InfCdnoTerminado && x.Operacion != OperacionExpediente.Eliminacion);
                //dic["Informacion_ciudadano_pct"] = dic["Informacion_ciudadano"] * 100 / (dic["total_dg"] == 0 ? 1 : dic["total_dg"]);
                //dic["datos_generales"] = procs.Count(x => x.DatosGeneralesTerminado && x.Operacion != OperacionExpediente.Eliminacion); 
                //dic["datos_generales_pct"] = dic["datos_generales"] * 100 / (dic["total_dg"] == 0 ? 1 : dic["total_dg"]);
                //dic["sustento_tecnico"] = procs.Count(x => x.SustTecnicoTerminado && x.TipoProcedimiento != TipoProcedimiento.Estandar);
                //dic["sustento_tecnico_pct"] = dic["sustento_tecnico"] * 100 / (dic["total_st"] == 0 ? 1 : dic["total_st"]);
                //dic["tabla_asme"] = procs.Count(x => x.TablaAsmeTerminado && x.TipoProcedimiento != TipoProcedimiento.Estandar);
                //dic["tabla_asme_pct"] = dic["tabla_asme"] * 100 / (dic["total_st"] == 0 ? 1 : dic["total_st"]);


                dic["Informacion_ciudadano"] = procs.Count(x => x.InfCdnoTerminado && x.Operacion != OperacionExpediente.Eliminacion);
                dic["Informacion_ciudadano_pct"] = dic["Informacion_ciudadano"] * 100 / (dic["total_dg"] == 0 ? 1 : dic["total_dg"]);
                dic["datos_generales"] = procs.Count(x => x.DatosGeneralesTerminado && x.Operacion != OperacionExpediente.Eliminacion);
                dic["datos_generales_pct"] = dic["datos_generales"] * 100 / (dic["total_dg"] == 0 ? 1 : dic["total_dg"]);
                dic["sustento_tecnico"] = procs.Count(x => x.SustTecnicoTerminado && x.Operacion != OperacionExpediente.Eliminacion);
                dic["sustento_tecnico_pct"] = dic["sustento_tecnico"] * 100 / (dic["total_st"] == 0 ? 1 : dic["total_st"]);
                dic["tabla_asme"] = procs.Count(x => x.TablaAsmeTerminado && x.Operacion != OperacionExpediente.Eliminacion);
                dic["tabla_asme_pct"] = dic["tabla_asme"] * 100 / (dic["total_st"] == 0 ? 1 : dic["total_st"]);

                return dic;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Expediente> GetAllLikePagin(Expediente filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Expediente> query = _expedienteRepository.GetByEntidad(filtro.EntidadId);

                var data = query.Where(x => x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper()))
                            .OrderByDescending(x => x.ExpedienteId);

                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Expediente> GetAllLikePaginRatificador(Expediente filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Expediente> query = _expedienteRepository.GetByEntidadRatificador(filtro.ProvinciaId);

                var data = query.Where(x => x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper()))
                            .OrderByDescending(x => x.ExpedienteId);

                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Expediente> GetAllLikePaginTodo(Expediente filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Expediente> query = _expedienteRepository.GetAllLikePaginTodo();
				if (filtro.Entidad.Provincia.Departamento.DepartamentoId > 0)
                {
                    query = _expedienteRepository.GetAllLikePaginTodo(filtro.Entidad.Provincia.Departamento.DepartamentoId);
                }
                var data = query.Where(x => x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper()));

                if (filtro.FecCreaciontexto != null)
                {

                    data = data.Where(x => x.FecCreacion.ToString().Contains((filtro.FecCreaciontexto ?? x.FecCreacion.ToString()).ToUpper()));
                }
                if (filtro.Entidadtexto != null)
                {

                    data = data.Where(x => x.Entidad.Nombre.ToString().Contains((filtro.Entidadtexto ?? x.Entidad.Nombre.ToString()).ToUpper()));
                }
                if (filtro.EstadoTipo > 0)
                {
                    if (filtro.EstadoTipo == 1)
                    {
                        data = data.Where(x => x.TipoExpediente == TipoExpediente.ExpedienteRegular);
                    }
                    else if (filtro.EstadoTipo == 2)
                    {
                        data = data.Where(x => x.TipoExpediente == TipoExpediente.CargaInicial);
                    } 
                }

                if (filtro.EstadoId > 0)
                {
                    if (filtro.EstadoId == 1)
                    {
                        data = data.Where(x => x.EstadoExpediente == EstadoExpediente.EnProceso);
                    }
                    else if (filtro.EstadoId == 2)
                    {
                        data = data.Where(x => x.EstadoExpediente == EstadoExpediente.Presentado);
                    }
                    else if (filtro.EstadoId == 3)
                    {
                        data = data.Where(x => x.EstadoExpediente == EstadoExpediente.Aprobado);
                    }
                    else if (filtro.EstadoId == 4)
                    {
                        data = data.Where(x => x.EstadoExpediente == EstadoExpediente.Observado);
                    }
                    else if (filtro.EstadoId == 5)
                    {
                        data = data.Where(x => x.EstadoExpediente == EstadoExpediente.Anulado);
                    }
                    else if (filtro.EstadoId == 6)
                    {
                        data = data.Where(x => x.EstadoExpediente == EstadoExpediente.Subsanado);
                    }
                    else if (filtro.EstadoId == 7)
                    {
                        data = data.Where(x => x.EstadoExpediente == EstadoExpediente.Publicado);
                    } 

                }

                if (filtro.publicarCIU > 0)
                {
                     
                        data = data.Where(x => x.publicarCIU == filtro.publicarCIU); 
                }
                if (filtro.PublicEstandar > 0)
                {

                    data = data.Where(x => x.PublicEstandar == filtro.PublicEstandar);
                }
				if (filtro.ProvinciaId > 0)
                {
                    data = data.Where(x => x.ProvinciaId == filtro.ProvinciaId);
                 }
                //data = query.OrderByDescending(x => x.ExpedienteId);

                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Expediente> GetAllLikePaginAsigando(Expediente filtro, int pageIndex, int pageSize, ref int totalRows,long UsuarioId)
        {
            try
            {




                var query1 = _entidadRepository.lista_entidadesAsignado(0, UsuarioId);
                 

                List<Expediente> query = _expedienteRepository.GetAllLikePaginAsigando(); 

                var excludedIDs = new HashSet<string>(query1.Select(p => p.EntidadId.ToString()));  

                var data = query.Where(p => excludedIDs.Contains(p.EntidadId.ToString())); 




                //var data = query.Where(x => x.EntidadId.ToUpper().Contains(query1.).ToUpper()));

                  data = data.Where(x => x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper()));

                if (filtro.FecCreaciontexto != null)
                {

                    data = data.Where(x => x.FecCreacion.ToString().Contains((filtro.FecCreaciontexto ?? x.FecCreacion.ToString()).ToUpper()));
                }
                if (filtro.Entidadtexto != null)
                {

                    data = data.Where(x => x.Entidad.Nombre.ToString().Contains((filtro.Entidadtexto ?? x.Entidad.Nombre.ToString()).ToUpper()));
                }
                if (filtro.EstadoTipo > 0)
                {
                    if (filtro.EstadoTipo == 1)
                    {
                        data = data.Where(x => x.TipoExpediente == TipoExpediente.ExpedienteRegular);
                    }
                    else if (filtro.EstadoTipo == 2)
                    {
                        data = data.Where(x => x.TipoExpediente == TipoExpediente.CargaInicial);
                    }
                }

                if (filtro.EstadoId > 0)
                {
                    if (filtro.EstadoId == 1)
                    {
                        data = data.Where(x => x.EstadoExpediente == EstadoExpediente.EnProceso);
                    }
                    else if (filtro.EstadoId == 2)
                    {
                        data = data.Where(x => x.EstadoExpediente == EstadoExpediente.Presentado);
                    }
                    else if (filtro.EstadoId == 3)
                    {
                        data = data.Where(x => x.EstadoExpediente == EstadoExpediente.Aprobado);
                    }
                    else if (filtro.EstadoId == 4)
                    {
                        data = data.Where(x => x.EstadoExpediente == EstadoExpediente.Observado);
                    }
                    else if (filtro.EstadoId == 5)
                    {
                        data = data.Where(x => x.EstadoExpediente == EstadoExpediente.Anulado);
                    }
                    else if (filtro.EstadoId == 6)
                    {
                        data = data.Where(x => x.EstadoExpediente == EstadoExpediente.Subsanado);
                    }
                    else if (filtro.EstadoId == 7)
                    {
                        data = data.Where(x => x.EstadoExpediente == EstadoExpediente.Publicado);
                    }

                }

                if (filtro.publicarCIU > 0)
                {

                    data = data.Where(x => x.publicarCIU == filtro.publicarCIU);
                }
                if (filtro.PublicEstandar > 0)
                {

                    data = data.Where(x => x.PublicEstandar == filtro.PublicEstandar);
                }
                //data = query.OrderByDescending(x => x.ExpedienteId);

                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Procedimiento> GetAllLikePaginTodoConfigurarProce(Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Procedimiento> query = _expedienteRepository.GetAllLikePaginTodoConfigurarProce();

                var data = query.Where(x => x.Expediente.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Codigo) ? x.Expediente.Codigo : filtro.Expediente.Codigo).ToUpper())
                && x.Expediente.Entidad.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Entidad.Nombre) ? x.Expediente.Entidad.Nombre : filtro.Expediente.Entidad.Nombre).ToUpper())
                && x.CodigoCorto.ToUpper().Contains((string.IsNullOrEmpty(filtro.CodigoCorto) ? x.CodigoCorto : filtro.CodigoCorto).ToUpper())
                && x.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Denominacion) ? x.Denominacion : filtro.Denominacion).ToUpper())
                )
                            .OrderByDescending(x => x.ProcedimientoId);

                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Procedimiento> GetAllLikePaginTodoConfigurar(Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Procedimiento> query = _expedienteRepository.GetAllLikePaginTodoConfigurar();

                var data = query.Where(x => x.Expediente.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Codigo) ? x.Expediente.Codigo : filtro.Expediente.Codigo).ToUpper())
                && x.Expediente.Entidad.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Entidad.Nombre) ? x.Expediente.Entidad.Nombre : filtro.Expediente.Entidad.Nombre).ToUpper())
                && x.CodigoCorto.ToUpper().Contains((string.IsNullOrEmpty(filtro.CodigoCorto) ? x.CodigoCorto : filtro.CodigoCorto).ToUpper())
                && x.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Denominacion) ? x.Denominacion : filtro.Denominacion).ToUpper())
                )
                            .OrderByDescending(x => x.ProcedimientoId);

                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Expediente> GetAllLikePaginSOLICITUDWORD(Expediente filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Expediente> query = _expedienteRepository.GetAllLikePaginSOLICITUDWORD();

                var data = query.Where(x => x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper())
                && x.Entidad.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Entidad.Nombre) ? x.Entidad.Nombre : filtro.Entidad.Nombre).ToUpper())
                )
                            .OrderByDescending(x => x.ExpedienteId);

                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public List<Requisito> GetAllLikePaginTodorequisitos(Requisito filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Requisito> query = _expedienteRepository.GetAllLikePaginTodorequisitos(filtro.ProcedimientoId);

                var data = query.Where(x => x.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Nombre) ? x.Nombre : filtro.Nombre).ToUpper()))
                            .OrderBy(x => x.RecNum);

                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Expediente> GetAllLikePaginFiscalizador(Expediente filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Expediente> query = _expedienteRepository.GetByEntidadFiscalizador(filtro.EstadoExpediente);

                var data = query.Where(x => x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper()))
                            .OrderByDescending(x => x.ExpedienteId);

                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Expediente> GetAllLikePaginEvaluador(Expediente filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Expediente> query = _expedienteRepository.GetByEntidadEvaluador(filtro.SectorId);

                var data = query.Where(x => x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper()))
                            .OrderByDescending(x => x.ExpedienteId);

                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Expediente> GetAllLikePaginEvaluadorMEFPCM(Expediente filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Expediente> query = _expedienteRepository.GetAllLikePaginEvaluadorMEFPCM(filtro.EstadoEvaluadorMinisterio);

                var data = query.Where(x => x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper()))
                            .OrderByDescending(x => x.ExpedienteId);

                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<Expediente> PresentadosGetAllLikePagin(Expediente filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Expediente> query = _expedienteRepository.GetAllBy(x => x.EstadoExpediente == EstadoExpediente.Presentado 
                                                                            && x.Entidad.TipoTupa == TipoTupa.Normal);

                var data = query.Where(x => x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper())
                                        && x.Entidad.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Entidad.Nombre) ? x.Entidad.Nombre : filtro.Entidad.Nombre).ToUpper()))
                            .OrderBy(x => x.Entidad.Nombre);

                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Expediente> GetAllLikePaginRptActividad(Expediente filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {


               


                //var data = query.Where(x => x.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Nombre) ? x.Nombre : filtro.Nombre).ToUpper())
                //                        && x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper())
                //                        && x.Acronimo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Acronimo) ? x.Acronimo : filtro.Acronimo).ToUpper())
                //                    );

                //if (filtro.DepartamentoId > 0) data = data.Where(x => x.ProvinciaId != null);

                //if (filtro.NivelGobiernoId > 0) data = data.Where(x => x.NivelGobiernoId == filtro.NivelGobiernoId);
                //if (filtro.SectorId > 0) data = data.Where(x => x.SectorId == filtro.SectorId);

                //if (filtro.DepartamentoId > 0)
                //{
                //    data = data.Where(x => x.DepartamentoId == filtro.DepartamentoId);
                //    if (filtro.ProvinciaId > 0) data = data.Where(x => x.ProvinciaId == filtro.ProvinciaId);
                //}


                //data = data.OrderBy(x => x.Nombre);

                //totalRows = data.Count();
                //var result = data.Skip((pageIndex - 1) * pageSize)
                //                .Take(pageSize)
                //                .ToList();
                 

                

                List<Expediente> query = _expedienteRepository.GetAllByRptActividad(filtro.ExpedienteId,filtro.ptiporeporte);



                var data = query.Where(x => x.campo4.ToUpper().Contains((string.IsNullOrEmpty(filtro.campo4) ? x.campo4 : filtro.campo4).ToUpper())
                                        && x.campo5.ToUpper().Contains((string.IsNullOrEmpty(filtro.campo5) ? x.campo5 : filtro.campo5).ToUpper())
                                        && x.campo7.ToUpper().Contains((string.IsNullOrEmpty(filtro.campo7) ? x.campo7 : filtro.campo7).ToUpper()));

                if (filtro.ptiporeporte == 1)
                {
                    data = data.Where(x => x.campo9.ToUpper().Contains((string.IsNullOrEmpty(filtro.campo9) ? x.campo9 : filtro.campo9).ToUpper()))
                        .OrderBy(x => x.campo2);

                    if (filtro.campo12 > 0)
                    {
                        data = data.Where(x => x.campo12 >= filtro.campo12);
                    }

                    
                }
                else if (filtro.ptiporeporte == 2)
                {
                    data = data.Where(x => x.campo10.ToUpper().Contains((string.IsNullOrEmpty(filtro.campo10) ? x.campo10 : filtro.campo10).ToUpper())
                                        && x.campo11.ToUpper().Contains((string.IsNullOrEmpty(filtro.campo11) ? x.campo11 : filtro.campo11).ToUpper()))
                       .        OrderBy(x => x.campo2); 
                }





                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Expediente> GetAllLikePaginRptActividadRPT(Expediente filtro)
        {
            try
            {
                  

                List<Expediente> query = _expedienteRepository.GetAllByRptActividad(filtro.ExpedienteId, filtro.ptiporeporte);



                var data = query.Where(x => x.campo4.ToUpper().Contains((string.IsNullOrEmpty(filtro.campo4) ? x.campo4 : filtro.campo4).ToUpper())
                                        && x.campo5.ToUpper().Contains((string.IsNullOrEmpty(filtro.campo5) ? x.campo5 : filtro.campo5).ToUpper())
                                        && x.campo7.ToUpper().Contains((string.IsNullOrEmpty(filtro.campo7) ? x.campo7 : filtro.campo7).ToUpper()));

                if (filtro.ptiporeporte == 1)
                {
                    data = data.Where(x => x.campo9.ToUpper().Contains((string.IsNullOrEmpty(filtro.campo9) ? x.campo9 : filtro.campo9).ToUpper()))
                        .OrderBy(x => x.campo2);

                    if (filtro.campo12 > 0)
                    {
                        data = data.Where(x => x.campo12 >= filtro.campo12);
                    }


                }
                else if (filtro.ptiporeporte == 2)
                {
                    data = data.Where(x => x.campo10.ToUpper().Contains((string.IsNullOrEmpty(filtro.campo10) ? x.campo10 : filtro.campo10).ToUpper())
                                        && x.campo11.ToUpper().Contains((string.IsNullOrEmpty(filtro.campo11) ? x.campo11 : filtro.campo11).ToUpper()))
                       .OrderBy(x => x.campo2);
                }
                 

                 
                var result = data.ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Save(Expediente obj)
        {
            try
            {
                _expedienteRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //jleon 05022019 creado
        public string ValidarNuevoExpediente(Expediente obj, long UsuarioId )
        {
            try
            {
                Usuario user = _usuarioRepository.GetOne(UsuarioId);

                var expedientes = _expedienteRepository.GetByEntidadAnulado(user.Entidad.EntidadId);
                bool flag = true;
                string mensaje = "";

                if (expedientes.Count() > 0)
                {
                    long maxId = expedientes.Max(x => x.ExpedienteId);
                    var exp = expedientes.First(x => x.ExpedienteId == maxId);

                    //flag = exp.EstadoExpediente == EstadoExpediente.Aprobado || exp.EstadoExpediente == EstadoExpediente.Anulado || exp.EstadoExpediente == EstadoExpediente.Publicado;
                    if (exp.Entidad.ActivarExpediente != 1) { 
                    flag = exp.EstadoExpediente == EstadoExpediente.Publicado || exp.EstadoExpediente == EstadoExpediente.Anulado;
                    }
                }

                if (flag == false)
                {
                    //throw new Exception("No se puede crear un nuevo Expediente porque se encuentra en proceso de evaluación.");
                    mensaje = "No se puede crear un nuevo expediente por que existe un expediente en proceso.";
                }

                return mensaje;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GenerarNuevo(Expediente obj, long UsuarioId, long Sector, long? Provincia, long EntidadId, long rol)
        {
            try
            {
                Usuario user = _usuarioRepository.GetOne(UsuarioId);


                if (rol != 1)
                {
                    obj.EntidadId = user.Entidad.EntidadId;
                }
                else
                {
                    obj.EntidadId = EntidadId;
                }

                var expedientes = _expedienteRepository.GetByEntidadAnulado(obj.EntidadId); // modificado para la generacion de tupac estandar

                //var expedientes = _expedienteRepository.GetByEntidad(user.Entidad.EntidadId);anter
                //bool flag = true;
                //jleon 05022019 modificado

                //if (expedientes.Count() > 0)
                //{
                //    long maxId = expedientes.Max(x => x.ExpedienteId);
                //    var exp = expedientes.First(x => x.ExpedienteId == maxId);

                //    flag = exp.EstadoExpediente == EstadoExpediente.Aprobado || exp.EstadoExpediente == EstadoExpediente.Anulado;
                //}

                //if (flag)
                //{
                if (expedientes.Count() > 0)
                {
                    long maxId = expedientes.Max(x => x.ExpedienteId);
                    var exp = expedientes.First(x => x.ExpedienteId == maxId);

                    obj.Numero = exp.Numero + 1;
                    //obj.TipoExpediente = TipoExpediente.ExpedienteRegular;
                }
                else
                {
                    obj.Numero = 1;
                    //obj.TipoExpediente = TipoExpediente.CargaInicial;
                }

                if (rol != 1)
                { 
                    obj.EntidadId = user.Entidad.EntidadId;
                }
                else {
                    obj.EntidadId = EntidadId;
                }
                    obj.EstadoExpediente = EstadoExpediente.EnProceso;

                    obj.Ano = DateTime.Now.Year;
                    obj.Codigo = string.Format("{0}-{1:D3}-{2}", user.Entidad.Codigo, obj.Numero, obj.Ano);

                    obj.UserCreacion = user.NroDocumento;
                    obj.UserModificacion = user.NroDocumento;
                    obj.FecCreacion = DateTime.Now;
                    obj.FecModificacion = DateTime.Now; 
                    obj.SectorId = Sector;
                    obj.ProvinciaId = Provincia;

                if (user.Entidad.TipoTupa == TipoTupa.Estandar)
                    {
                        obj.TipoExpediente = TipoExpediente.ExpedienteRegular;
                    }

                    if(obj.TipoExpediente == TipoExpediente.ExpedienteRegular)
                    {
                        CopiarContenidoTupaVigente(obj, user);
                    }
                obj.SustCostosTerminado = true;
                _expedienteRepository.Save(obj);
                    _unitOfWork.SaveChanges();
                //}
                //else
                //{
                //    throw new Exception("No se puede crear un nuevo Expediente porque se encuentra en proceso de evaluación.");
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EsPosibleCargaInicial(long EntidadId)
        {
            try
            {
                bool flag = false;

                var expedientes = _expedienteRepository.GetByEntidad(EntidadId);
                if (expedientes.Count() > 0)
                {
                    long maxId = expedientes.Max(x => x.ExpedienteId);
                    var exp = expedientes.First(x => x.ExpedienteId == maxId);

                    flag = expedientes.Count(x => x.EstadoExpediente == EstadoExpediente.Anulado) == expedientes.Count();
                }
                else
                {
                    flag = true;
                }
                return flag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CopiarContenidoTupaVigente(Expediente newExp, Usuario user)
        {
            try
            {
                Tupa vigente = _tupaRepository.GetOne(x => x.EntidadId == newExp.EntidadId && x.EstadoTupa == EstadoTupa.Vigente);

                if (vigente != null)
                {
                    List<Procedimiento> procs = _procedimientoRepository.GetByExpedienteNoTracking(vigente.ExpedienteId);

                    foreach (Procedimiento p in procs)
                    {
                        p.Operacion = OperacionExpediente.Ninguna;
                        p.TupaId = null;
                        p.TupaId = null;    

                        p.FecCreacion = DateTime.Now;
                        p.UserCreacion = user.NroDocumento;
                        p.FecModificacion = DateTime.Now;
                        p.UserModificacion = user.NroDocumento;
                        p.FlagCopiarProcedimiento = 1;
                        p.SustTecnicoTerminado = true;
                        p.SustLegalTerminado = true; 
                        p.DatosGeneralesTerminado = true; 
                        p.InfCdnoTerminado = true;
                        p.TablaAsmeTerminado = true;  


                        foreach (TablaAsme t in p.TablaAsme)
                        {
                            //t.AsmeActualId = t.TablaAsmeId;
                            t.FecCreacion = DateTime.Now;
                            t.UserCreacion = user.NroDocumento;
                            t.FecModificacion = DateTime.Now;
                            t.UserModificacion = user.NroDocumento;
                        }
                         foreach (ProcedimientoCategoria tcat in p.ProcedimientoCategoria)
                        {
                            tcat.ExpedienteId = 0;
                        }
                    }



                    //List<ProcedimientoCategoria> procat = _ProcedimientoCategoriaService.Lsitaprocedimientocategoriavalor(vigente.ExpedienteId);
                 

                    newExp.Procedimiento.AddRange(procs);
                    //newExp.ProcedimientoCategoria.AddRange(procat);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public void EliminarProcesarCostos(long ExpedienteId)
        {
            try
            {
                _expedienteRepository.EliminarProcesarCostos(ExpedienteId); 
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ProcesarCostos(long ExpedienteId)
        {
            try
            {
                _expedienteRepository.ProcesaCostos(ExpedienteId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CambiarEstadoSustento(long ExpedienteId, bool estado)
        {
            try
            {
                if (estado)
                {
                    var procs = _procedimientoRepository
                        .GetAllBy(x => x.ExpedienteId == ExpedienteId && x.Estado != 3 && x.Operacion != OperacionExpediente.Eliminacion) 
                        .ToList();

                    if (procs.Count(x => x.DatosGeneralesTerminado) < procs.Count()
                        || procs.Count(x => x.SustTecnicoTerminado) < procs.Count() || procs.Count(x => x.TablaAsmeTerminado) < procs.Count())
                        throw new Exception("Debe terminar el registro de la tabla asme.");
                }
                
                Expediente obj = _expedienteRepository.GetOne(x => x.ExpedienteId == ExpedienteId);
                obj.SustCostosTerminado = estado;
                
                _expedienteRepository.SaveOnlyExpediente(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public void activarpublicado(long ExpedienteId, int estado)
        {
            try
            {
                 
                Expediente obj = _expedienteRepository.GetOne(x => x.ExpedienteId == ExpedienteId);
                obj.publicarCIU = estado;

                _expedienteRepository.SaveOnlyExpediente(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void activarinfcondicion(long ExpedienteId, TipoOrdenPa estado)
        {
            try
            {

                Expediente obj = _expedienteRepository.GetOne(x => x.ExpedienteId == ExpedienteId);
                obj.OrdenPa = estado;

                _expedienteRepository.SaveOnlyExpediente(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActivarSolicitudWord(long expedienteId)
        {
            try
            {

                Expediente obj = _expedienteRepository.GetOne(x => x.ExpedienteId == expedienteId);
                obj.EstadoReporteWord = 1;

                _expedienteRepository.SaveOnlyExpediente(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void activarpublicadoexp(long ExpedienteId, int estado)
        {
            try
            {

                Expediente obj = _expedienteRepository.GetOne(x => x.ExpedienteId == ExpedienteId);
                obj.PublicEstandar = estado;

                _expedienteRepository.SaveOnlyExpediente(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void activaImportarPAEliminados(long ExpedienteId, int estado)
        {
            try
            {

                Expediente obj = _expedienteRepository.GetOne(x => x.ExpedienteId == ExpedienteId);
                obj.Importar = estado;

                _expedienteRepository.SaveOnlyExpediente(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CambiarEstado(long ExpedienteId, EstadoExpediente estado)
        {
            try
            {
                Expediente obj = _expedienteRepository.GetOne(ExpedienteId);
                
                switch (estado)
                {
                    case EstadoExpediente.Anulado: obj.FecAnulacion = DateTime.Now; break;
                    case EstadoExpediente.Presentado: obj.FecPresentacion = DateTime.Now; break;
                    case EstadoExpediente.Observado: obj.FecObservacion = DateTime.Now; break;
                    case EstadoExpediente.Aprobado: obj.FecAprobacion = DateTime.Now; break;
                    case EstadoExpediente.Subsanado: obj.FecSubsanado = DateTime.Now; break;
                    case EstadoExpediente.Publicado: obj.FecPublicacion = DateTime.Now; break;
                }

                obj.EstadoExpediente = estado;

                if (estado == EstadoExpediente.Publicado)
                {
                    Tupa vigente = _tupaRepository.GetOne(x => x.EntidadId == obj.EntidadId && x.EstadoTupa == EstadoTupa.Vigente);

                    Tupa oTupa = new Tupa()
                    {
                        EntidadId = obj.EntidadId,
                        ExpedienteId = obj.ExpedienteId,
                        FecPublicacion = DateTime.Now,
                        TipoTupa = obj.Entidad.TipoTupa,
                        EstadoTupa = EstadoTupa.Vigente
                    };

                    if (vigente != null)
                    {
                        vigente.EstadoTupa = EstadoTupa.NoVigente;
                        _tupaRepository.Save(vigente);
                    }

                    _tupaRepository.Insert(oTupa);

                    List<Procedimiento> procs = _procedimientoRepository
                                                .GetAllBy(x => x.ExpedienteId == obj.ExpedienteId
                                                    && x.Operacion != OperacionExpediente.Eliminacion && x.Estado != 3);

                    foreach (Procedimiento p in procs)
                    {
                        oTupa.Procedimiento.Add(p);
                        _procedimientoRepository.SaveOnlyProcedimiento(p);
                    }
                } else if (obj.Entidad.TipoTupa == TipoTupa.Estandar && estado == EstadoExpediente.Aprobado)
                {
                     Tupa vigente = _tupaRepository.GetOne(x => x.EntidadId == obj.EntidadId && x.EstadoTupa == EstadoTupa.Vigente);

                    Tupa oTupa = new Tupa()
                    {
                        EntidadId = obj.EntidadId,
                        ExpedienteId = obj.ExpedienteId,
                        FecPublicacion = DateTime.Now,
                        TipoTupa = obj.Entidad.TipoTupa,
                        EstadoTupa = EstadoTupa.Vigente
                    };

                    if (vigente != null)
                    {
                        vigente.EstadoTupa = EstadoTupa.NoVigente;
                        _tupaRepository.Save(vigente);
                    }

                    _tupaRepository.Insert(oTupa);

                    List<Procedimiento> procs = _procedimientoRepository
                                                .GetAllBy(x => x.ExpedienteId == obj.ExpedienteId
                                                    && x.Operacion != OperacionExpediente.Eliminacion && x.Estado != 3);

                    foreach (Procedimiento p in procs)
                    {
                        oTupa.Procedimiento.Add(p);
                        _procedimientoRepository.SaveOnlyProcedimiento(p);
                    }
                }
                //else if (obj.TipoExpediente == TipoExpediente.CargaInicial && estado == EstadoExpediente.Publicado) {
                //    Tupa vigente = _tupaRepository.GetOne(x => x.EntidadId == obj.EntidadId && x.EstadoTupa == EstadoTupa.Vigente);

                //    Tupa oTupa = new Tupa()
                //    {
                //        EntidadId = obj.EntidadId,
                //        ExpedienteId = obj.ExpedienteId,
                //        FecPublicacion = DateTime.Now,
                //        TipoTupa = obj.Entidad.TipoTupa,
                //        EstadoTupa = EstadoTupa.Vigente
                //    };

                //    if (vigente != null)
                //    {
                //        vigente.EstadoTupa = EstadoTupa.NoVigente;
                //        _tupaRepository.Save(vigente);
                //    }

                //    _tupaRepository.Insert(oTupa);

                //    List<Procedimiento> procs = _procedimientoRepository
                //                                .GetAllBy(x => x.ExpedienteId == obj.ExpedienteId
                //                                    && x.Operacion != OperacionExpediente.Eliminacion && x.Estado !=3);

                //    foreach (Procedimiento p in procs)
                //    {
                //        oTupa.Procedimiento.Add(p);
                //        _procedimientoRepository.SaveOnlyProcedimiento(p);
                //    }
                //}

                _expedienteRepository.SaveOnlyExpediente(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void DespublicarCambiarEstado(long ExpedienteId, EstadoExpediente estado)
        {
            try
            {
                 
                Expediente obj = _expedienteRepository.GetOne(ExpedienteId);

                obj.FecPublicacion = null;
                obj.FecAnulacion = null;
                obj.EstadoExpediente = estado;
                 
                    Tupa vigente = _tupaRepository.GetOne(x => x.EntidadId == obj.EntidadId && x.EstadoTupa == EstadoTupa.Vigente);

                    Tupa oTupa = new Tupa()
                    {
                        EntidadId = obj.EntidadId,
                        ExpedienteId = obj.ExpedienteId,
                        FecPublicacion = DateTime.Now,
                        TipoTupa = obj.Entidad.TipoTupa,
                        EstadoTupa = EstadoTupa.Vigente
                    };

                if (vigente != null)
                    {
                        vigente.EstadoTupa = EstadoTupa.NoVigente;
                        _tupaRepository.Save(vigente);
                    }

                    //_tupaRepository.Save(oTupa);

                    List<Procedimiento> procs = _procedimientoRepository
                                                .GetAllBy(x => x.ExpedienteId == obj.ExpedienteId
                                                    && x.Operacion != OperacionExpediente.Eliminacion && x.Estado != 3);

                    foreach (Procedimiento p in procs)
                    {
                        oTupa.Procedimiento.Add(p);
                        _procedimientoRepository.SaveOnlyProcedimiento(p);
                    }
                

                _expedienteRepository.SaveOnlyExpediente(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void CambiarEstadoEvaluadores(long ExpedienteId, EstadoExpediente estado,short rol, long EntidadId, short EstadoMinistrio)
        {
            try
            {
                Expediente obj = _expedienteRepository.GetOne(ExpedienteId);

                switch (estado)
                {
                    case EstadoExpediente.Anulado: obj.FecAnulacion = DateTime.Now; break;
                    case EstadoExpediente.Presentado: obj.FecPresentacion = DateTime.Now; break;
                    case EstadoExpediente.Observado: obj.FecObservacion = DateTime.Now; break;
                    case EstadoExpediente.Aprobado: obj.FecAprobacion = DateTime.Now; break;
                    case EstadoExpediente.Subsanado: obj.FecSubsanado = DateTime.Now; break;
                    case EstadoExpediente.Publicado: obj.FecPublicacion = DateTime.Now; break;
                }
                if (rol != 8) { 
                    obj.EstadoExpediente = estado;
                }
                if (rol == 6) { 
                    obj.EstadoRactificador = estado;
                } else if (rol == 8)
                {
                    if (EntidadId == 4105)
                    {
                        obj.EstadoFiscalizadorPCM = estado;
                    }else if (EntidadId == 18497)
                    {
                        obj.EstadoFiscalizadorMEF = estado;
                    }
                    else if (EntidadId == 20553)
                    {
                        obj.EstadoFiscalizadorINDECOPI = estado;
                    }

                }
                else if (rol == 7 )
                {
                    if (EntidadId == 4105)
                    {
                        obj.EstadoEvaluadorPCM = estado;
                        if(estado == EstadoExpediente.Aprobado)
                        {
                            obj.EstadoEvaluadorMEF = EstadoExpediente.Presentado;
                            obj.EstadoExpediente = EstadoExpediente.Presentado;
                        }

                    }
                    else if (EntidadId == 18497)
                    {
                        obj.EstadoEvaluadorMEF = estado;
                    }
                    else  
                    {
                        obj.EstadoEvaluadorMinisterio = estado;
                        if(estado == EstadoExpediente.Aprobado) { 
                        obj.EstadoExpediente = EstadoExpediente.Presentado;
                        obj.EstadoEvaluadorPCM = EstadoExpediente.Presentado;
                        }
                    }

                }
                else if (rol ==(short)Rol.Coordinador && EstadoMinistrio==1)
                {
                    
                        obj.EstadoEvaluadorMinisterio = estado;
                }

                if (estado == EstadoExpediente.Aprobado)
                {
                    Tupa vigente = _tupaRepository.GetOne(x => x.EntidadId == obj.EntidadId && x.EstadoTupa == EstadoTupa.Vigente);

                    Tupa oTupa = new Tupa()
                    {
                        EntidadId = obj.EntidadId,
                        ExpedienteId = obj.ExpedienteId,
                        FecPublicacion = DateTime.Now,
                        TipoTupa = obj.Entidad.TipoTupa,
                        EstadoTupa = EstadoTupa.Vigente
                    };

                    if (vigente != null)
                    {
                        vigente.EstadoTupa = EstadoTupa.NoVigente;
                        _tupaRepository.Save(vigente);
                    }

                    _tupaRepository.Insert(oTupa);

                    List<Procedimiento> procs = _procedimientoRepository
                                                .GetAllBy(x => x.ExpedienteId == obj.ExpedienteId
                                                    && x.Operacion != OperacionExpediente.Eliminacion);

                    foreach (Procedimiento p in procs)
                    {
                        oTupa.Procedimiento.Add(p);
                        _procedimientoRepository.SaveOnlyProcedimiento(p);
                    }
                }

                _expedienteRepository.SaveOnlyExpediente(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> ValidarCostos(long ExpedienteId,int procesogratuito)
        {
            try
            {
                List<string> lista = new List<string>();
                bool valid = true;

                var asmes = _tablaAsmeRepository.GetAllBy(x => x.Procedimiento.ExpedienteId == ExpedienteId
                                                            //&& x.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar
                                                            && x.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                                            && x.Procedimiento.Operacion != OperacionExpediente.Ninguna
                                                            && x.Prestaciones == 0
                                                            && x.Procedimiento.Estado!=3);

                //if (asmes.Count() != 0)
                //{

                if (asmes.Count() > 0) {

                    List<string> procs2 = asmes.GroupBy(x => x.Codigo).Select(x => x.Key).ToList();
                    lista.Add(string.Format("Debe asignar la cantidad de prestaciones de todos los procedimientos/servicios. {0}", string.Join(",", procs2.ToArray())));
                }

                var asmes2 = _tablaAsmeRepository.GetAllBy(x => x.Procedimiento.ExpedienteId == ExpedienteId
                                                    && x.EsGratuito == false
                                                    && x.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                                    && x.Procedimiento.Operacion != OperacionExpediente.Ninguna
                                                    && x.Procedimiento.Estado != 3);
  

                var conteogratis = asmes2.Count();

                //if (asmes2.Count() > 0) lista.Add("Debe asignar la cantidad de prestaciones de todos los procedimientos/servicios.");
       
                if (conteogratis != 0)
                {
                    valid = true;
                    var inductores = _inductorRepository.GetByExpediente(ExpedienteId);
                    var valores = _inductorValorRepository.GetAllBy(x => x.ExpedienteId == ExpedienteId);
                    foreach (var ind in inductores)
                    {
                        if (ind != null)
                        {
                            if (valores.Where(x => x.InductorId == ind.InductorId).Sum(x => x.Valor) == 0) valid = false;
                        }
                        else
                        {
                            valid = false;
                        }
                        
                    }
                    if (!valid) lista.Add("Debe asignar valores a todos los inductores utilizados para el proceso de cálculo de costos.");

                    valid = true;

                    String actividadessts = _actividadRepository
                                   .GetAllBy(x => x.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                               && x.TipoActividad != TipoActividad.Espera
                                               && x.Duracion == 0
                                               //&& (
                                               //    (x.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                                               //    && x.TablaAsme.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar)
                                               //||
                                               //    (x.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                                               //    && x.TablaAsme.Procedimiento.TipoProcedimiento == TipoProcedimiento.Estandar)
                                               //)
                                               ).ToString();

                    var actividades = _actividadRepository
                                        .GetAllBy(x => x.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                                    && x.TipoActividad != TipoActividad.Espera
                                                    && x.Duracion == 0 && x.TablaAsme.Procedimiento.Estado!= 3 && x.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                                    //&& (
                                                    //    (x.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                                                    //    && x.TablaAsme.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar)
                                                    //||
                                                    //    (x.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                                                    //    && x.TablaAsme.Procedimiento.TipoProcedimiento == TipoProcedimiento.Estandar)
                                                    //)
                                                    );
                    if (actividades.Count() > 0)
                    {
                        List<string> procs = actividades.GroupBy(x => x.TablaAsme.Procedimiento.CodigoCorto).Select(x => x.Key).ToList();
                        lista.Add(string.Format("Los siguientes procedimientos tienen actividades con duración igual a cero : {0}", string.Join(",", procs.ToArray())));
                    }


                //var listTA = _tablaAsmeRepository
                //                  .GetAllBy(x => x.ProcedimientoId == ProcedimientoId
                //                  && x.Actividad.Count() == 0
                //                  && (
                //                      (x.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                //                      && x.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar)
                //                  ||
                //                      (x.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                //                      && x.Procedimiento.TipoProcedimiento == TipoProcedimiento.Estandar)
                //                  ));

               
                    valid = true;

                    List<Procedimiento> listProc = new List<Procedimiento>();
                    if (procesogratuito == 1)
                    {
                        listProc = _procedimientoRepository
                                          .GetAllBy(x => x.ExpedienteId == ExpedienteId
                                          && x.TablaAsme.Count(t => t.Actividad.Count() == 0 && t.EsGratuito==false) > 0
                                          && x.Operacion != OperacionExpediente.Eliminacion
                                          && x.Operacion != OperacionExpediente.Ninguna && x.Estado != 3 
                                            //&& (
                                            //    (x.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                                            //    && x.TipoProcedimiento != TipoProcedimiento.Estandar)
                                            //||
                                            //    (x.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                                            //    && x.TipoProcedimiento == TipoProcedimiento.Estandar)
                                            //)
                                            );
                    }
                    else
                    {
                          listProc = _procedimientoRepository
                                          .GetAllBy(x => x.ExpedienteId == ExpedienteId
                                          && x.TablaAsme.Count(t => t.Actividad.Count() == 0) > 0
                                          && x.Operacion != OperacionExpediente.Eliminacion
                                          && x.Operacion != OperacionExpediente.Ninguna && x.Estado != 3 
                                          //&& (
                                          //    (x.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                                          //    && x.TipoProcedimiento != TipoProcedimiento.Estandar)
                                          //||
                                          //    (x.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                                          //    && x.TipoProcedimiento == TipoProcedimiento.Estandar)
                                          //)
                                          );
                    }
                    if (listProc.Count() > 0)
                    {
                        lista.Add(string.Format("Los siguientes procedimientos no tienen tablas ASME-VM elaboradas : {0}", string.Join(",", listProc.Select(x => x.CodigoCorto).ToArray())));
                    }
                }

             


                //}
                //else
                //{
                //    lista.Add("Debe Registrar los costos para poder procesar ");
                //}

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> ValidarSubvencion(long ExpedienteId, long Sector)
        {
            try
            {
                List<string> lista = new List<string>();

                int EstadoSector =_auditoriaRepository.ExcluirEntidadMEF(Sector);

                if (EstadoSector != 0) 
                {
                    var tablasMef = _tablaAsmeRepository.GetAllBy(x => x.Procedimiento.ExpedienteId == ExpedienteId
                                                                    && x.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                                                    && !x.EsGratuito
                                                                    && x.Procedimiento.Estado != 3
                                                                    && (x.AutorizacionMEF != Autorizacionmef.Autorizar && x.DerechoTramitacion > x.UIT.Monto));

                    if (tablasMef.Count() > 0)
                    {
                        lista.Add("No cuentan con la autorizacion de la DIRECCIÓN GENERAL DE POLÍTICA DE INGRESOS PÚBLICOS");
                    }
                } 

                var tablas = _tablaAsmeRepository.GetAllBy(x => x.Procedimiento.ExpedienteId == ExpedienteId
                                                               && x.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                                               && !x.EsGratuito
                                                               && x.DerechoTramitacion > x.CostoUnitario && x.Procedimiento.Estado != 3);
                 

                if (tablas.Count() > 0)
                { 
                    lista.Add("Existen procedimientos con Derecho de Tramitación mayor a su costo unitario.");
                } 
                else 
                {
                    var estado = _expedienteRepository.GetAllBy(x => x.ExpedienteId == ExpedienteId
                                                                    && x.ProcesarCosto == 1);
                    if (estado.Count() == 0)
                    {
                        lista.Add("Favor de Procesar Costos.");
                    }
                 
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
