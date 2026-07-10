using Microsoft.AspNetCore.Components;
using Use_Case_Carte.Components.Route;
using Use_Case_Carte.Models;

namespace Use_Case_Carte.Components.Pages.GestionMAG.TraiterMAG;

public partial class TraiterMag : ComponentBase
{
    [Inject]
    NavigationService NavigationService {get; set;} = default!;

    protected InputModel inputModel {get; set;} = new();

    public void OnCancel()
    {
        NavigationService.GoGestionMAG();
    }
    public void Traiter()
    {
        NavigationService.GoGestionMAG();
    }

}