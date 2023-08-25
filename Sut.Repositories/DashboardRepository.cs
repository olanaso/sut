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
using System.Security.Cryptography;

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
                parameters.Add("@nvParametro2", dashboard.parameter2);
                parameters.Add("@nvParametro3", dashboard.parameter3);
                parameters.Add("@nvParametro4", dashboard.parameter4);
                parameters.Add("@nvParametro5", dashboard.parameter5);
                parameters.Add("@nvParametro6", dashboard.parameter6);
                parameters.Add("@nvParametro7", dashboard.parameter7);

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

        public Dashboard RegistrarVideo(Dashboard dashboard)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {

                var parameters = new DynamicParameters();
                parameters.Add("@iOpSp", dashboard.iOpSp);
                parameters.Add("@nvParametro1", dashboard.parameter1); //titulo
                parameters.Add("@nvParametro2", dashboard.parameter2); //[Descripcion]
                parameters.Add("@nvParametro3", dashboard.parameter3); // [url]
                parameters.Add("@nvParametro4", dashboard.parameter4); // [path]
                parameters.Add("@nvParametro5", dashboard.parameter5); // [duracion]
                parameters.Add("@nvParametro6", dashboard.parameter6); // [duracion]
                parameters.Add("@nvParametro7", dashboard.parameter7); // [duracion]


                var _result = conexion.Query<string>(
                                        ProcedimientoAuditoria.Mantener_Video,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);

                string resultado = "";

                foreach (string val in _result)
                {
                    resultado += val;
                }

                dashboard.result = resultado;
                return dashboard;
            }
        }


        public Dashboard ListarVideo(Dashboard dashboard)
        {

            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {

                var parameters = new DynamicParameters();
                parameters.Add("@iOpSp", dashboard.iOpSp);
                parameters.Add("@nvParametro1", dashboard.parameter1); //titulo
                parameters.Add("@nvParametro2", dashboard.parameter2); //[Descripcion]
                parameters.Add("@nvParametro3", dashboard.parameter3); // [url]
                parameters.Add("@nvParametro4", dashboard.parameter4); // [path]
                parameters.Add("@nvParametro5", dashboard.parameter5); // [duracion]
                parameters.Add("@nvParametro6", dashboard.parameter6); // [duracion]
                parameters.Add("@nvParametro7", dashboard.parameter7); // [duracion]




                var _result = conexion.Query<string>(
                                        ProcedimientoAuditoria.Mantener_Video,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);

                string resultado = "";

                foreach (string val in _result)
                {
                    resultado += val;
                }

                dashboard.result = resultado;
                return dashboard;
            }
        }


        public Dashboard EditarVideo(Dashboard dashboard)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {

                var parameters = new DynamicParameters();
                parameters.Add("@iOpSp", dashboard.iOpSp);
                parameters.Add("@nvParametro1", dashboard.parameter1); //titulo
                parameters.Add("@nvParametro2", dashboard.parameter2); //[Descripcion]
                parameters.Add("@nvParametro3", dashboard.parameter3); // [url]
                parameters.Add("@nvParametro4", dashboard.parameter4); // [path]
                parameters.Add("@nvParametro5", dashboard.parameter5); // [duracion]
                parameters.Add("@nvParametro6", dashboard.parameter6); // [duracion]
                parameters.Add("@nvParametro7", dashboard.parameter7); // [duracion]




                var _result = conexion.Query<string>(
                                        ProcedimientoAuditoria.Mantener_Video,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);

                string resultado = "";

                foreach (string val in _result)
                {
                    resultado += val;
                }

                dashboard.result = resultado;
                return dashboard;
            }
        }


        public Dashboard EliminarVideo(Dashboard dashboard)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {

                var parameters = new DynamicParameters();
                parameters.Add("@iOpSp", dashboard.iOpSp);
                parameters.Add("@nvParametro1", dashboard.parameter1); //titulo
                parameters.Add("@nvParametro2", dashboard.parameter2); //[Descripcion]
                parameters.Add("@nvParametro3", dashboard.parameter3); // [url]
                parameters.Add("@nvParametro4", dashboard.parameter4); // [path]
                parameters.Add("@nvParametro5", dashboard.parameter5); // [duracion]
                parameters.Add("@nvParametro6", dashboard.parameter6); // [duracion]
                parameters.Add("@nvParametro7", dashboard.parameter7); // [duracion]

                var _result = conexion.Query<string>(
                                        ProcedimientoAuditoria.Mantener_Video,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);

                string resultado = "";

                foreach (string val in _result)
                {
                    resultado += val;
                }

                dashboard.result = resultado;
                return dashboard;
            }
        }


    }
}
