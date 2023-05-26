using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IMaestroFijoService
    {
        MaestroFijo GetOne(long MaestroFijoId);
        MaestroFijo GetOneByEntidad(long EntidadId);
        MaestroFijo GetOneByEntidadMaestro(long EntidadId);
        void Save(MaestroFijo obj);
        void FijarDatoAdicional(long EntidadId, MaestroFijoDatoAdicional da);
        void FijarDatoConsultaTramite(long EntidadId, MaestroFijo model);
        void FijarSede(long EntidadId, MaestroFijoSede sede);
        void FijarPasoSeguir(long EntidadId, MaestroFijoPasoSeguir ps);
        void FijarNotaCiudadano(long ExpedienteId, MaestroFijoNotaCiudadano ps);


        void CambiarEstadoInfCdo(bool tipo, long EntidadId);
    }
}
