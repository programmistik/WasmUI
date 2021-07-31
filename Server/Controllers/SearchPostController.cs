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
    public class SearchPostController : ControllerBase
    {
        [HttpGet]
        public async Task<List<Post> > Get(string searchStr)
        {
            var client = await GatewayService.CreateClient();

            var link = SettingsClass.GatewayLink + "searchpost/" + searchStr;

                var response = await client.GetAsync(link);
                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();

                    var posts = BsonSerializer.Deserialize<List<Post>>(content);
                return posts;
                }

            return new List<Post>();
        }
    }
}
