using Microsoft.AspNetCore.Components;
using Use_Case_Carte.Components.Route;
using Use_Case_Carte.Models;

namespace Use_Case_Carte.Components.Pages.GestionMAG.TraiterMAG;

public partial class ListeMag 
{
    [Inject]
    public NavigationService NavigationService { get; set; } = default!;

    protected List<TypeMag> typeMags = new();

    // utiliser un contructeur pour écouter à chaque fois la liste des mags traiter
    private void NouveauMag()
    {
        NavigationService.GoNouveauMAG();
    }
}