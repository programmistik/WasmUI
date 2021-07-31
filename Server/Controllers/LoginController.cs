using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WasmUI.Server.Services;
using WasmUI.Shared.DTO;

namespace WasmUI.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<AuthResponseDto> Post(UserForAuthenticationDto userForAuthentication)
        {
            var ChangePassLink = SettingsClass.AppSettings["ChangePass"];
            var _client = new HttpClient();
            var content = JsonSerializer.Serialize(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage authResult = await _client.PostAsync(ChangePassLink, bodyContent);

            if (authResult.IsSuccessStatusCode)
            {               
                
                var authContent = await authResult.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<AuthResponseDto>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                return result;
            }
            
            Log.Error(authResult.ToString());

            return new AuthResponseDto() { IsAuthSuccessful = false};
        }
    }
}
