using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Use_Case_Carte.Components.Layout;
using Use_Case_Carte.Models;

namespace Use_Case_Carte.Services
{

    public class DetailReclamationService : BaseApiService
    {
        private SafeJs _safeJs;
        private readonly IJSRuntime _js;

        public DetailReclamationService(SafeJs safeJs, HttpClient http,
                ILocalStorageService storage,
                IJSRuntime js) : base(http, storage)
        {
            _js = js;
            _safeJs = safeJs;
        }
    }
}