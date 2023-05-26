using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IMaestroFijoRepository : IBaseRepository<MaestroFijo>
    {
        MaestroFijo GetOne(long MaestroFijoId);
        MaestroFijo GetOneByEntidad(long EntidadId);
        MaestroFijo GetOneByEntidadMaestro(long EntidadId);

        void SaveOnlyEntidadMaestro(MaestroFijo obj);
        new void Save(MaestroFijo obj);
        void FijarDatoAdicional(long ExpedienteId, MaestroFijoDatoAdicional da);
        void FijarDatoConsultaTramite(long ExpedienteId, MaestroFijo model);
        void FijarSede(long ExpedienteId, MaestroFijoSede sede);
        void FijarPasoSeguir(long ExpedienteId, MaestroFijoPasoSeguir ps);
        void FijarNotaCiudadano(long ExpedienteId, MaestroFijoNotaCiudadano ps);

    }
}
