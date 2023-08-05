using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IDashboardService
    {
        Dashboard getDashboard(Dashboard dashboard);
        Dashboard getDashboardCalendario(Dashboard dashboard);

        Dashboard RegistrarVideo(Dashboard dashboard);

        Dashboard ListarVideo(Dashboard dashboard);

        Dashboard EditarVideo(Dashboard dashboard);

        Dashboard EliminarVideo(Dashboard dashboard);


    }
}
