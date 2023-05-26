using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IIncentivosCorteRepository : IBaseRepository<IncentivosCorte>
    {
        
        new void Save(IncentivosCorte obj); 
        IncentivosCorte GetByone(long IncentivosCorteId); 

        new List<IncentivosCorte> GetAll();
        new List<IncentivosCorte> GetAllPeriodorepAdmin(long EntidadId);

        new List<IncentivosCorte> GetAllPeriodo1RepAdmin(long EntidadId);
        new List<IncentivosCorte> GetAllPeriodo2RepAdmin(long EntidadId);

        new List<IncentivosCorte> GetAllPeriodo1(long EntidadId);
        new List<IncentivosCorte> GetAllPeriodo2(long EntidadId);

        new List<IncentivosCorte> GetAllPeriodoTodo(long EntidadId);
        new List<IncentivosCorte> GetByIncentivosCorteBajoMedio(long EntidadId);

        new List<IncentivosCorte> GetByIncentivosCorteBajoMedio2(long EntidadId);

         

        new List<IncentivosCorte> GetByIncentivosCorteAltoMuyAlto(long EntidadId);
        new List<IncentivosCorte> GetByIncentivosCorteAltoMuyAlto2(long EntidadId);

    }
}
