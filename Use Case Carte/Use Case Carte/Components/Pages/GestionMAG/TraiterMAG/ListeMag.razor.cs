using Microsoft.AspNetCore.Components;
using Use_Case_Carte.Components.Route;

namespace Use_Case_Carte.Components.Pages.GestionMAG.TraiterMAG;

public partial class ListeMag 
{
    [Inject]
    public NavigationService NavigationService { get; set; } = default!;

    private void NouveauMag()
    {
        NavigationService.GoNouveauMAG();
    }
}