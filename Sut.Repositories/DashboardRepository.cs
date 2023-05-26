using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Sut.Repositories.Procedimientos;
using System.Data;
using Sut.Repositories.Configuracion;

namespace Sut.Repositories
{
    public class DashboardRepository : BaseRepository<Dashboard>, IDashboardRepository
    {
        public DashboardRepository(IContext context) : base(context) { }

       
        public Dashboard getDashboard(Dashboard dashboard)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add("@iOpSp", dashboard.iOpSp);
                parameters.Add("@nvParametro1", dashboard.parameter1);
                parameters.Add("@nvParametro2", dashboard.parameter1);
                parameters.Add("@nvParametro3", dashboard.parameter1);
                parameters.Add("@nvParametro4", dashboard.parameter1);
                parameters.Add("@nvParametro5", dashboard.parameter1);
                parameters.Add("@nvParametro6", dashboard.parameter1);
                parameters.Add("@nvParametro7", dashboard.parameter1);

                var _idAuditoria = conexion.Query<string>(
                                        ProcedimientoAuditoria.ListarDashboard,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                string resultado = "";

                foreach (string val in _idAuditoria)
                {
                    resultado += val;
                }

                dashboard.result = resultado;
                return dashboard;
            }
        }

        public Dashboard getDashboardCalendario(Dashboard dashboard)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

               

                var _idAuditoria = conexion.Query<string>(
                                        ProcedimientoAuditoria.ListarActividadesFechas,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                string resultado = "";

                foreach (string val in _idAuditoria)
                {
                    resultado += val;
                }

                dashboard.result = resultado;
                return dashboard;
            }
        } 


    }
}
