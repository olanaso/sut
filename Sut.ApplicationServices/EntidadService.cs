using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.ApplicationServices
{
    public class EntidadService : IEntidadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntidadRepository _entidadRepository;
        private readonly IMetaDatoRepository _metaDatoRepository;
        private readonly IProcedimientoRepository _procedimientoRepository;
        private readonly IAuditoriaRepository _auditoriaRepository;

        public EntidadService(IUnitOfWork unitOfWork,
                                IEntidadRepository entidadRepository,
                                IMetaDatoRepository metaDatoRepository,
                                IProcedimientoRepository procedimientoRepository,
                                IAuditoriaRepository auditoriaRepository)
        {
            _unitOfWork = unitOfWork;
            _entidadRepository = entidadRepository;
            _metaDatoRepository = metaDatoRepository;
            _procedimientoRepository = procedimientoRepository;
            _auditoriaRepository = auditoriaRepository;
        }
        
        public List<Entidad> EstandarGetAllLikePagin(Entidad filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Entidad> query = _entidadRepository.GetAllBy(x => x.TipoTupa == TipoTupa.Estandar);

                var data = query.Where(x => x.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Nombre) ? x.Nombre : filtro.Nombre).ToUpper()))
                            .OrderByDescending(x => x.EntidadId);

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
        public List<Entidad> GetAllLikePagin(Entidad filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Entidad> query = _entidadRepository.GetByTipoTupa(filtro.TipoTupa);

                var data = query.Where(x => x.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Nombre) ? x.Nombre : filtro.Nombre).ToUpper())
                                        && x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper())
                                        && x.Acronimo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Acronimo) ? x.Acronimo : filtro.Acronimo).ToUpper())
                                    );

                if (filtro.DepartamentoId > 0) data = data.Where(x => x.ProvinciaId != null); 

                if (filtro.NivelGobiernoId > 0) data = data.Where(x => x.NivelGobiernoId == filtro.NivelGobiernoId);
                if (filtro.SectorId > 0) data = data.Where(x => x.SectorId == filtro.SectorId);

                if (filtro.DepartamentoId > 0) {
                    data = data.Where(x => x.Provincia.Departamento.DepartamentoId == filtro.DepartamentoId);
                    if (filtro.ProvinciaId > 0) data = data.Where(x => x.ProvinciaId == filtro.ProvinciaId);
                }
               
   
                data = data.OrderBy(x => x.Nombre);

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
        public List<Entidad> GetAllLikePaginDetalle(Entidad filtro, int pageIndex, int pageSize, ref int totalRows, long usuarioId)
        {
            try
            {

                
                List<Entidad> query = _entidadRepository.lista_entidades(0, usuarioId);


                var data = query.Where(x => x.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Nombre) ? x.Nombre : filtro.Nombre).ToUpper())
                                        && x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper())
                                        && x.Acronimo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Acronimo) ? x.Acronimo : filtro.Acronimo).ToUpper())
                                    );

                if (filtro.DepartamentoId > 0) data = data.Where(x => x.ProvinciaId != null);

                if (filtro.NivelGobiernoId > 0) data = data.Where(x => x.NivelGobiernoId == filtro.NivelGobiernoId);
                if (filtro.SectorId > 0) data = data.Where(x => x.SectorId == filtro.SectorId);

                if (filtro.DepartamentoId > 0)
                {
                    data = data.Where(x => x.DepartamentoId == filtro.DepartamentoId);
                    if (filtro.ProvinciaId > 0) data = data.Where(x => x.ProvinciaId == filtro.ProvinciaId);
                }


                data = data.OrderBy(x => x.Nombre);

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

        public List<Entidad> GetAllLikePaginDetalleActividad(Entidad filtro, int pageIndex, int pageSize, ref int totalRows, long usuarioId)
        {
            try
            {


                List<Entidad> query = _entidadRepository.lista_entidadesActividad(0, usuarioId);


                var data = query.Where(x => x.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Nombre) ? x.Nombre : filtro.Nombre).ToUpper())
                                        && x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper())
                                        && x.Acronimo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Acronimo) ? x.Acronimo : filtro.Acronimo).ToUpper())
                                    );

                if (filtro.DepartamentoId > 0) data = data.Where(x => x.ProvinciaId != null);

                if (filtro.NivelGobiernoId > 0) data = data.Where(x => x.NivelGobiernoId == filtro.NivelGobiernoId);
                if (filtro.SectorId > 0) data = data.Where(x => x.SectorId == filtro.SectorId);

                if (filtro.DepartamentoId > 0)
                {
                    data = data.Where(x => x.DepartamentoId == filtro.DepartamentoId);
                    if (filtro.ProvinciaId > 0) data = data.Where(x => x.ProvinciaId == filtro.ProvinciaId);
                }


                data = data.OrderBy(x => x.Nombre);

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
    
        public List<Entidad> GetAllLikePaginDetalleAsignado(Entidad filtro, int pageIndex, int pageSize, ref int totalRows, long usuarioId)
        {
            try
            {
                 
                List<Entidad> query = _entidadRepository.lista_entidadesAsignado(0, usuarioId);

                var data = query.Where(x => x.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Nombre) ? x.Nombre : filtro.Nombre).ToUpper())
                                        && x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper())
                                        && x.Acronimo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Acronimo) ? x.Acronimo : filtro.Acronimo).ToUpper())
                                    );

                //if (filtro.DepartamentoId > 0) data = data.Where(x => x.ProvinciaId != null);

                //if (filtro.NivelGobiernoId > 0) data = data.Where(x => x.NivelGobiernoId == filtro.NivelGobiernoId);
                //if (filtro.SectorId > 0) data = data.Where(x => x.SectorId == filtro.SectorId);

                //if (filtro.DepartamentoId > 0)
                //{
                //    data = data.Where(x => x.DepartamentoId == filtro.DepartamentoId);
                //    if (filtro.ProvinciaId > 0) data = data.Where(x => x.ProvinciaId == filtro.ProvinciaId);
                //}


                data = data.OrderBy(x => x.Nombre);

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
        public List<Entidad> GetAllLikePaginDetalleAsignadoActividad(Entidad filtro, int pageIndex, int pageSize, ref int totalRows, long pCalendarioActividadesId)
        {
            try
            {

                List<Entidad> query = _entidadRepository.lista_entidadesAsignadoActividad(pCalendarioActividadesId);

                var data = query.Where(x => x.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Nombre) ? x.Nombre : filtro.Nombre).ToUpper())
                                        && x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper())
                                        && x.Acronimo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Acronimo) ? x.Acronimo : filtro.Acronimo).ToUpper())
                                    );
                 

                data = data.OrderBy(x => x.Nombre);

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

        

        public Entidad GetOneProvincia(long ProvinciaId)
        {
            try
            {
                return _entidadRepository.GetOneProvincia(ProvinciaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Entidad GetOneMin(long SectorID)
        {
            try
            {
                return _entidadRepository.GetOneMin(SectorID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Entidad GetOne(long EntidadId)
        {
            try
            {
                return _entidadRepository.GetOne(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Entidad GetOne(string Codigo)
        {
            try
            {
                return _entidadRepository.GetOne(Codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Entidad obj)
        {
            try
            {
                if (obj.EntidadId == 0 && obj.TipoTupa == TipoTupa.Estandar)
                {
                    var cantidad = _entidadRepository.CountBy(x => x.TipoTupa == TipoTupa.Estandar);
                    obj.Codigo = string.Format("{0:D6}", cantidad + 1);
                }

                if (obj.TipoTupa == TipoTupa.Normal)
                {
                    int correlativo = obj.EntidadId == 0 ? _entidadRepository.MaxCorrelativo() + 1 : obj.Correlativo;
                    MetaDato nivel = _metaDatoRepository.GetOne(obj.NivelGobiernoId);
                    MetaDato sector = _metaDatoRepository.GetOne(obj.SectorId);
                    //cambio de codigo
                    obj.Codigo = string.Format("{0}.{1}.{2:D4}", nivel.Codigo, sector.Codigo, correlativo); 
                    obj.Correlativo = correlativo;
                }

                _entidadRepository.Guardar(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Entidad> GetAll()
        {
            try
            {
                return _entidadRepository.GetAllBy(x => x.TipoTupa == TipoTupa.Normal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Entidad> GetByTipoTupa(TipoTupa tipo)
        {
            try
            {
                return _entidadRepository.GetByTipoTupa(tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Entidad GetOneByCodigo(string Codigo)
        {
            try
            {
                return _entidadRepository.GetOne(x => x.Codigo == Codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Entidad GetOneByAcronimo(string Acronimo)
        {
            try
            {
                return _entidadRepository.GetOne(x => x.Acronimo == Acronimo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}