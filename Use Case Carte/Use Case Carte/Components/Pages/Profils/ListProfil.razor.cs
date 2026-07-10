using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Use_Case_Carte.Components.Route;
using Use_Case_Carte.Models;
using Use_Case_Carte.Services;

namespace Use_Case_Carte.Components.Pages.Profils;

public partial class ListProfil : ComponentBase
{
    [Inject]
    protected ProfilService ProfilService { get; set; } = default!;

    [Inject]
    private NavigationService NavigationService { get; set; } = default!;
    public IEnumerable<ProfilModel> profils = new List<ProfilModel>();

    [Inject]
    private IJSRuntime JS { get; set; } = default!;

    public string searchQuery = "";

    // protected override async Task OnInitializedAsync(){

    // }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadProfils();
        }
    }

    private async Task LoadProfils()
    {
        profils = await ProfilService.GetAllProfils();
        StateHasChanged();
    }

    public ProfilModel SelectedProfil = new();

    private async void EditProfil(ProfilModel profil)
    {
        Console.WriteLine(
            $"information {profil.Email}, {profil.IsDeleted}, {profil.Status}, {profil.UserName}, {profil.Userag}"
        );
        SelectedProfil = new ProfilModel
        {
            Id = profil.Id,
            UserName = profil.UserName,
            Userag = profil.Userag,
            Email = profil.Email,
            TypeProfile = profil.TypeProfile,
            Status = profil.Status,
        };

        await JS.InvokeVoidAsync("showUpdateProfileModal");
    }

    private void CreateProfil()
    {
        NavigationService.GoCreerProfil();
    }

    private void ModifierProfil(ProfilModel profil)
    {
        NavigationService.GoModifierProfil(profil);
    }
}
