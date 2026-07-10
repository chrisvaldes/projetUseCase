using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Use_Case_Carte.Components.Route;
using Use_Case_Carte.Models;
using Use_Case_Carte.Services;

namespace Use_Case_Carte.Components.Pages.Profils;

public partial class UpdateProfil
{
    [Inject]
    public ProfilService ProfilService { get; set; } = default!;

    [Inject]
    public NavigationService NavigationService { get; set; } = default!;

    [Inject]
    public IJSRuntime JS { get; set; } = default!;

    [Parameter]
    public Guid Id { get; set; }

    private ProfilModel profilModel = new();

    private bool loaded = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !loaded)
        {
            loaded = true;

            Console.WriteLine($"Id reçu : {Id}");

            var profil = await ProfilService.GetById(Id);
 
            if (profil != null)
            {
                profilModel = profil;
                StateHasChanged();
            }
        }
    }

    private async Task SaveProfil()
    {
        var resp = await ProfilService.Save(profilModel);

        if (resp?.Success == true)
        {
            profilModel = new ProfilModel();

            // retour liste si besoin
        }
    }

    private void onCancel()
    {
        NavigationService.GoProfil();
    }
}
