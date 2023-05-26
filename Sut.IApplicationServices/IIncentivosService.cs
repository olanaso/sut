using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IIncentivosService
    {
       
        void Save(Incentivos obj);
         
        List<Incentivos> GetByIncentivos(Incentivos filtro, long EntidadId, int pageIndex, int pageSize, Int32 tipoperiodo , ref int totalRows);

        List<Incentivos> listaByIncentivos();
        List<Incentivos> GetByImformacionIncentivos(long EntidadId, int Tipoperiodo, Rol Roles);

        List<Incentivos> GetByIncentivosBajoMedio(long EntidadId, int tipoperiodo);
        List<Incentivos> GetByIncentivosAltoMuyAlto(long EntidadId, int tipoperiodo);
        Incentivos GetByone(long UsuarioId);


    }
}
