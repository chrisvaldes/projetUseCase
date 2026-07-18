    using Blazored.LocalStorage;
using Microsoft.JSInterop;
using Use_Case_Carte.Models; 
namespace Use_Case_Carte.Services
{
    public class RoleService : BaseApiService
    {
        private readonly ILogger<RoleService> _logger;

        public RoleService(
            HttpClient http,
            ILocalStorageService storage,
            ILogger<RoleService> logger
        )
            : base(http, storage)
        {
            _logger = logger;
        }

        public async Task<ApiResponse<string>> CreateAsync(CreateRoleDto dto)
        {
            await AddAuthHeader();

            try
            {
                var response = await _http.PostAsJsonAsync("api/Role", dto);

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();

                if (!response.IsSuccessStatusCode)
                {
                    var errorText = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Role CREATE failed: {response.StatusCode} - {errorText}");
                    return result!;
                }

                return result!;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erreur CreateAsync (Role): {ex.Message}");
                throw new Exception($"Erreur : {ex.Message}");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(Guid roleId, CreateRoleDto dto)
        {
            await AddAuthHeader();

            try
            {
                var response = await _http.PutAsJsonAsync($"api/Role/{roleId}", dto);

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();

                if (!response.IsSuccessStatusCode)
                {
                    var errorText = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Role UPDATE failed: {response.StatusCode} - {errorText}");
                    return result!;
                }

                return result!;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erreur UpdateAsync (Role): {ex.Message}");
                throw new Exception($"Erreur : {ex.Message}");
            }
        }
    }
}
