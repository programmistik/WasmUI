using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WasmUI.Server.Services
{
    public static class SettingsClass
    {
        public static IConfigurationSection AppSettings { get; set; }
        public static string GatewayLink { get; set; }

        static SettingsClass()
        {
            AppSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings");
            GatewayLink = AppSettings["Gateway"];
        }


    }
}
