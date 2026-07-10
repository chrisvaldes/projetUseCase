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

    [Inject]
    public ToastService ToastService { get; set; } = default;

    protected LoginRequest loginRequest = new();

    private bool IsLoading = false;
    private string Message = string.Empty;

    protected async Task LoginAsync()
    {
        try
        {
            await JS.InvokeVoidAsync("toggleOnLoaderAndToast");
            await Task.Delay(1500);
            var result = await AuthService.Login(loginRequest);
            ToastService.ShowSuccess(result.Message);
            Console.WriteLine("---------->> Response success? : " + result.Success);

            if (result.Success)
            {
                await JS.InvokeVoidAsync("toggleOffLoaderAndToast");
                NavigationService.GoProfil();
            }
            else
            {
                Message = result?.Message ?? "Erreur de connexion";
            }
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }
}