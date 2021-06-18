﻿using System;
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

        public static object ToLista(this Exception exception)
        {
            return new { errors = new { General = new string[] { exception.Message } } };
        }

        public static object ToDB(this String value)
        {
            return String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value) ? DBNull.Value : (object)value;
        }

        public static object ToDB(this int value)
        {
            return value <= 0 ? DBNull.Value : (object)value;
        }
    }
}
