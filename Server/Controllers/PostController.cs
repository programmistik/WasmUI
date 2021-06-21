﻿using IdentityModel.Client;
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
    public class PostController : ControllerBase
    {
        //private readonly GatewayService _gatewayClient;

        //public PostController(GatewayService gatewayClient)
        //{
        //    _gatewayClient = gatewayClient;
        //}

        // Находит все созданные пользователем посты по AppUserId
        [HttpGet]
        public async Task<List<Post>> Get(string AppUserId)
        {
            var client = await GatewayService.CreateClient();

            string link;
            if(AppUserId == null)
                link = "https://localhost:44382/gateway/getposts";
            else
                link = "https://localhost:44382/gateway/getposts/" + AppUserId;

            var response = await client.GetAsync(link);
            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();

                var posts = BsonSerializer.Deserialize<List<Post>>(content); // JsonConvert.DeserializeObject<List<Post>>(content); ;
                return posts;
            }

            return new List<Post>();
        }

        [HttpPut]
        public async Task Put(Post post)
        {

            var client = await GatewayService.CreateClient();

            var link = "https://localhost:44382/gateway/editpost";

            var js = JsonConvert.SerializeObject(post);
            HttpContent content = new StringContent(js, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(link, content);

            if (response.IsSuccessStatusCode)
            {

            }

        }

        [HttpPost]
        public async Task Post(Post post)
        {

            var client = await GatewayService.CreateClient(); 

            var link = "https://localhost:44382/gateway/addpost";

            var js = JsonConvert.SerializeObject(post);
            HttpContent content = new StringContent(js, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(link, content);

            if (response.IsSuccessStatusCode)
            {
                var cont = await response.Content.ReadAsStringAsync();

                var created = BsonSerializer.Deserialize<Post>(cont);
            }

        }

        [HttpDelete]
        public async Task Delete(string PostId)
        {
            var client = await GatewayService.CreateClient();

            var link = "https://localhost:44382/gateway/deletepost/" + PostId;


            var response = await client.DeleteAsync(link);

            if (response.IsSuccessStatusCode)
            {
            }

        }


    }
}