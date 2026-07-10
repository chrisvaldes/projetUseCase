using Blazored.LocalStorage; 
using System.Net.Http.Json;
using Use_Case_Carte.Models.Auth;

namespace Use_Case_Carte.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _storage;

        public AuthService(HttpClient http, ILocalStorageService storage)
        {
            _http = http;
            _storage = storage;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            Console.WriteLine($"===========>> LoginRequest: {request.Username}, {request.Password}");
            Console.WriteLine($"===========>> adress de base : {_http.BaseAddress}");
            var response = await _http.PostAsJsonAsync("api/auth/login", request);

            Console.WriteLine($"---------->> LoginRequest: {request.Username}, {request.Password}");

            Console.WriteLine($"---------->> Response: {response.StatusCode}"); 

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            Console.WriteLine($"---------->> Response success? : {result.Success}"); 
            if (result?.Success == true)
            {
                await _storage.SetItemAsync("authToken", result.Token);
                await _storage.SetItemAsync("refreshToken", result.RefreshToken);
            }

            return result;
        }

        public async Task Logout()
        {
            await _storage.RemoveItemAsync("authToken");
            await _storage.RemoveItemAsync("refreshToken");
        }

        public async Task<string?> GetToken()
        {
            return await _storage.GetItemAsync<string>("authToken");
        }
    }
}
