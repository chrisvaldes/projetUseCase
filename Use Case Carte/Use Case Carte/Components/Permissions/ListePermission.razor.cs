 
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Use_Case_Carte.Components.Route;
using Use_Case_Carte.Models;
using Use_Case_Carte.Services;

namespace Use_Case_Carte.Components.Permissions
{
    public partial class ListePermission : ComponentBase
    {
        private readonly ILogger<ListePermission> _logger;

        public ListePermission(ILogger<ListePermission> logger)
        {
            _logger = logger;
        }

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
                await JS.InvokeVoidAsync("toggleOnLoaderAndToast");

                await LoadProfils();

                await JS.InvokeVoidAsync("toggleOffLoaderAndToast");
            }
        }

        private async Task LoadProfils()
        {
            profils = await ProfilService.GetAllProfils();

            StateHasChanged();
        }

        public ProfilModel SelectedProfil = new();

        private void ModifierProfil(ProfilModel profil)
        {
            NavigationService.GoModifierProfil(profil);
        }

        public async Task NouvellePermission()
        { 
            NavigationService.GoNouvellePermission();
        }
    }
}
