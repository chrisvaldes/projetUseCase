using Microsoft.AspNetCore.Components;
using Use_Case_Carte.Models;
using Use_Case_Carte.Services;

namespace Use_Case_Carte.Components.Pages.GestionMAG.DetailReclamation
{
    public partial class DetailReclamation : ComponentBase
    {
        [Inject]
        protected DetailReclamationService detailReclamationService {get; set;} = default!;

        protected CustomerBilling customerBilling {get; set;} = new();

        protected InputBilling inputBilling {get; set;} = new();

        public void GetCustomerBilling()
        {
            
        }

        public void Submit()
        {
            
        }
    }
}