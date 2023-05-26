using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IIncentivosCorteService
    {
       
        void Save(IncentivosCorte obj);
         
        List<IncentivosCorte> GetByIncentivosCorte(IncentivosCorte filtro, long EntidadId, int pageIndex, int pageSize, Int32 tipoperiodo , ref int totalRows);

        List<IncentivosCorte> listaByIncentivosCorte();
        List<IncentivosCorte> GetByImformacionIncentivosCorte(long EntidadId, int Tipoperiodo, Rol Roles);

        List<IncentivosCorte> GetByIncentivosCorteBajoMedio(long EntidadId, int tipoperiodo);
        List<IncentivosCorte> GetByIncentivosCorteAltoMuyAlto(long EntidadId, int tipoperiodo);
        IncentivosCorte GetByone(long UsuarioId);


    }
}
