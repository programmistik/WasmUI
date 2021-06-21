using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WasmUI.Server.Services;
using WasmUI.Shared;

namespace WasmUI.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchProfileController : ControllerBase
    {
        [HttpGet]
        public async Task<List<ProfileClass>> Get(string searchStr)
        {
            var client = await GatewayService.CreateClient();

            var link = "https://localhost:44382/gateway/searchprofile/" + searchStr;

                var response = await client.GetAsync(link);
                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();

                    var profs = BsonSerializer.Deserialize<List<ProfileClass>>(content);
                return profs;
                }


            return new List<ProfileClass>();
        }
    }
}
