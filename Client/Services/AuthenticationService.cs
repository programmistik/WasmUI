using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using WasmUI.Client.Providers;
using WasmUI.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WasmUI.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<AuthResponseDto> Login(AuthResponseDto res, UserForAuthenticationDto userForAuthentication)
        {

            if (!res.IsAuthSuccessful)
                return res;
            await _localStorage.SetItemAsync("authToken", res.Token);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(userForAuthentication.Email);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", res.Token);
            return new AuthResponseDto { IsAuthSuccessful = true };
        }
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            throw new NotImplementedException();
        }
    }
}
