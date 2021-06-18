using API_JWT_NETCORE.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_JWT_NETCORE.Models
{
    public class Conexion
    {
        public static SqlConnection Conectar()
        {
            try
            {
                SqlConnection con = null;
                string query = GetConnectionString();
                con = new SqlConnection(query);
                return con;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static string GetConnectionString()
        {
            try
            {
                //string con = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["DB"];
                string con = ConfigurationHelper.GetSection("ConnectionStrings")["DB"];
                return con;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
