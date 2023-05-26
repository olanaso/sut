using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.ApplicationServices
{
    public class TupaService : ITupaService
    {
        private readonly ITupaRepository _tupaRepository;

        public TupaService(ITupaRepository tupaRepository)
        {
            _tupaRepository = tupaRepository;
        }

        public Tupa GetTupaVigenteByEntidad(long EntidadId)
        {
            try
            {
                var tupa = _tupaRepository
                                .GetOne(x => x.EntidadId == EntidadId 
                                && x.EstadoTupa == EstadoTupa.Vigente);
                return tupa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
