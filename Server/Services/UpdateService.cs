using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WasmUI.Shared;

namespace WasmUI.Server.Services
{
    public static class UpdateService
    {
        [HttpGet]
        public static async Task UpdatePostAsync(string id, string uri)
        {
            // get post by id
            var client = await GatewayService.CreateClient();


            var link = SettingsClass.GatewayLink + "postunique/" + id;

            var response = await client.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                var post = BsonSerializer.Deserialize<Post>(content);
                post.Image = uri;
                
                await PutPostAsync(post);
            }
            else
                Log.Error(response.RequestMessage.ToString());
            ;
        }

        [HttpPut]
        public static async Task PutPostAsync(Post post)
        {

            var client = await GatewayService.CreateClient();

            var link = SettingsClass.GatewayLink + "editpost";

            var js = JsonConvert.SerializeObject(post);
            HttpContent content = new StringContent(js, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(link, content);

            if (response.IsSuccessStatusCode)
            {

            }

        }

        [HttpGet]
        public static async Task UpdateProfileAsync(string AppUserId, string uri)
        {
            // get profile by id
            var client = await GatewayService.CreateClient();

            var link = SettingsClass.GatewayLink + "Profile/" + AppUserId;
            var response = await client.GetAsync(link);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var prof = JsonConvert.DeserializeObject<ProfileClass>(content);

                prof.Avatara = uri;

                await PutProfileAsync(prof);
            }
            ;
        }

        [HttpPut]
        public static async Task PutProfileAsync(ProfileClass profile)
        {

            var client = await GatewayService.CreateClient();

            var link = SettingsClass.GatewayLink + "Profile/" + profile.AppUserId;

            var js = JsonConvert.SerializeObject(profile);
            HttpContent content = new StringContent(js, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(link, content);

            if (response.IsSuccessStatusCode)
            {

            }

        }

        [HttpGet]
        public static async Task<Post> GetPostAsync(string id)
        {
            // get post by id
            var client = await GatewayService.CreateClient();


            var link = SettingsClass.GatewayLink + "post/" + id;

            var response = await client.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                var post = BsonSerializer.Deserialize<Post>(content);
                return post;
              
            }
            return new Post();
        }
    }
}
