using Sut.Entities;
using System.Collections.Generic;

namespace Sut.IRepositories
{
    public interface IRequisitoRepository : IBaseRepository<Requisito>
    {
        Requisito GetOne(long RequisitoId);

        RequisitoFormulario GetOneForm(long RequisitoId, long FormularioId);


        List<Requisito> GetByProcedimiento(long ProcedimientoId);
        List<Requisito> GetByProcedimientoELI(long ProcedimientoId);

        List<RequisitoFormulario> listProcedimiento(long RequisitoId);
        void Save(Requisito obj);

        void SaveFormulario(RequisitoFormulario obj);
    }
}
