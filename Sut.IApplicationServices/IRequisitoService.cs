using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IRequisitoService
    {
        Requisito GetOne(long RequisitoId);

        RequisitoFormulario GetOneForm(long RequisitoId, long FormularioId);

        List<Requisito> GetByProcedimiento(long ProcedimientoId);
        List<Requisito> GetByProcedimientoELI(long ProcedimientoId);
        List<RequisitoFormulario> listProcedimiento(long RequisitoId);
        List<Requisito> GetByExpediente(long ExpedienteId);


        void SaveFormulario(RequisitoFormulario obj);

        void Save(Requisito obj);

        void GuardarSustentoTecnico(Procedimiento obj);
        void GuardarSustentoLegal(Procedimiento obj);
    }
}
