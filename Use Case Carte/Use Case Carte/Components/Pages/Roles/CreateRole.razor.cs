
// using Microsoft.AspNetCore.Components;
// using Microsoft.JSInterop;
// using Use_Case_Carte.Components.Permissions;
// using Use_Case_Carte.Components.Route;
// using Use_Case_Carte.Models;
// using Use_Case_Carte.Services;

// namespace Use_Case_Carte.Components.Pages.Roles
// {
//     public partial class CreateRole : ComponentBase, IDisposable
//     {
//         private readonly ILogger<ListePermission> _logger;

//         public CreateRole(ILogger<ListePermission> logger)
//         {
//             _logger = logger;
//         }

//         [Inject]
//         protected ProfilService ProfilService { get; set; } = default!;

//         [Inject]
//         private NavigationService NavigationService { get; set; } = default!;
//         public IEnumerable<ProfilModel> profils = new List<ProfilModel>();

//         [Inject]
//         private IJSRuntime JS { get; set; } = default!;

//         public string searchQuery = "";

//         // protected override async Task OnInitializedAsync(){

//         // }

//         protected override async Task OnAfterRenderAsync(bool firstRender)
//         {
//             if (firstRender)
//             {
//                 await JS.InvokeVoidAsync("toggleOnLoaderAndToast");

 
//                 // On construit l'arbre après le premier render, une fois les données chargées
//                 await RenderPermissionTreeAsync();

//                 await JS.InvokeVoidAsync("toggleOffLoaderAndToast");
//             }
//         }

 
//         public async Task NouveauRole()
//         {
//             NavigationService.GoCreerRole();
//         }




//         [Inject]
//         protected PermissionService PermissionService { get; set; } = default!;

//         [Inject]
//         public ToastService ToastService { get; set; } = default!;
//         protected List<PermissionTreeDto> permissionsTree = new();

//         protected override async Task OnInitializedAsync()
//         {
//             // await LoadProfilsAsync();
//             await LoadPermissionsAsync();
//         }



//         // private async Task LoadProfilsAsync()
//         // {
//         //     // profils = await ProfilService.GetAllAsync();
//         //     profils ??= new List<ProfilModel>();
//         // }

//         private async Task LoadPermissionsAsync()
//         {
//             permissionsTree = await PermissionService.GetAllAsync();

//             if (!permissionsTree.Any())
//             {
//                 Console.WriteLine("---------->> Aucune permission récupérée");
//             }
//         }

//         private DotNetObjectReference<CreateRole>? _dotNetRef;

//         private async Task RenderPermissionTreeAsync()
//         {
//             if (permissionsTree.Any())
//             {
//                 _dotNetRef = DotNetObjectReference.Create(this);
//                 await JS.InvokeVoidAsync("initPermissionTree", "permissionTreeContainer", permissionsTree, _dotNetRef);
//             }
//         }

//         [JSInvokable]
//         public void OnPermissionsChecked(string[] selectedIds)
//         {
//             Console.WriteLine($"Permissions cochées : {string.Join(", ", selectedIds)}");
//             // Traite les IDs cochés ici, ex: stocker dans une liste, appeler un service, etc.
//         }

//         public void Dispose()
//         {
//             _dotNetRef?.Dispose();
//         }

//     }
// }

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Use_Case_Carte.Components.Route;
using Use_Case_Carte.Models;
using Use_Case_Carte.Services;

namespace Use_Case_Carte.Components.Pages.Roles;

public partial class CreateRole : ComponentBase, IDisposable
{
    private readonly ILogger<CreateRole> _logger;

    public CreateRole(ILogger<CreateRole> logger)
    {
        _logger = logger;
    }

    [Inject]
    private PermissionService PermissionService { get; set; } = default!;

    [Inject]
    private RoleService RoleService { get; set; } = default!;

    [Inject]
    private NavigationService NavigationService { get; set; } = default!;

    [Inject]
    private ToastService ToastService { get; set; } = default!;

    [Inject]
    private IJSRuntime JS { get; set; } = default!;

    protected CreateRoleDto roleModel = new();

    protected List<PermissionTreeDto> permissionsTree = new();

    protected bool isSaving;

    private DotNetObjectReference<CreateRole>? _dotNetRef;

    // protected override async Task OnInitializedAsync()
    // {
    //     permissionsTree = await PermissionService.GetAllAsync();

    //     _logger.LogInformation("{Count} permissions récupérées", permissionsTree.Count);
    // }
    protected override async Task OnInitializedAsync()
{
    permissionsTree = await PermissionService.GetAllAsync();

    _logger.LogInformation("{Count} permissions récupérées", permissionsTree.Count);

    await InvokeAsync(StateHasChanged);
}

    // protected override async Task OnAfterRenderAsync(bool firstRender)
    // {
    //     if (firstRender && permissionsTree.Any())
    //     {
    //         _dotNetRef = DotNetObjectReference.Create(this);

    //         await JS.InvokeVoidAsync(
    //             "initPermissionTree",
    //             "permissionTreeContainer",
    //             permissionsTree,
    //             _dotNetRef);
    //     }
    // }
    private bool _treeInitialized;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!_treeInitialized && permissionsTree.Any())
        {
            _treeInitialized = true;

            _dotNetRef = DotNetObjectReference.Create(this);

            await JS.InvokeVoidAsync(
                "initPermissionTree",
                "permissionTreeContainer",
                permissionsTree,
                _dotNetRef);
        }
    }

    protected async Task OnValiderPermissions()
    {
        isSaving = true;

        try
        {
            await JS.InvokeVoidAsync("toggleOnLoaderAndToast");

            var checkedIds = await JS.InvokeAsync<string[]>(
                "getCheckedPermissions",
                "permissionTreeContainer");

            roleModel.Permissions = checkedIds
                .Select(long.Parse)
                .ToList();

            if (!roleModel.Permissions.Any())
            {
                ToastService.ShowError("Sélectionnez au moins une permission.");
                return;
            }

            var response = await RoleService.CreateAsync(roleModel);

            if (response.Success)
            {
                ToastService.ShowSuccess(response.Message);
                NavigationService.GoRole();
            }
            else
            {
                ToastService.ShowError(response.Message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création du rôle");
            ToastService.ShowError("Une erreur est survenue.");
        }
        finally
        {
            isSaving = false;
            await JS.InvokeVoidAsync("toggleOffLoaderAndToast");
            StateHasChanged();
        }
    }

    protected void Annuler()
    {
        NavigationService.GoRole();
    }

    public void Dispose()
    {
        _dotNetRef?.Dispose();
    }
}