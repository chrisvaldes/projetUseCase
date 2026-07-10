using System;
using Microsoft.AspNetCore.Components;
using Use_Case_Carte.Models;

namespace Use_Case_Carte.Components.Route;

public class NavigationService
{
    private readonly NavigationManager _navigation;

    public NavigationService(NavigationManager navigation)
    {
        _navigation = navigation;
    }

    public void GoProfil()
    {
        _navigation.NavigateTo(Route.Profil);
    }

    public void GoCreerProfil()
    {
        _navigation.NavigateTo(Route.CreateProfil);
    }

    public void GoModifierProfil(ProfilModel profilModel)
    {
        _navigation.NavigateTo($"{Route.ModifierProfil}/{profilModel.Id}");
    }

    public void GoSupprimerProfil()
    {
        _navigation.NavigateTo(Route.SupprimerProfil);
    }

    public void GoDashboard()
    {
        _navigation.NavigateTo(Route.Dashboard);
    }

    public void GoGestionMAG()
    {
        _navigation.NavigateTo(Route.GestionMAG);
    }

    public void GoNouveauMAG()
    {
        _navigation.NavigateTo(Route.NouveauMag);
    }

    public void DetailFacturation()
    {
        _navigation.NavigateTo(Route.DetailFacturation);
    }

    //public void GoCustomers()
    //{
    //    _navigation.NavigateTo(Route.Customers);
    //}

    //public void GoCreateCustomer()
    //{
    //    _navigation.NavigateTo(Route.CreateCustomer);
    //}

    //public void GoCustomer(int id)
    //{
    //    _navigation.NavigateTo(
    //        string.Format(Route.CustomerDetails, id));
    //}

    //public void GoEditCustomer(int id)
    //{
    //    _navigation.NavigateTo(
    //        string.Format(Route.EditCustomer, id));
    //}
}
