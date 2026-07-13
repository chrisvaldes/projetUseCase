using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Use_Case_Carte.Components.Route;
using Use_Case_Carte.Models;
using Use_Case_Carte.Services;

namespace Use_Case_Carte.Components.Pages.GestionMAG.TraiterMAG;

public partial class TraiterMag : ComponentBase
{
    [Inject]
    NavigationService NavigationService { get; set; } = default!;

    [Inject]
    NouveauMagService NouveauMagService { get; set; } = default!;
    [Inject]
    public IJSRuntime JS { get; set; } = default!;

    private readonly ILogger<TraiterMag> _logger;

    protected InputModel inputModel { get; set; } = new();

    public TraiterMag(ILogger<TraiterMag> logger)
    {
        _logger = logger;
    }

    public void OnCancel()
    {
        NavigationService.GoGestionMAG();
    }

    public async Task Traiter()
    {
        await JS.InvokeVoidAsync("toggleOnLoaderAndToast");
        Console.WriteLine($"data : {inputModel.TypeMag}");
        try
        {
            var resultRequest = await NouveauMagService.NouveauMag(inputModel);
            if (resultRequest == null || !resultRequest.Success)
            {
                // TODO: afficher un toast d'erreur avec resultRequest?.Message
                await JS.InvokeVoidAsync("toggleOffLoaderAndToast");
                return;
            }
            // succès -> navigation ou notification
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur inattendue : " + ex.Message);
        }
    }

    private async Task OnFileSelected(InputFileChangeEventArgs e, string propertyName)
    {
        var file = e.File;

        switch (propertyName)
        {
            case nameof(inputModel.Apprint):
                inputModel.Apprint = file;
                break;

            case nameof(inputModel.OpenAccount):
                inputModel.OpenAccount = file;
                break;

            case nameof(inputModel.ActiveAccount):
                inputModel.ActiveAccount = file;
                break;

            case nameof(inputModel.ActivePackage):
                inputModel.ActivePackage = file;
                break;

            case nameof(inputModel.CtxAccount):
                inputModel.CtxAccount = file;
                break;

            case nameof(inputModel.DateLastSouPackEchu):
                inputModel.DateLastSouPackEchu = file;
                break;

            case nameof(inputModel.AccountHisDebiteByRedevCard):
                inputModel.AccountHisDebiteByRedevCard = file;
                break;
        }
    }
}
