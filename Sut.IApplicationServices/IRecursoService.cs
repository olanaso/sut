using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IRecursoService
    {
        Recurso GetOne(long RecursoId);
        List<Recurso> GetAll(long EntidadId, TipoRecurso tipo);
        List<Recurso> GetAllLikePagin(Recurso filtro, int pageIndex, int pageSize, ref int totalRows);
        List<Recurso> GetAllLikePagin(Recurso filtro, int tipo, int pageIndex, int pageSize, ref int totalRows);
        void Save(Recurso obj);
        void Eliminar(long idRecurso);
    }
}
