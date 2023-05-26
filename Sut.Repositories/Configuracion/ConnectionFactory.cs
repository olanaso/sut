using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories.Configuracion
{
    public class ConnectionFactory : IConnectionFactory
    {

        public IDbConnection GetConnectionSUT
        {
            get { return ObtenerConexion("SUTEntities"); }
        }


        private static DbConnection ObtenerConexion(string connectionStringName)
        {
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var conn = factory.CreateConnection();

            if (conn == null) return null;

            conn.ConnectionString = ConfigurationManager.ConnectionStrings?[connectionStringName]?.ConnectionString;
            conn.Open();
            return conn;
        }
    }
}
