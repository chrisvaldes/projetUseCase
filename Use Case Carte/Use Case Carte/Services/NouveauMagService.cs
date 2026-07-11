using Blazored.LocalStorage;
using Microsoft.JSInterop;
using Use_Case_Carte.Components.Layout;
using Use_Case_Carte.Models;

namespace Use_Case_Carte.Services
{

    public class NouveauMagService : BaseApiService
    {
        private SafeJs _safeJs;
        private readonly IJSRuntime _js;

        public NouveauMagService(SafeJs safeJs, HttpClient http,
                ILocalStorageService storage,
                IJSRuntime js) : base(http, storage)
        {
            _js = js;
            _safeJs = safeJs;
        }
        public async Task<ApiResponse<InputModel>> NouveauMag(InputModel request)
        {

            try
            {
                await _safeJs.SafeJsUtilities("toggleOnLoaderAndToast");

                await AddAuthHeader();

                var response = await _http.PostAsJsonAsync("api/nouveauMag", request);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
        }
    }
}