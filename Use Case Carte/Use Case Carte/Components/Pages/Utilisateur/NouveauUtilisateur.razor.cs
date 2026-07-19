using System.Text.Json; // ajouté
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Use_Case_Carte.Components.Route;
using Use_Case_Carte.Models;
using Use_Case_Carte.Services;

namespace Use_Case_Carte.Components.Pages.Utilisateur;

public partial class NouveauUtilisateur : ComponentBase
{
    [Inject]
    private NavigationService NavigationService { get; set; } = default!;

    [Inject]
    protected UserService UserService { get; set; } = default!;

    [Inject]
    protected RoleService RoleService { get; set; } = default!;

    [Parameter]
    public int? Id { get; set; }

    private UserDto UserDto = new();

    // private bool IsUpdate => Id.HasValue;

    // [Parameter]
    // public EventCallback OnSaved { get; set; }

    [Inject]
    public ToastService ToastService { get; set; } = default!;

    [Inject]
    private IJSRuntime JS { get; set; } = default!;


    public IEnumerable<RoleDto> RoleDto =  new List<RoleDto>();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {

            await LoadRoles();
            // await JS.InvokeVoidAsync("initProfileModals");
        }
    }

    private async Task LoadRoles()
    {
        RoleDto = await RoleService.GetAllRoles();

        StateHasChanged();
    }
    private async Task OnSaveUser()
    {
        var resp = await UserService.Save(UserDto);

        if (resp?.Success == true)
        {
            UserDto = new UserDto();

            // 1. Afficher le toast AVANT toute navigation
            ToastService.ShowSuccess(resp.Message);

            // 2. Laisser le temps au toast de s'afficher/être visible
            await Task.Delay(1500);

            // 3. Naviguer seulement après
            NavigationService.GoProfil();

            StateHasChanged();
        }
        else
        {
            await JS.InvokeVoidAsync("showToast", "message", "warning");
            ToastService.ShowError(resp!.Message);
            // 2. Laisser le temps au toast de s'afficher/être visible
            await Task.Delay(2000);
        }
    }

    private async Task OnCancel()
    {
        NavigationService.GoProfil();
        await Task.CompletedTask;
    }

    private async Task OnSaveUtilisateur()
    {
        NavigationService.GoProfil();
        await Task.CompletedTask;
    }
}
