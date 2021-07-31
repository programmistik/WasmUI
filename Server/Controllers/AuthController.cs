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
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public async Task<RegistrationResponseDto> Post(UserForRegistrationDto userForRegistration)
        {
            var registrationLink = SettingsClass.AppSettings["Registration"];
            var _client = new HttpClient();
            var content = JsonSerializer.Serialize(userForRegistration);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var registrationResult = await _client.PostAsync(registrationLink, bodyContent);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();
            if (!registrationResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                Log.Error(registrationResult.StatusCode.ToString());
                return result;
            }
            return new RegistrationResponseDto { IsSuccessfulRegistration = true };

        }
    }
}
