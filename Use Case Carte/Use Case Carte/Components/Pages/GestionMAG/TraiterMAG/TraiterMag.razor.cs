using Microsoft.AspNetCore.Components;
using Use_Case_Carte.Components.Route;
using Use_Case_Carte.Models;
using Use_Case_Carte.Services;

namespace Use_Case_Carte.Components.Pages.GestionMAG.TraiterMAG;

public partial class TraiterMag : ComponentBase
{
    [Inject]
    NavigationService NavigationService {get; set;} = default!;

    [Inject]
    NouveauMagService NouveauMagService {get; set;} = default!;

    private readonly ILogger<TraiterMag> _logger;

    protected InputModel inputModel {get; set;} = new();

    public TraiterMag(ILogger<TraiterMag> logger)
    {
        _logger = logger;
    }
    public void OnCancel()
    {
        NavigationService.GoGestionMAG();
    }
    public async void Traiter()
    {
        Console.WriteLine($"data : {inputModel.TypeMag}");
        var resultRequest = await NouveauMagService.NouveauMag(inputModel);
        _logger.LogInformation($"data response : {resultRequest}");
        // NavigationService.GoGestionMAG();
    }

}