using Microsoft.AspNetCore.Components;

namespace Use_Case_Carte.Components.Shared;

public partial class SharedHeader : ComponentBase
{
    [Parameter]
    public string CurrentPage { get; set; } = string.Empty;

    [Parameter]
    public string PreviousPage { get; set; } = string.Empty;
}