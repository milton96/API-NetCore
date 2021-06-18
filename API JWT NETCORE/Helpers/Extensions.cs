using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_JWT_NETCORE.Helpers
{
    public static class Extensions
    {
        public static T GetValor<T>(this SqlDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? default : (T)reader.GetValue(index);
        }
    }
}
