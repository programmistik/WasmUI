using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WasmUI.Server.Services;
using WasmUI.Shared;

namespace WasmUI.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {

        [HttpGet]
        public async Task<ProfileClass> Get(string AppUserId)
        {
            var client = await GatewayService.CreateClient();

            var link = SettingsClass.GatewayLink + "Profile/" + AppUserId;
            var response = await client.GetAsync(link);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                //var prof = BsonSerializer.Deserialize<ProfileClass>(content); ;
                var prof = JsonConvert.DeserializeObject<ProfileClass>(content);
                return prof;
            }
            return new ProfileClass();
        }

        [HttpPut]
        public async Task Put(ProfileClass prof)
        {
            
            var AppUserId = prof.AppUserId;

            var client = await GatewayService.CreateClient();

            var link = SettingsClass.GatewayLink + "Profile/" + AppUserId;

            var js = JsonConvert.SerializeObject(prof);
            HttpContent content = new StringContent(js, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(link, content);

            if (response.IsSuccessStatusCode)
            {
               
            }
           
        }

        [HttpPost]
        public async Task Post(ProfileClass prof)
        {

            var client = new HttpClient();
            var link = "http://localhost:5010/api/Profile";

            var js = JsonConvert.SerializeObject(prof);
            HttpContent content = new StringContent(js, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(link, content);

            if (response.IsSuccessStatusCode)
            {

            }

        }


    }
}
