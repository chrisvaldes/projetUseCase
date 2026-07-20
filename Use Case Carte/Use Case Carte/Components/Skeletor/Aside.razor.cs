using Microsoft.AspNetCore.Components;

namespace Use_Case_Carte.Components.Skeletor;

public partial class Aside : ComponentBase
{
    [Inject]
    protected PermissionServiceAuth PermissionServiceAuth { get; set; } = default!;

    protected HashSet<string> Permissions = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Permissions = await PermissionServiceAuth.GetPermissions();

            StateHasChanged();
        }
    }

    protected bool HasPermission(string permission)
        => Permissions.Contains(permission);

}