using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IIncentivosRepository : IBaseRepository<Incentivos>
    {
        
        new void Save(Incentivos obj); 
        Incentivos GetByone(long IncentivosId); 

        new List<Incentivos> GetAll();
        new List<Incentivos> GetAllPeriodorepAdmin(long EntidadId);

        new List<Incentivos> GetAllPeriodo1RepAdmin(long EntidadId);
        new List<Incentivos> GetAllPeriodo2RepAdmin(long EntidadId);

        new List<Incentivos> GetAllPeriodo1(long EntidadId);
        new List<Incentivos> GetAllPeriodo2(long EntidadId);

        new List<Incentivos> GetAllPeriodoTodo(long EntidadId);
        new List<Incentivos> GetByIncentivosBajoMedio(long EntidadId);

        new List<Incentivos> GetByIncentivosBajoMedio2(long EntidadId);

         

        new List<Incentivos> GetByIncentivosAltoMuyAlto(long EntidadId);
        new List<Incentivos> GetByIncentivosAltoMuyAlto2(long EntidadId);

    }
}
