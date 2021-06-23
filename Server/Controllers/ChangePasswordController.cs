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
    public class ChangePasswordController : Controller
    {
        [HttpPost]
        public async Task<ChangeResponseDto> Post(UserForChangeDto userForChange)
        {
            var _client = new HttpClient();
            var content = JsonSerializer.Serialize(userForChange);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var registrationResult = await _client.PostAsync("https://localhost:5001/api/accounts/change", bodyContent);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();
            if (!registrationResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ChangeResponseDto>(registrationContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return new ChangeResponseDto { IsSuccessfulRegistration = true };

        }
    }
}
