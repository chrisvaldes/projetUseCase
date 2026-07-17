// using Microsoft.AspNetCore.Components;
// using Microsoft.JSInterop;
// using Use_Case_Carte.Services;
// using Use_Case_Carte.Models.Auth;
// using Use_Case_Carte.Components.Route;
// namespace Use_Case_Carte.Components.Pages;


// public partial class Home : ComponentBase
// {

//     [Inject]
//     public ToastService ToastService { get; set; } = default!;

//     [Inject]
//     private AuthService AuthService { get; set; } = default!;

//     [Inject]
//     private NavigationManager Navigation { get; set; } = default!;

//     [Inject]
//     private NavigationService NavigationService { get; set; } = default!;

//     [Inject]
//     private IJSRuntime JS { get; set; } = default!;

//     protected LoginRequest loginRequest = new();


//     protected async Task LoginAsync()
//     {
//         try
//         {

//             Console.WriteLine($"LoginRequest : {loginRequest.Username}, {loginRequest.Password}");

//             var result = await AuthService.Login(loginRequest);

//             Console.WriteLine("---------->> Response success? : " + result.Success);

//             if (result.Success)
//             {

//                 NavigationService.GoProfil(); 

//                 await JS.InvokeVoidAsync("eval", "setTimeout(function(){ location.reload(); }, 10);");
//                                 Console.WriteLine($"message de succès : {result.Message}");
//                 ToastService.ShowSuccess(result.Message);
//                 await Task.Delay(1500);
//             }
//             else
//             {
//                 // Message = result?.Message ?? "Erreur de connexion";
//                 NavigationService.GoLogin();
//             }
//         }
//         catch (Exception ex)
//         {
//             // Message = ex.Message;
//             await JS.InvokeVoidAsync("toggleOffLoaderAndToast");
//         }
//         finally
//         {
//             await JS.InvokeVoidAsync("toggleOffLoaderAndToast");
//         }
//     }
// }
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Use_Case_Carte.Services;
using Use_Case_Carte.Models.Auth;
using Use_Case_Carte.Components.Route;

namespace Use_Case_Carte.Components.Pages;

public partial class Home : ComponentBase
{
    [Inject]
    public ToastService ToastService { get; set; } = default!;

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
            Console.WriteLine($"LoginRequest : {loginRequest.Username}, {loginRequest.Password}");

            // AuthService.Login gère déjà le toggle on/off du loader en interne
            var result = await AuthService.Login(loginRequest);

            if (result.Success)
            { 
                ToastService.ShowSuccess(result.Message);

                // On laisse le temps au toast de s'afficher AVANT de recharger
                await Task.Delay(1500);

                NavigationService.GoProfil();

                // Reload après le délai, pas 10ms plus tard
                await JS.InvokeVoidAsync("eval", "location.reload();");
            }
            else
            {
                ToastService.ShowError(result.Message ?? "Erreur de connexion");
                NavigationService.GoLogin();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---------->> Exception LoginAsync: {ex.Message}");
            ToastService.ShowError("Une erreur est survenue");
        }
        // Plus besoin de finally ici : AuthService.Login gère déjà toggleOffLoaderAndToast
    }
}