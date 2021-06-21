using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
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
    public class MypostController : ControllerBase
    {
        
        [HttpGet]
        public async Task<Post> Get(string PostId)
        {
            var client = await GatewayService.CreateClient();


            var  link = "https://localhost:44382/gateway/postunique/" + PostId;

            var response = await client.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                //var posts = JsonConvert.DeserializeObject<Post>(content); ;
                var post = BsonSerializer.Deserialize<Post>(content);
                return post;
            }

            return new Post();
        }
    }
}
