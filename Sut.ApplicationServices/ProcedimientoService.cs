using Sut.IApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sut.Entities;
using Sut.IRepositories;
using Sut.Log; 
using Sut.Security; 

namespace Sut.ApplicationServices
{
    public class ProcedimientoService : IProcedimientoService
    {
        private readonly ILogService<ProcedimientoService> _log;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProcedimientoRepository _procedimientoRepository;
        private readonly IProcedimientoSedeRepository _procedimientoSedeRepository;
        private readonly IActividadRepository _actividadRepository;
        private readonly ITablaAsmeRepository _tablaAsmeRepository;
        private readonly IDatoService _datoService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMaestroFijoService _maestroFijoService;
        private readonly IFactorDedicacionService _factorDedicacionService;

        public ProcedimientoService (IUnitOfWork unitOfWork,
                                        IProcedimientoRepository procedimientoRepository,
                                        IProcedimientoSedeRepository procedimientoSedeRepository,
                                        IActividadRepository actividadRepository,
                                        ITablaAsmeRepository tablaAsmeRepository,
                                        IMaestroFijoService maestroFijoService,
                                        IDatoService datoService,
                                        IFactorDedicacionService factorDedicacionService,
                                        IUsuarioRepository usuarioRepository)
        {
            _log = new LogService<ProcedimientoService>();
            _unitOfWork = unitOfWork;
            _procedimientoRepository = procedimientoRepository;
            _procedimientoSedeRepository = procedimientoSedeRepository;
            _actividadRepository = actividadRepository;
            _maestroFijoService = maestroFijoService;
            _datoService = datoService;
            _tablaAsmeRepository = tablaAsmeRepository;
            _usuarioRepository = usuarioRepository;
            _factorDedicacionService = factorDedicacionService;
        }

        public List<Procedimiento> GetByExpediente(long ExpedienteId)
        {
            try
            {
                return _procedimientoRepository.GetByExpediente(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> GetByExpedienteNumero(long ExpedienteId)
        {
            try
            {
                return _procedimientoRepository.GetByExpedienteNumero(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> GetByExpedienteNumeroEli(long ExpedienteId)
        {
            try
            {
                return _procedimientoRepository.GetByExpedienteNumeroEli(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Procedimiento> GetByExpedienteNumerotipoorder(long ExpedienteId, int tipo)
        {
            try
            { 

                if (tipo == 1)
                {
                    return _procedimientoRepository.GetByExpediente(ExpedienteId).OrderByDescending(x => x.Numero).ToList();
                } else { 
                    return _procedimientoRepository.GetByExpediente(ExpedienteId).OrderBy(x => x.Numero).ToList();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Procedimiento> GetByExpedienteNumerotipoordersinEliminar(long ExpedienteId, int tipo)
        {
            try
            {

                if (tipo == 1)
                {
                    return _procedimientoRepository.GetByExpediente(ExpedienteId).OrderByDescending(x => x.Numero).ToList();
                }
                else
                {
                    return _procedimientoRepository.GetByExpedientesinEliminar(ExpedienteId).OrderBy(x => x.Numero).ToList();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Procedimiento GetOne(long ProcedimientoId)
        {
            try
            {
                return _procedimientoRepository.GetOne(ProcedimientoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        
        public Procedimiento GetOneCount(string codigo, long ExpedienteId)
        {
            try
            {
                return _procedimientoRepository.GetOneCount( codigo, ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Procedimiento> codigocorto(string codigocorto)
        {
            try
            {
                return _procedimientoRepository.codigocorto(codigocorto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Procedimiento> codvalor(string codvalor)
        {
            try
            {
                return _procedimientoRepository.codvalor(codvalor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Denominacion(long ProcedimientoId)
        {
            try
            {
                return _procedimientoRepository.Denominacion(ProcedimientoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> GetAllLikePagin_(Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {

                List<Procedimiento> query = new List<Procedimiento>();


                //if (filtro.Ascendente == 1)
                //{

                //    if (filtro.CodigoCorto != null || filtro.UndOrgResponsable.Nombre != null)
                //    {
                //        query = _procedimientoRepository.GetByExpediente(filtro.ExpedienteId).OrderBy(x => x.Numero).Where(x => x.CodigoCorto != null).ToList();

                //    }
                //    else
                //    {
                //        query = _procedimientoRepository.GetByExpediente(filtro.ExpedienteId).OrderByDescending(x => x.Numero).ToList();
                //    }
                //}
                //else if (filtro.CodigoCorto != null || filtro.UndOrgResponsable != null) {
                //    query = _procedimientoRepository.GetByExpediente(filtro.ExpedienteId).OrderBy(x => x.Numero).Where(x=>x.CodigoCorto != null).ToList();

                //}
                //else
                //{
                //    query = _procedimientoRepository.GetByExpediente(filtro.ExpedienteId).OrderBy(x => x.Numero).ToList();
                //}
             
               // ordernar PA

                if (filtro.Expediente.OrdenPa == TipoOrdenPa.Sistema) { 

                    if (filtro.Ascendente == 1)
                    {
                        query = _procedimientoRepository.GetByExpediente(filtro.ExpedienteId).OrderByDescending(x => x.Numero).ToList();

                    }
                    else
                    {
                        query = _procedimientoRepository.GetByExpediente(filtro.ExpedienteId).OrderBy(x => x.Numero).ToList();
                    }
                }
                else if (filtro.Expediente.OrdenPa == TipoOrdenPa.UnidadOrganica)
                { 
                        query = _procedimientoRepository.GetByExpediente(filtro.ExpedienteId).OrderBy(x => x.UndOrgResponsable.Nombre).ToList();
 
                }
                else if (filtro.Expediente.OrdenPa == TipoOrdenPa.Bloque)
                {
                    query = _procedimientoRepository.GetByExpediente(filtro.ExpedienteId).OrderBy(x => x.TipoProcedimiento).OrderBy(x => x.Bloque).ToList();

                }
                else if (filtro.Expediente.OrdenPa == TipoOrdenPa.UnidadOrganicaBloque)
                {
                    query = _procedimientoRepository.GetByExpediente(filtro.ExpedienteId).OrderBy(x => x.UndOrgResponsable.Nombre).OrderBy(x => x.Bloque).ToList();

                }











                var data = query
                          .Where(x =>  x.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Denominacion) ? x.Denominacion : filtro.Denominacion).ToUpper()));

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
                    if (filtro.UndOrgResponsable.Nombre != null) {

                    data = data.Where(x => x.UndOrgResponsable.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.UndOrgResponsable.Nombre) ? x.UndOrgResponsable.Nombre : filtro.UndOrgResponsable.Nombre).ToUpper()));
                    }
                }

                if (filtro.FiltroTipoProcedimiento > 0)
                {
                    if (filtro.FiltroTipoProcedimiento == 4)
                    {
                        data = data.Where(x => x.CodigoACR != "0");
                    }
                    else if (filtro.FiltroTipoProcedimiento == 6)
                    {
                        data = data.Where(x => x.CodigoACR == "0");
                    }
                    else
                    {
                        data = data.Where(x => (short)x.TipoProcedimiento == filtro.FiltroTipoProcedimiento);
                    }
                    
                }
                if (filtro.CodigoACR != null) /*JJJMSP2*/
                {
                    data = data.Where(x => x.CodigoACR == filtro.CodigoACR).OrderByDescending(x => x.ProcedimientoId);
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
        /*Modificado por ESEO*/
        public List<Procedimiento> GetAllLikePagin(Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                var query = _procedimientoRepository.GetByExpediente(filtro.ExpedienteId).AsQueryable();

                if (filtro.Expediente.OrdenPa == TipoOrdenPa.Sistema)
                {
                    query = filtro.Ascendente == 1 ? query.OrderByDescending(x => x.Numero) : query.OrderBy(x => x.Numero);
                }
                else if (filtro.Expediente.OrdenPa == TipoOrdenPa.UnidadOrganica)
                {
                    query = query.OrderBy(x => x.UndOrgResponsable.Nombre);
                }
                else if (filtro.Expediente.OrdenPa == TipoOrdenPa.Bloque)
                {
                    query = query.OrderBy(x => x.TipoProcedimiento).ThenBy(x => x.Bloque);
                }
                else if (filtro.Expediente.OrdenPa == TipoOrdenPa.UnidadOrganicaBloque)
                {
                    query = query.OrderBy(x => x.UndOrgResponsable.Nombre).ThenBy(x => x.Bloque);
                }

                query = query.Where(x => x.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Denominacion) ? x.Denominacion : filtro.Denominacion).ToUpper()));

                if (filtro.CodigoCorto != null)
                {
                    query = query.Where(x => x.CodigoCorto != null && x.CodigoCorto.ToUpper().Contains((string.IsNullOrEmpty(filtro.CodigoCorto) ? x.CodigoCorto : filtro.CodigoCorto).ToUpper()));
                }

                if (filtro.FiltroOperacion > 0)
                {
                    query = query.Where(x => (short)x.Operacion == filtro.FiltroOperacion);
                }
                else
                {
                    query = query.Where(x => (short)x.Operacion != 3);
                }

                if (filtro.UndOrgResponsable?.Nombre != null)
                {
                    query = query.Where(x => x.UndOrgResponsable.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.UndOrgResponsable.Nombre) ? x.UndOrgResponsable.Nombre : filtro.UndOrgResponsable.Nombre).ToUpper()));
                }

                if (filtro.FiltroTipoProcedimiento > 0)
                {
                    if (filtro.FiltroTipoProcedimiento == 4)
                    {
                        query = query.Where(x => x.CodigoACR != "0");
                    }
                    else if (filtro.FiltroTipoProcedimiento == 6)
                    {
                        query = query.Where(x => x.CodigoACR == "0");
                    }
                    else
                    {
                        query = query.Where(x => (short)x.TipoProcedimiento == filtro.FiltroTipoProcedimiento);
                    }
                }
                if (filtro.CodigoACR != null) /*JJJMSP2*/
                {
                    query = query.Where(x => x.CodigoACR == filtro.CodigoACR).OrderByDescending(x => x.ProcedimientoId);
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
        public List<Procedimiento> GetAllLikePaginACR(Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {

                List<Procedimiento> query = new List<Procedimiento>();

                query = _procedimientoRepository.GetByExpedienteACR(filtro.ExpedienteId).OrderBy(x => x.Numero).ToList();

              

                var data = query
                            .Where(x => x.CodigoCorto.ToUpper().Contains((string.IsNullOrEmpty(filtro.CodigoCorto) ? x.CodigoCorto : filtro.CodigoCorto).ToUpper())
                                    && x.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Denominacion) ? x.Denominacion : filtro.Denominacion).ToUpper()));
                


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

        public List<Procedimiento> GetAllLikePaginObservado(Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {

                List<Procedimiento> query = new List<Procedimiento>();


                //if (filtro.Ascendente == 1)
                //{
                //    query = _procedimientoRepository.GetByExpediente(filtro.ExpedienteId).OrderByDescending(x => x.Numero).Where(x => x.Operacion != OperacionExpediente.Eliminacion && x.Estado!=3).ToList();
                //}
                //else
                //{
                //    query = _procedimientoRepository.GetByExpediente(filtro.ExpedienteId).OrderBy(x => x.Numero).Where(x => x.Operacion != OperacionExpediente.Eliminacion && x.Estado != 3).ToList();
                //} 
                //var data = query
                //            .Where(x => x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper())
                //                    && x.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Denominacion) ? x.Denominacion : filtro.Denominacion).ToUpper()));


                //if (filtro.FiltroOperacion > 0) data = data.Where(x => (short)x.Operacion == filtro.FiltroOperacion);
                ////if (filtro.UndOrgResponsable.Nombre != null) data = data.Where(x => x.UndOrgResponsable.Nombre == filtro.UndOrgResponsable.Nombre);
                //if (filtro.FiltroTipoProcedimiento > 0)
                //{
                //    if (filtro.FiltroTipoProcedimiento == 4)
                //    {
                //        data = data.Where(x => x.CodigoACR != "0");
                //    }
                //    else if (filtro.FiltroTipoProcedimiento == 5)
                //    {
                //        data = data.Where(x => x.CodigoACR == "0");
                //    }
                //    else
                //    {
                //        data = data.Where(x => (short)x.TipoProcedimiento == filtro.FiltroTipoProcedimiento);
                //    }

                //}

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

                if (filtro.Codigo != null)  /*JJJMSP2*/
                {
                    data = data.Where(x => x.CodigoCorto != null);
                    data = data.Where(x => x.CodigoCorto.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.CodigoCorto : filtro.Codigo).ToUpper()));

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
                if (filtro.CodigoACR != null) /*JJJMSP2*/
                {
                    data = data.Where(x => x.CodigoACR == filtro.CodigoACR).OrderByDescending(x => x.ProcedimientoId);
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


        public List<TablaAsme> GetAllLikePaginFiltroAsme(TablaAsme filtro, int pageIndex, int pageSize,long tablaAsmeId, ref int totalRows)
        {
            try
            {
                List<TablaAsme> query = _procedimientoRepository.GetByExpedienteFiltroAsme(filtro.Procedimiento.ExpedienteId, tablaAsmeId);

 
                var data = query
                             .Where(x => x.Procedimiento.CodigoCorto.ToUpper().Contains((string.IsNullOrEmpty(filtro.Procedimiento.CodigoCorto) ? x.Procedimiento.CodigoCorto : filtro.Procedimiento.CodigoCorto).ToUpper())
                                    && x.Procedimiento.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Procedimiento.Denominacion) ? x.Procedimiento.Denominacion : filtro.Procedimiento.Denominacion).ToUpper())
                                     //&& x.Descripcion.Contains((string.IsNullOrEmpty(filtro.Descripcion) ? x.Descripcion : filtro.Descripcion))
                                     )
              .OrderBy(x => x.ProcedimientoId);

                //if (filtro.Descripcion != null)
                //{

                //    data = query
                //             .Where(x => x.Descripcion.Contains((string.IsNullOrEmpty(filtro.Descripcion) ? x.Descripcion : filtro.Descripcion))
                //             && x.Descripcion!= null)
                              
                //        .OrderBy(x => x.ProcedimientoId);
                //}
                 


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

        public List<Procedimiento> GetAllLikePaginFiltro(Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows, Filtros fill)
        {
            try
            {
                List<Procedimiento> query = _procedimientoRepository.GetByExpedienteFiltro(filtro.ExpedienteId, fill);


                //var data = query
                //            .Where(x => x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper())
                //                    && x.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Denominacion) ? x.Denominacion : filtro.Denominacion).ToUpper()))
                //            .OrderByDescending(x => x.ProcedimientoId);

                //if (filtro.FiltroOperacion > 0) data = data.Where(x => (short)x.Operacion == filtro.FiltroOperacion).OrderByDescending(x => x.ProcedimientoId);
                //if (filtro.FiltroTipoProcedimiento > 0) data = data.Where(x => (short)x.TipoProcedimiento == filtro.FiltroTipoProcedimiento).OrderByDescending(x => x.ProcedimientoId);

                var data = query
              .OrderBy(x => x.ProcedimientoId);

                if (fill.Clasificacion != CalificacionProcedimiento.Ninguno)
                {
                    data = query.Where(x => x.Calificacion == fill.Clasificacion).OrderByDescending(x => x.ProcedimientoId); 

                }
                if (fill.Prestacionesanuales == FiltrosOrdenar.Ascendente)
                {
                    data = data.OrderByDescending(x => x.Pprestacionesanuales); 
                }else if (fill.Prestacionesanuales == FiltrosOrdenar.Descendente)
                {
                    data = data.OrderBy(x => x.Pprestacionesanuales);
                }

                if (fill.PrestacionesCosto == FiltrosOrdenar.Ascendente)
                {
                    data = data.OrderByDescending(x => x.Pprestacioncosto);

                }
                else if (fill.PrestacionesCosto == FiltrosOrdenar.Descendente)
                {
                    data = data.OrderBy(x => x.Pprestacioncosto);
                }

                if (fill.Duracion == FiltrosOrdenar.Ascendente)
                {
                    data = data.OrderByDescending(x => x.Pduracion);

                }
                else if (fill.Duracion == FiltrosOrdenar.Descendente)
                {
                    data = data.OrderBy(x => x.Pduracion);
                }



                if (fill.PlazoAtencion == FiltrosOrdenar.Ascendente)
                {
                    data = data.OrderByDescending(x => x.PlazoAtencion);

                }
                else if (fill.PlazoAtencion == FiltrosOrdenar.Descendente)
                {
                    data = data.OrderBy(x => x.PlazoAtencion);
                }



                if (fill.Requisitos == FiltrosOrdenar.Ascendente)
                {
                    data = data.OrderByDescending(x => x.Prequisitos);

                }
                else if (fill.Requisitos == FiltrosOrdenar.Descendente)
                {
                    data = data.OrderBy(x => x.Prequisitos);
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


        public void GuardarNuevoCodigo(Procedimiento obj)
        {
            try
            {
                Procedimiento proc = _procedimientoRepository.GetOne(x => x.ProcedimientoId == obj.ProcedimientoId);
                proc.Codigo = obj.Codigo;
                proc.CategoriaProcedimientoId = obj.CategoriaProcedimientoId;
                proc.TipoProcedimientoId = obj.TipoProcedimientoId;
                proc.UndOrgResponsableId = obj.UndOrgResponsableId;
                proc.CodigoCorto = obj.CodigoCorto;

                proc.UndOrgResponsableId2 = obj.UndOrgResponsableId2;
                proc.UndOrgResponsableId3 = obj.UndOrgResponsableId3;
                proc.UndOrgResponsableId4 = obj.UndOrgResponsableId4;
                proc.UndOrgResponsableId5 = obj.UndOrgResponsableId5;



                //int codig= _procedimientoService.GetByExpedienteNumero(model.ExpedienteId).Max(x=>x.Numero);
                //model.Numero = codig + 1;
                int numcont = _procedimientoRepository.GetByExpedienteNumero(proc.ExpedienteId).Count();
                if (numcont == 0)
                { 
                    proc.Numero = 1;
                }
                else
                {
                    proc.Numero = _procedimientoRepository.GetByExpedienteNumero(proc.ExpedienteId).Max(x => x.Numero) + 1;
                }
                _procedimientoRepository.SaveOnlyProcedimiento(proc);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void GuardarPosicion(Procedimiento obj)
        {
            try
            {
                Procedimiento proc = _procedimientoRepository.GetOne(x => x.ProcedimientoId == obj.ProcedimientoId);
                proc.Posicion = obj.Posicion;
               
           
                _procedimientoRepository.SaveOnlyProcedimiento(proc);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Procedimiento obj)
        {
            try
            {
                _procedimientoRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Savemodalidad(Procedimiento obj)
        {
            try
            {
                _procedimientoRepository.Savemodalidad(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SaveCopia(Procedimiento obj)
        {
            try
            {
                _procedimientoRepository.Save(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveInformacionCiudadano(Procedimiento obj)
        {
            try
            {
                _procedimientoRepository.SaveInformacionCiudadano(obj);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveTablaAsme(Procedimiento obj)
        {
            try
            {
                _procedimientoRepository.SaveTablaAsme(obj);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveInformacionBasica(Procedimiento obj)
        {
            try
            {
                _procedimientoRepository.SaveInformacionBasica(obj);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> CopiarDatosProcedimiento(long ProcedimientoOrigenId, long ProcedimientoDestinoId, List<DatoCopia> lstCopia, string CodigoCopia, string CodigoCortoCopia)
        {
            List<string> mensajes = new List<string>();
            try
            {
                mensajes = _procedimientoRepository.CopiarDatosProcedimiento(ProcedimientoOrigenId, ProcedimientoDestinoId, lstCopia, CodigoCopia,CodigoCortoCopia);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                Eliminar(ProcedimientoDestinoId);
                mensajes.Add(ex.Message);
                _log.Error(ex);
            }
            return mensajes;
        }

        public List<string> CopiarDatosProcedimientoEli(long ProcedimientoOrigenId, long ProcedimientoDestinoId, List<DatoCopia> lstCopia, string CodigoCopia, string CodigoCortoCopia)
        {
            List<string> mensajes = new List<string>();
            try
            {
                mensajes = _procedimientoRepository.CopiarDatosProcedimientoEli(ProcedimientoOrigenId, ProcedimientoDestinoId, lstCopia, CodigoCopia, CodigoCortoCopia);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                Eliminar(ProcedimientoDestinoId);
                mensajes.Add(ex.Message);
                _log.Error(ex);
            }
            return mensajes;
        }

        public List<string> CopiarDatosProcedimientoTablaASME(long ProcedimientoOrigenId, long ProcedimientoDestinoId, long TablaAsmeIdSeleccionado, long TablaAsmeIdCopiar)
        {
            List<string> mensajes = new List<string>();
            try
            {
                mensajes = _procedimientoRepository.CopiarDatosProcedimientoTablaASME(ProcedimientoOrigenId, ProcedimientoDestinoId, TablaAsmeIdSeleccionado, TablaAsmeIdCopiar);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                Eliminar(ProcedimientoDestinoId);
                mensajes.Add(ex.Message);
                _log.Error(ex);
            }
            return mensajes;
        }

        public void GuardarSustEliminacion(Procedimiento obj)
        {
            try
            {
                Procedimiento proc = _procedimientoRepository.GetOne(x => x.ProcedimientoId == obj.ProcedimientoId);
                proc.SustentoEliminacion = obj.SustentoEliminacion;
                proc.SustTecCalificacion = obj.SustTecCalificacion;

                _procedimientoRepository.SaveOnlyProcedimiento(proc);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminacionprocedimiento(Procedimiento obj)
        {
            try
            {
                Procedimiento proc = _procedimientoRepository.GetOne(x => x.ProcedimientoId == obj.ProcedimientoId);
        
                _procedimientoRepository.SaveOnlyProcedimiento(proc);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void activarCondicion(Procedimiento obj)
        {
            try
            {
                 

                Procedimiento proc = _procedimientoRepository.GetOne(x => x.ProcedimientoId == obj.ProcedimientoId);

                if(obj.TipoFicha == 1)
                {
                    proc.EstadoInfoEva = obj.condicion;
                    proc.EstadoInfo = obj.condicion; 
                    proc.InfCdnoTerminado = false;

                } else if (obj.TipoFicha == 2)
                {
                    proc.EstadodatosEva = obj.condicion;
                }
                else if (obj.TipoFicha == 3)
                {
                    proc.EstadostlEva = obj.condicion;
                }
                else if (obj.TipoFicha == 4)
                {
                    proc.EstadotablaasmeEva = obj.condicion;
                }


                _procedimientoRepository.SaveOnlyProcedimiento(proc);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminacionorg(Procedimiento obj)
        {
            try
            {
                _procedimientoRepository.SaveOnlyProcedimiento(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Updateprocedimiento(Procedimiento obj)
        {
            try
            { 
                _procedimientoRepository.SaveOnlyProcedimiento(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void guardarnumeroprocedimiento(Procedimiento obj)
        {
            try
            {
                Procedimiento proc = _procedimientoRepository.GetOne(x => x.ProcedimientoId == obj.ProcedimientoId);
                proc.Numero = obj.Numero;
                if (proc.TipoProcedimiento == TipoProcedimiento.Estandar)
                {
                    proc.TipoProcedimiento = proc.TipoProcedimiento;
                } 

                _procedimientoRepository.SaveOnlyProcedimiento(proc);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void modificarprocedimiento(Procedimiento obj)
        {
            try
            {
                Procedimiento proc = _procedimientoRepository.GetOne(x => x.ProcedimientoId == obj.ProcedimientoId);
                proc.Denominacion = obj.Denominacion;
                proc.DenominacionAnterior = obj.Denominacion;

                if (proc.TipoProcedimiento == TipoProcedimiento.Estandar)
                {
                    proc.TipoProcedimiento = proc.TipoProcedimiento;
                }
                else {
                    proc.TipoProcedimiento = obj.TipoProcedimiento;
                }
                proc.CategoriaProcedimientoId = obj.CategoriaProcedimientoId;
                proc.TipoProcedimientoId = obj.TipoProcedimientoId;
                proc.UndOrgResponsableId = obj.UndOrgResponsableId;
                proc.UndOrgResponsableId2 = obj.UndOrgResponsableId2;
                proc.UndOrgResponsableId3 = obj.UndOrgResponsableId3;
                proc.UndOrgResponsableId4 = obj.UndOrgResponsableId4;
                proc.UndOrgResponsableId5 = obj.UndOrgResponsableId5;
                proc.ProcedimientoUndOrgResponsable = obj.ProcedimientoUndOrgResponsable;
                //proc.Numero = obj.Numero;




                _procedimientoRepository.SaveOnlyProcedimiento(proc);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         

        public void quitarduplocado()
        {
            try
            {
                List<Procedimiento> lstproce = _procedimientoRepository.duplicadocodigo();
                string codigoevaluar = "";
                foreach (Procedimiento l in lstproce)
                {
                    Procedimiento obj = _procedimientoRepository.GetOne(x => x.ProcedimientoId == l.ProcedimientoId);

                    if (obj.CodigoCorto == codigoevaluar)
                    { 
                        for (var cod2 = 0; cod2 < 1; cod2++)
                        {
                            var codigovalor = obj.CodigoCorto.ToString().Substring(1, 6) + Guid.NewGuid().ToString().Substring(1, 4).ToUpper();

                            var codigoBuscar = _procedimientoRepository.codigocorto(codigovalor);

                            if (codigoBuscar.Count() == 0)
                            {
                                obj.CodigoCorto = codigovalor;

                                _procedimientoRepository.SaveOnlyProcedimiento(obj);
                                _unitOfWork.SaveChanges();
                            }
                            else
                            {
                                cod2 = 0;
                            }
                        }

                       
                    }
                    codigoevaluar = obj.CodigoCorto;
                }



                List<Procedimiento> lstproceclargo = _procedimientoRepository.duplicadocodigolargo();
                string codigoevaluarlargo = "";
                foreach (Procedimiento l in lstproceclargo)
                {
                    Procedimiento obj2 = _procedimientoRepository.GetOne(x => x.ProcedimientoId == l.ProcedimientoId);

                    if (obj2.Codigo == codigoevaluarlargo)
                    {
                        for (var cod3 = 0; cod3 < 1; cod3++)
                        {
                            var codigovalor = obj2.Codigo.ToString() + Guid.NewGuid().ToString().Substring(1, 4).ToUpper();

                            var codigoBuscar = _procedimientoRepository.codvalor(codigovalor);

                            if (codigoBuscar.Count() == 0)
                            {
                                obj2.Codigo = codigovalor;

                                _procedimientoRepository.SaveOnlyProcedimiento(obj2);
                                _unitOfWork.SaveChanges();
                            }
                            else
                            {
                                cod3 = 0;
                            }
                        }


                    }
                    codigoevaluarlargo = obj2.Codigo;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CambiarEstadoSustento(TipoSustento tipo, long ProcedimientoId, bool estado)
        {
            try
            {
                //Procedimiento obj = _procedimientoRepository.GetOne(x => x.ProcedimientoId == ProcedimientoId);
                Procedimiento obj = _procedimientoRepository.GetOne(ProcedimientoId);

                if (tipo != TipoSustento.InfCdno)
                {
                    if (estado)
                    {
                        if (tipo != TipoSustento.InfCdno)
                            if (!obj.InfCdnoTerminado) throw new Exception("Debe terminar el registro de información al ciudadano.");

                        if (tipo != TipoSustento.DatosGenerales)
                            if (!obj.DatosGeneralesTerminado) throw new Exception("Debe terminar el registro de datos generales.");

                            else if (tipo != TipoSustento.SustentoTecnico)
                                if (!obj.SustTecnicoTerminado) throw new Exception("Debe terminar el registro de sustento técnico y legal.");
                    }
                }

                switch (tipo)
                {
                    case TipoSustento.DatosGenerales: obj.DatosGeneralesTerminado = estado; break;
                    case TipoSustento.SustentoTecnico: obj.SustTecnicoTerminado = estado; break;
                    case TipoSustento.SustentoLegal: obj.SustLegalTerminado = estado; break;
                    case TipoSustento.InfCdno: obj.InfCdnoTerminado = estado; break;
                    case TipoSustento.TablaAsme: obj.TablaAsmeTerminado = estado; break;
                }

                //if(TipoSustento.DatosGenerales== tipo && estado == false) 
                //{
                //    obj.EstadodatosEva = 4;
                //}
                //if (TipoSustento.SustentoTecnico == tipo && estado == false)
                //{
                //    obj.EstadostlEva = 4;
                //} 
                //if (TipoSustento.InfCdno == tipo && estado == false)
                //{
                //    obj.EstadoInfoEva = 4;
                //}
                //if (TipoSustento.TablaAsme == tipo && estado == false)
                //{
                //    obj.EstadotablaasmeEva = 4;
                //}
                var respuesta = 0;
                if (obj.Expediente.EstadoExpediente == EstadoExpediente.EnProceso) {
                    if (estado == false)
                    {
                        respuesta = 1;
                    }
                    else
                    {
                        respuesta = 2;
                    }
                    switch (tipo)
                    { 
                        case TipoSustento.DatosGenerales: obj.Estadodatos = respuesta; break;
                        case TipoSustento.SustentoTecnico: obj.Estadostl = respuesta; break;
                        case TipoSustento.SustentoLegal: obj.Estadostl = respuesta; break;
                        case TipoSustento.InfCdno: obj.EstadoInfo = respuesta; break;
                        case TipoSustento.TablaAsme: obj.Estadotablaasme = respuesta; break;
                    }
                    //if (estado == true) {
                    //    if (tipo == TipoSustento.DatosGenerales)
                    //    {
                    //        obj.Estadostl = 1; 
                    //        obj.Estadotablaasme = 1;  
                    //    }else if (tipo == TipoSustento.SustentoTecnico)
                    //    { 
                    //        obj.Estadotablaasme = 1;
                    //    }
                    //} 
                }

                //if (obj.Expediente.EstadoExpediente == EstadoExpediente.Observado)
                //{

                //    if (TipoSustento.DatosGenerales == tipo) 
                //    { 
                //        if (obj.EstadodatosEva == 4)
                //        {

                //            if (estado == false)
                //            {
                //                respuesta = 4;
                //            }
                //            else
                //            {
                //                respuesta = 5;
                //            }
                //        }
                //        else
                //        {
                //            if (estado == false)
                //            {
                //                respuesta = obj.EstadodatosEva;
                //            }
                //            else
                //            {
                //                respuesta = 2;
                //            }
                //        }
                //    }
                //    if (TipoSustento.SustentoTecnico == tipo)
                //    {
                //        if (obj.EstadostlEva == 4)
                //        {

                //            if (estado == false)
                //            {
                //                respuesta = 4;
                //            }
                //            else
                //            {
                //                respuesta = 5;
                //            }
                //        }
                //        else
                //        {
                //            if (estado == false)
                //            {
                //                respuesta = obj.EstadostlEva;
                //            }
                //            else
                //            {
                //                respuesta = 2;
                //            }
                //        }
                //    }
                    
                //    if (TipoSustento.InfCdno == tipo)
                //    {
                //        if (obj.EstadoInfoEva == 4) { 

                //            if (estado == false)
                //            {
                //                respuesta = 4;
                //            }
                //            else
                //            {
                //                respuesta = 5;
                //            }
                //        }
                //        else
                //        {
                //            if (estado == false)
                //            {
                //                respuesta = obj.EstadoInfoEva;
                //            }
                //            else
                //            {
                //                respuesta = 2;
                //            }
                //        }
                //    }
                //    if (TipoSustento.TablaAsme == tipo)
                //    {
                //        if (obj.EstadotablaasmeEva == 4)
                //        {

                //            if (estado == false)
                //            {
                //                respuesta = 4;
                //            }
                //            else
                //            {
                //                respuesta = 5;
                //            }
                //        }
                //        else
                //        {
                //            if (estado == false)
                //            {
                //                respuesta = obj.EstadotablaasmeEva;
                //            }
                //            else
                //            {
                //                respuesta = 2;
                //            }
                //        }

                //    } 

                //    switch (tipo)
                //    {
                //        case TipoSustento.DatosGenerales: obj.Estadodatos = respuesta; break;
                //        case TipoSustento.SustentoTecnico: obj.Estadostl = respuesta; break;
                //        case TipoSustento.SustentoLegal: obj.Estadostl = respuesta; break;
                //        case TipoSustento.InfCdno: obj.EstadoInfo = respuesta; break;
                //        case TipoSustento.TablaAsme: obj.Estadotablaasme = respuesta; break;
                //    }
                //    //switch (tipo)
                //    //{
                //    //    case TipoSustento.DatosGenerales: obj.EstadodatosEva = 0; break;
                //    //    case TipoSustento.SustentoTecnico: obj.EstadostlEva = 0; break;
                //    //    case TipoSustento.SustentoLegal: obj.EstadostlEva = 0; break;
                //    //    case TipoSustento.InfCdno: obj.EstadoInfoEva = 0; break;
                //    //    case TipoSustento.TablaAsme: obj.EstadotablaasmeEva = 0; break;
                //    //}
                //    //if (estado == true)
                //    //{
                //    //    if (tipo == TipoSustento.DatosGenerales)
                //    //    {
                //    //        obj.Estadostl = 1;
                //    //        obj.Estadotablaasme = 1;
                //    //    }
                //    //    else if (tipo == TipoSustento.SustentoTecnico)
                //    //    {
                //    //        obj.Estadotablaasme = 1;
                //    //    }
                //    //}
                //}
                //if (tipo!= TipoSustento.InfCdno)
                //{
                //    _factorDedicacionService.Eliminar(obj.ExpedienteId);
                //    obj.Expediente.SustCostosTerminado = false;
                //    obj.Expediente.ProcesarCosto = 0;
                //}
 


                _procedimientoRepository.SaveOnlyProcedimiento(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CambiarOperacion(OperacionExpediente operacion, long ProcedimientoId)
        {
            try
            {
                Procedimiento obj = _procedimientoRepository.GetOne(x => x.ProcedimientoId == ProcedimientoId);

                if (obj.Operacion == OperacionExpediente.Ninguna)
                {
                    obj.EstadoNinguno = 1;
                }
                else
                {
                    obj.EstadoNinguno = 0;
                }

                obj.Operacion = operacion;

             



                switch (operacion)
                {
                    case OperacionExpediente.Modificacion:
                        //obj.DatosGeneralesTerminado = false;
                        //obj.SustTecnicoTerminado = false;
                        //obj.SustLegalTerminado = false;
                        //obj.TablaAsmeTerminado = false;
                        //obj.InfCdnoTerminado = false;
                        //obj.Estadodatos  = 0;
                        //obj.EstadoInfo = 0;
                        //obj.Estadostl = 0; 
                        //obj.Estadotablaasme = 0;

                        obj.DatosGeneralesTerminado = true;
                        obj.SustTecnicoTerminado = true;
                        obj.SustLegalTerminado = true;
                        obj.TablaAsmeTerminado = true;
                        obj.InfCdnoTerminado = true;
                        obj.Estadodatos = 2;
                        obj.EstadoInfo = 2;
                        obj.Estadostl = 2;
                        obj.Estadotablaasme = 2;

                        break;
                    case OperacionExpediente.Eliminacion:
                        obj.DatosGeneralesTerminado = true;
                        obj.SustTecnicoTerminado = true;
                        obj.SustLegalTerminado = true;
                        obj.TablaAsmeTerminado = true;
                        obj.InfCdnoTerminado = true;
                        obj.Estadodatos = 2;
                        obj.EstadoInfo = 2;
                        obj.Estadostl = 2;
                        obj.Estadotablaasme = 2;
                        break;
                }
                
                _procedimientoRepository.SaveOnlyProcedimiento(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CambiarObsEstado( long ProcedimientoId, string Pantalla, int estado)
        {
            try
            {
                Procedimiento obj = _procedimientoRepository.GetOne(x => x.ProcedimientoId == ProcedimientoId);

                if (Pantalla == "Datos Generales")
                {
                    obj.ObsDG = estado;
                    if (estado == 1) { 
                        obj.ObsDGH = estado;
                    }
                }
                else if (Pantalla == "Sustento Tecnico")
                {
                    obj.ObsSTL = estado;
                    if (estado == 1)
                    {
                        obj.ObsSTLH = estado;
                    }
                }
                else if (Pantalla == "Tabla Asme")
                {
                    obj.ObsASME = estado;
                    if (estado == 1)
                    {
                        obj.ObsASMEH = estado;
                    }
                } 

                _procedimientoRepository.SaveOnlyProcedimiento(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ResetObsEstado(long ProcedimientoId, int estado)
        {
            try
            {
                Procedimiento obj = _procedimientoRepository.GetOne(x => x.ProcedimientoId == ProcedimientoId);               
                obj.ObsDG = estado;
                obj.ObsSTL = estado;
                obj.ObsASME = estado;

                _procedimientoRepository.SaveOnlyProcedimiento(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimiento> GetAllLikePaginByTupa(TipoTupa tipo, Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Procedimiento> query = _procedimientoRepository.GetByTipoTupa(tipo);

                var data = query
                            .Where(x => x.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Denominacion) ? x.Denominacion : filtro.Denominacion).ToUpper())
                                    && x.Expediente.Entidad.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Entidad.Nombre) ? x.Expediente.Entidad.Nombre : filtro.Expediente.Entidad.Nombre).ToUpper())
                            );

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


        public List<Procedimiento> GetAllLikePaginByTupaEstandar(TipoTupa tipo, Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Procedimiento> query = _procedimientoRepository.GetByTipoTupaEstantar(tipo);

                var data = query
                            .Where(x => x.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Denominacion) ? x.Denominacion : filtro.Denominacion).ToUpper())
                                    && x.Expediente.Entidad.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Entidad.Nombre) ? x.Expediente.Entidad.Nombre : filtro.Expediente.Entidad.Nombre).ToUpper())
                            );

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

        public List<Procedimiento> GetAllLikePaginByTupaEstandarDelete(long expedienteId, long tipo, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Procedimiento> query = _procedimientoRepository.GetByTipoTupaEstantarDelete(expedienteId,tipo);

                //var data = query
                // .Where(x => x.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Denominacion) ? x.Denominacion : filtro.Denominacion).ToUpper())
                //&& x.Expediente.Entidad.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Entidad.Nombre) ? x.Expediente.Entidad.Nombre : filtro.Expediente.Entidad.Nombre).ToUpper())
                //);

                var data = query
                 .Where(x => x.TipoProcedimiento != TipoProcedimiento.Estandar && x.TipoProcedimiento != TipoProcedimiento.EstandarServicio);
            
           

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

        public List<Procedimiento> GetAllLikePaginByTupaxidDelete(long expedienteId, long tipo, Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Procedimiento> query = _procedimientoRepository.GetByTipoTupaxidDelete(expedienteId,tipo, filtro.ExpedienteId);

                //var dataw = query
                //.Where(x => x.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Denominacion) ? x.Denominacion : filtro.Denominacion).ToUpper())
                //&& x.Expediente.Entidad.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Entidad.Nombre) ? x.Expediente.Entidad.Nombre : filtro.Expediente.Entidad.Nombre).ToUpper())
                //).ToString();

                //var data = query
                // .Where(x => x.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Denominacion) ? x.Denominacion : filtro.Denominacion).ToUpper())
                //&& x.Expediente.Entidad.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Entidad.Nombre) ? x.Expediente.Entidad.Nombre : filtro.Expediente.Entidad.Nombre).ToUpper())
                //);
                var data = query
               .Where(x => x.TipoProcedimiento != TipoProcedimiento.Estandar && x.TipoProcedimiento != TipoProcedimiento.EstandarServicio);



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


        public List<Procedimiento> GetAllLikePaginByTupaxid(TipoTupa tipo, Procedimiento filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Procedimiento> query = _procedimientoRepository.GetByTipoTupaxid(tipo, filtro.ExpedienteId);

                var data = query
                            .Where(x => x.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Denominacion) ? x.Denominacion : filtro.Denominacion).ToUpper())
                                    && x.Expediente.Entidad.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Entidad.Nombre) ? x.Expediente.Entidad.Nombre : filtro.Expediente.Entidad.Nombre).ToUpper())
                            );

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

        public void CopiarProcedimiento(long ExpedienteId, List<long> ids, long UsuarioId, long EntidadId, int CodigoCanalOficinaEntidad)
        {
            try
            {
                Usuario user = _usuarioRepository.GetOne(UsuarioId);
                List<Procedimiento> procs = _procedimientoRepository.GetNoTracking(ids);
                MaestroFijo modelMaestroFijo = _maestroFijoService.GetOneByEntidad(EntidadId); 

                if (modelMaestroFijo == null)
                    modelMaestroFijo = new MaestroFijo();

                if (modelMaestroFijo.MaestroFijoDatoAdicional == null)
                    modelMaestroFijo.MaestroFijoDatoAdicional = new List<MaestroFijoDatoAdicional>();

                if (modelMaestroFijo.MaestroFijoSede == null)
                    modelMaestroFijo.MaestroFijoSede = new List<MaestroFijoSede>();

                if (modelMaestroFijo.MaestroFijoPasoSeguir == null)
                    modelMaestroFijo.MaestroFijoPasoSeguir = new List<MaestroFijoPasoSeguir>();

                if (modelMaestroFijo.MaestroFijoNotaCiudadano == null)
                    modelMaestroFijo.MaestroFijoNotaCiudadano = new List<MaestroFijoNotaCiudadano>();

                foreach (Procedimiento p in procs)
                {
                    p.ProcedimientoId = 0;
                    p.ExpedienteId = ExpedienteId;
                    p.Operacion = OperacionExpediente.Nuevo;
                    p.TupaId = null;

                    p.DatosGeneralesTerminado = false;

                    p.UndOrgResponsable = null;
                    p.UndOrgResponsableId = null;

                    p.CargoReconsideracion = "";
                    p.UndOrgReconsideracion = null;
                    p.UndOrgReconsideracionId = null;
                    p.PzoReconPresent = 0;
                    p.PzoReconResol = 0;

                    p.CargoApelacion = "";
                    p.UndOrgApelacion = null;
                    p.UndOrgApelacionId = null;
                    p.PzoApelPresent = 0;
                    p.PzoApelResol = 0;

                    p.FecCreacion = DateTime.Now;
                    p.UserCreacion = user.NroDocumento;
                    p.FecModificacion = DateTime.Now;
                    p.UserModificacion = user.NroDocumento;


                    p.EstadoInfo = 0; 
                    p.Estadodatos = 0; 
                    p.Estadostl = 0; 
                    p.Estadotablaasme = 0;



                    foreach (BaseLegalNorma t in p.BaseLegal.BaseLegalNorma)
                    {
                        t.EstadoACR = "0"; 
                         
                    } 


                    foreach (TablaAsme t in p.TablaAsme)
                    {
                        t.FecCreacion = DateTime.Now;
                        t.UserCreacion = user.NroDocumento;
                        t.FecModificacion = DateTime.Now;
                        t.UserModificacion = user.NroDocumento;
                        foreach (Actividad h in t.Actividad)
                        {
                            h.ActividadRecurso = null;

                        }
                       
                    }

                    if (p.Anexo==null) {

                        p.Anexo = modelMaestroFijo.Anexo;
                    }
                    if (p.Telefono == null)
                    {

                        p.Telefono = modelMaestroFijo.Telefono;
                    }
                    if (p.Correo == null)
                    {

                        p.Correo = modelMaestroFijo.Correo;
                    }

                    if (p.ProcedimientoSede.Count == 0)
                    {
                        foreach (var obj in modelMaestroFijo.MaestroFijoSede)
                        {
                            p.ProcedimientoSede.Add(new ProcedimientoSede()
                            {
                                SedeId = obj.SedeId,
                                Sede = obj.Sede
                            });
                        }
                    }

                    if (modelMaestroFijo.MaestroFijoSede != null)
                        if (modelMaestroFijo.MaestroFijoSede.Count() > 0)
                        {
                            foreach (MaestroFijoSede mf in modelMaestroFijo.MaestroFijoSede)
                            {
                                if ((mf.Mostrar ?? false) == true)
                                    if (mf.MaestroFijoUndOrgRecepcionDocum != null)
                                    {
                                        if (mf.MaestroFijoUndOrgRecepcionDocum.Count() > 0)
                                            foreach (MaestroFijoUndOrgRecepcionDocum mfu in mf.MaestroFijoUndOrgRecepcionDocum)
                                            {
                                                ProcedimientoSede ps = p.ProcedimientoSede.Where(x => x.SedeId == mfu.SedeId).FirstOrDefault();

                                                if (ps != null)
                                                {
                                                    if (ps.UndOrgRecepcionDocumentos == null) ps.UndOrgRecepcionDocumentos = new List<UndOrgRecepcionDocumentos>();

                                                    ps.UndOrgRecepcionDocumentos.Add(new UndOrgRecepcionDocumentos()
                                                    {
                                                        UnidadOrganicaId = mfu.UnidadOrganicaId,
                                                        SedeId = mfu.SedeId,
                                                    });
                                                }
                                            }
                                    }
                            }
                        }


                    if (p.PasoSeguir.Count == 0)
                    {
                        foreach (var obj in modelMaestroFijo.MaestroFijoPasoSeguir)
                        {
                            p.PasoSeguir.Add(new PasoSeguir()
                            {
                                PasoSeguirId = obj.PasoSeguirId,
                                Descripcion = obj.Descripcion
                            });
                        }
                    }

                    if (p.NotaCiudadano.Count == 0)
                    {
                        foreach (var obj in modelMaestroFijo.MaestroFijoNotaCiudadano)
                        {
                            p.NotaCiudadano.Add(new NotaCiudadano()
                            {
                                NotaCiudadanoId = obj.NotaCiudadanoId,
                                TipoNotaId = obj.MetaDatoTipoNotaId,
                                Nota = obj.Comentario
                            });
                        }
                    }

                

                    for (int i = 0; i < p.ProcedimientoDatoAdicional.Count(); i++)
                    {
                        if (p.ProcedimientoDatoAdicional[i].Comentario == null)
                        {
                            for (int a = 0; a < modelMaestroFijo.MaestroFijoDatoAdicional.Count(); a++)
                            {
                                //if (modelMaestroFijo.MaestroFijoDatoAdicional.Count - 1 >= i)
                                //{
                                if (p.ProcedimientoDatoAdicional[i].MetaDatoId == modelMaestroFijo.MaestroFijoDatoAdicional[a].MetaDatoId  ) { 
                                    p.ProcedimientoDatoAdicional[i].Comentario = modelMaestroFijo.MaestroFijoDatoAdicional[a].Comentario; 
                                    p.ProcedimientoDatoAdicional[i].Checked = true;
                                }
                                //}
                            }
                        }
                    }



 





                    _procedimientoRepository.Insert(p);
                }

                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(long id)
        {
            try
            {
                //_miembroEquipoRepository.Delete(new MiembroEquipo() { MiembroEquipoId = id }); 
                _procedimientoRepository.Delete(_procedimientoRepository.GetOne(x => x.ProcedimientoId == id));
                //_procedimientoRepository.Delete(new Procedimiento() { ProcedimientoId = id });
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedimientoSede> GetSedesByExpediente(long ExpedienteId)
        {
            try
            {
                return _procedimientoSedeRepository.GetByExpediente(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> ValidarSustTecnico(long ProcedimientoId)
        {
            try
            {
                List<string> lista = new List<string>();
                bool valid = true;
                
                //var actividades = _actividadRepository
                //                    .GetAllBy(x => x.TablaAsme.Procedimiento.ProcedimientoId == ProcedimientoId
                //                                && x.TipoActividad != TipoActividad.Espera
                //                                && x.Duracion == 0
                //                                && (
                //                                    (x.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                //                                    && x.TablaAsme.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar)
                //                                ||
                //                                    (x.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                //                                    && x.TablaAsme.Procedimiento.TipoProcedimiento == TipoProcedimiento.Estandar)
                //                                ));


                var actividades = _actividadRepository
                                    .GetAllBy(x => x.TablaAsme.Procedimiento.ProcedimientoId == ProcedimientoId
                                                && x.TipoActividad != TipoActividad.Espera
                                                && x.Duracion == 0
                                                &&   x.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal  
                                                ) ;
                if (actividades.Count() > 0)
                {
                    List<string> procs = actividades.GroupBy(x => x.TablaAsme.Codigo).Select(x => x.Key).ToList();
                    lista.Add(string.Format("Las siguientes Tablas ASME-VM tienen actividades con duración igual a cero : {0}", string.Join(",", procs.ToArray())));
                }

                valid = true;
                //var listTA = _tablaAsmeRepository
                //                    .GetAllBy(x => x.ProcedimientoId == ProcedimientoId
                //                    && x.Actividad.Count() == 0
                //                    && (
                //                        (x.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                //                        && x.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar)
                //                    ||
                //                        (x.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                //                        && x.Procedimiento.TipoProcedimiento == TipoProcedimiento.Estandar)
                //                    ));
                //var listTAGratis = _tablaAsmeRepository
                //                   .GetAllBy(x => x.ProcedimientoId == ProcedimientoId
                //                   && x.Actividad.Count() == 0 && x.EsGratuito == true
                //                   && (
                //                       (x.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                //                       && x.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar)
                //                   ||
                //                       (x.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                //                       && x.Procedimiento.TipoProcedimiento == TipoProcedimiento.Estandar)
                //                   ));

                var listTA = _tablaAsmeRepository
                                  .GetAllBy(x => x.ProcedimientoId == ProcedimientoId
                                  && x.Actividad.Count() == 0
                                  &&  x.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal 
                                   );
                var listTAGratis = _tablaAsmeRepository
                                   .GetAllBy(x => x.ProcedimientoId == ProcedimientoId
                                   && x.Actividad.Count() == 0 && x.EsGratuito == true
                                   &&  x.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal 
                                    );

                //if (listTA.Count() != listTAGratis.Count())
                //{
                    if (listTA.Count() > 0)
                    {
                        lista.Add(string.Format("Las siguientes Tablas ASME-VM no tienen actividades registradas : {0}", string.Join(",", listTA.Select(x => x.Descripcion).ToArray())));
                    }
                //}

              

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetOrdenNewProcByEntidad(long EntidadId)
        {
            try
            {
                int resp = 1;
                var procs = _procedimientoRepository.GetProcByEntidad(EntidadId).ToList();
                var numeros = procs.Where(x => x.Codigo != "S/C")
                    //.Select(x => int.Parse(x.Codigo.Substring(14,4)))
                    .Select(x => Convert.ToInt32(x.ExpedienteId))
                    .ToList<int>();

                if(numeros.Count > 0)
                {
                    //resp = numeros.Max() + 1;

                    resp = numeros.Count + 1;
                }

                return resp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<string> ImportarProcedimientoACR(Procedimiento obj, long EntidadId)
        {
            List<string> mensajes = new List<string>();
            try
            {
                mensajes = _procedimientoRepository.ImportarProcedimientoACR(obj, EntidadId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack(); 
                mensajes.Add(ex.Message);
                _log.Error(ex);
            }
            return mensajes;
        }

        public List<string> ImportarProcedimientoACREXANTE(Procedimiento obj, long EntidadId)
        {
            List<string> mensajes = new List<string>();
            try
            {
                mensajes = _procedimientoRepository.ImportarProcedimientoACREXANTE(obj, EntidadId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                mensajes.Add(ex.Message);
                _log.Error(ex);
            }
            return mensajes;
        }


        

    }
}
