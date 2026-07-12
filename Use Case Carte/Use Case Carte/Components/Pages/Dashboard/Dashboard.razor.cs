using Microsoft.AspNetCore.Components;
using Use_Case_Carte.Models;

namespace Use_Case_Carte.Components.Pages.Dashboard
{
    public partial class Dashboard : ComponentBase
    {
 
        [Inject]
        private NavigationManager Navigation { get; set; } = default!;

        protected DashboardSynthese dashboardSynthese = new();
    }
}