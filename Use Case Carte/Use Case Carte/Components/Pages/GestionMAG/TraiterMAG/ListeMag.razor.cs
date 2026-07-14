using Microsoft.AspNetCore.Components;
using Use_Case_Carte.Components.Route;
using Use_Case_Carte.Models;
using Use_Case_Carte.Services;

namespace Use_Case_Carte.Components.Pages.GestionMAG.TraiterMAG;

public partial class ListeMag
{
    [Inject]
    public NavigationService NavigationService { get; set; } = default!;

    protected IEnumerable<TypeMag> typeMags = new List<TypeMag>();

    [Inject]
    public TypeMagService TypeMagService { get; set; } = default!;

    // utiliser un contructeur pour écouter à chaque fois la liste des mags traiter
 
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadProfils();
        }
    }

    private async Task LoadProfils()
    {
        typeMags = await TypeMagService.GetAllMags();

        StateHasChanged();
    }

    private void NouveauMag()
    {
        NavigationService.GoNouveauMAG();
    }

    public void GoToSyntheseMag(TypeMag typeMag)
    {
        Console.WriteLine("go to synthese");
        NavigationService.GoSyntheseMag(typeMag);
    }
}
