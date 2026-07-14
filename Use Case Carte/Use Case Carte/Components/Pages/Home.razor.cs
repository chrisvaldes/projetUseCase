using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Use_Case_Carte.Services;
using Use_Case_Carte.Models.Auth;
using Use_Case_Carte.Components.Route;
namespace Use_Case_Carte.Components.Pages;


public partial class Home : ComponentBase
{
    [Inject]
    private AuthService AuthService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Inject]
    private NavigationService NavigationService { get; set; } = default!;

    [Inject]
    private IJSRuntime JS { get; set; } = default!;

    protected LoginRequest loginRequest = new();


    protected async Task LoginAsync()
    {
        try
        {
            await JS.InvokeVoidAsync("toggleOnLoaderAndToast");
            
            Console.WriteLine($"LoginRequest : {loginRequest.Username}, {loginRequest.Password}");

            var result = await AuthService.Login(loginRequest);

            Console.WriteLine("---------->> Response success? : " + result.Success);

            if (result.Success)
            {
                NavigationService.GoProfil();

                // Quick fix : forcer un rechargement complet pour réinitialiser les scripts client (sidebar, simplebar, defaultmenu...)
                // Remarque : remplacez par une réinitialisation JS propre si vous implémentez la fonction reinitUi (recommandé ci‑dessous).
                await JS.InvokeVoidAsync("eval", "setTimeout(function(){ location.reload(); }, 10);");
            }
            else
            {
                // Message = result?.Message ?? "Erreur de connexion";
            }
        }
        catch (Exception ex)
        {
            // Message = ex.Message;
            await JS.InvokeVoidAsync("toggleOffLoaderAndToast");
        }
        finally
        {
            await JS.InvokeVoidAsync("toggleOffLoaderAndToast");
        }
    }
}