using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Use_Case_Carte.Components.Layout;
using Use_Case_Carte.Models;

namespace Use_Case_Carte.Services
{
    public class NouveauMagService : BaseApiService
    {
        private SafeJs _safeJs;
        private readonly IJSRuntime _js;

        public NouveauMagService(
            SafeJs safeJs,
            HttpClient http,
            ILocalStorageService storage,
            IJSRuntime js
        )
            : base(http, storage)
        {
            _js = js;
            _safeJs = safeJs;
        }

        public async Task<ApiResponse<InputModel>> NouveauMag(InputModel request)
        {
            try
            {
                await _js.InvokeVoidAsync("toggleOnLoaderAndToast");

                await AddAuthHeader();

                using var form = new MultipartFormDataContent();

                form.Add(new StringContent(request.TypeMag ?? ""), "TypeMag");
                form.Add(new StringContent(request.StartPeriod.ToString("o")), "StartPeriod");
                form.Add(new StringContent(request.EndPeriod.ToString("o")), "EndPeriod");

                var tempFiles = new List<string>();

                async Task AddFileAsync(IBrowserFile file, string fieldName)
                {
                    if (file == null)
                        return;

                    var tempPath = Path.GetTempFileName();
                    tempFiles.Add(tempPath);

                    await using (var browserStream = file.OpenReadStream(500 * 1024 * 1024))
                    await using (var fileStream = File.Create(tempPath))
                    {
                        await browserStream.CopyToAsync(fileStream);
                    }

                    var streamContent = new StreamContent(File.OpenRead(tempPath));
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue(
                        string.IsNullOrWhiteSpace(file.ContentType)
                            ? "application/octet-stream"
                            : file.ContentType
                    );
                    form.Add(streamContent, fieldName, file.Name);
                }

                // Cette boucle vide d'abord TOUS les streams navigateur avant de toucher au réseau HTTP
                await AddFileAsync(request.Apprint, "Apprint");
                await AddFileAsync(request.OpenAccount, "OpenAccount");
                await AddFileAsync(request.ActiveAccount, "ActiveAccount");
                await AddFileAsync(request.ActivePackage, "ActivePackage");
                await AddFileAsync(request.CtxAccount, "CtxAccount");
                await AddFileAsync(request.DateLastSouPackEchu, "DateLastSouPackEchu");
                await AddFileAsync(
                    request.AccountHisDebiteByRedevCard,
                    "AccountHisDebiteByRedevCard"
                );

                var response = await _http.PostAsync("api/nouveauMag", form);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"STATUS API : {response.StatusCode}");
                Console.WriteLine($"RESPONSE API : {content}");

                // nettoyage des fichiers temporaires
                foreach (var path in tempFiles)
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch
                    { /* ignore */
                    }
                }

                if (!response.IsSuccessStatusCode)
                {
                    await _js.InvokeVoidAsync("toggleOffLoaderAndToast");
                    return null;
                }

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<InputModel>>();
                await _js.InvokeVoidAsync("toggleOffLoaderAndToast");
                return result!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await _js.InvokeVoidAsync("toggleOffLoaderAndToast");
                return null;
            }
        }
    }
}
