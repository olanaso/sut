using Sut.IApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sut.Entities;
using Sut.IRepositories;

namespace Sut.ApplicationServices
{
    public class RecursoCostoService : IRecursoCostoService
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IRecursoCostoRepository _recursoCostoRepository;

        public RecursoCostoService(IUnitOfWork unitOfWork,
                                    IRecursoCostoRepository recursoCostoRepository)
        {
            _unitOfWork = unitOfWork;
            _recursoCostoRepository = recursoCostoRepository;
        }

        public List<RecursoCosto> GetCostoPersonal(long ExpedienteId)
        {
            try
            {
                List<RecursoCosto> rc = _recursoCostoRepository.GetAllBy(x => x.ExpedienteId == ExpedienteId && x.Recurso.TipoRecurso == TipoRecurso.Personal);
                List<RecursoCosto> lista = _recursoCostoRepository.GetCostoPersonal(ExpedienteId);
                
                var query = lista.GroupJoin(rc,
                    a => new { a.UnidadOrganicaId, a.RecursoId },
                    b => new { b.UnidadOrganicaId, b.RecursoId },
                    (a, b) => new { src = a, db = b })
                    .Select(x => {
                        if (x.db.Count() > 0)
                        {
                            x.src.RecursoCostoId = x.db.First().RecursoCostoId;
                            x.src.CostoAnual = x.db.First().CostoAnual;
                            x.src.CostoUnitario = x.db.First().CostoUnitario;
                            x.src.EscalaIngreso = x.db.First().EscalaIngreso;
                            x.src.DocSustento = x.db.First().DocSustento;
                        }
                        return x.src;
                    });

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RecursoCosto> GetRecursoCosto(long ExpedienteId, TipoRecurso tipo)
        {
            try
            {
                var t = (Int32)tipo;
                List<RecursoCosto> rc = _recursoCostoRepository.GetAllBy(x => x.ExpedienteId == ExpedienteId && (int)x.Recurso.TipoRecurso == t);

                List<RecursoCosto> lista = _recursoCostoRepository.GetRecursoCosto(ExpedienteId, tipo);

                var query = lista.GroupJoin(rc,
                    a => a.RecursoId,
                    b => b.RecursoId,
                    (a, b) => new { src = a, db = b })
                    .Select(x => {
                        if (x.db.Count() > 0)
                        {
                            x.src.RecursoCostoId = x.db.First().RecursoCostoId;
                            x.src.InductorId = x.db.First().InductorId;
                            x.src.Cantidad = x.db.First().Cantidad;
                            x.src.CostoAdquisicion = x.db.First().CostoAdquisicion;
                            x.src.CostoAnual = x.db.First().CostoAnual;
                            x.src.CostoUnitario = x.db.First().CostoUnitario;
                            x.src.DocSustento = x.db.First().DocSustento;
                        }
                        return x.src;
                    });

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Guardar(List<RecursoCosto> lista)
        {
            try
            {
                _recursoCostoRepository.Guardar(lista);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
