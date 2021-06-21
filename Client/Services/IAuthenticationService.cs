using WasmUI.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WasmUI.Client.Services
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration);        
        Task<AuthResponseDto> Login( AuthResponseDto res, UserForAuthenticationDto userForAuthentication);
        Task Logout();
    }
}
