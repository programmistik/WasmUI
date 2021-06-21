using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
            var _client = new HttpClient();
            var content = JsonSerializer.Serialize(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage authResult = await _client.PostAsync("https://localhost:5001/api/accounts/login", bodyContent);

            if (authResult.IsSuccessStatusCode)
            {
                
                
                var authContent = await authResult.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<AuthResponseDto>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result;
            }
            return new AuthResponseDto() { IsAuthSuccessful = false};
            //return authResult;
        }
    }
}
