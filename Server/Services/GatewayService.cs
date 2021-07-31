using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WasmUI.Server.Services
{
    public static class GatewayService //: IGatewayService
    {
        public static async Task<HttpClient> CreateClient()
        {
            var st = SettingsClass.AppSettings;

            var ClientId = st["ClientId"];
            var ClientSecret = st["ClientSecret"];
            var Scope = st["Scope"];
            var ids = st["IdentityServer"];

            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(ids);
            if (disco.IsError)
            {
                Log.Error(disco.Error);
            }

            //GET TOKEN
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = ClientId,
                ClientSecret = ClientSecret,
                Scope = Scope
            });
            if (tokenResponse.IsError)
            {
                Log.Error(tokenResponse.Error);
            }


            //CALL API
            client.SetBearerToken(tokenResponse.AccessToken);
            return client;
        }
    }
}
