using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories.Configuracion
{
    public class Conexion
    {
        public static DbConnection GetConexionSUT()
        {
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var conn = factory.CreateConnection();

            if (conn == null) return null;

            conn.ConnectionString = ConfigurationManager.ConnectionStrings?["SUTEntities"]?.ConnectionString;
            conn.Open();
            return conn;
        }

    }
}
