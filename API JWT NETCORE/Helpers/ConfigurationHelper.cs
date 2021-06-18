using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_JWT_NETCORE.Helpers
{
    public class ConfigurationHelper
    {
        public static IConfigurationSection GetSection(string section)
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection(section);
        }
    }
}
