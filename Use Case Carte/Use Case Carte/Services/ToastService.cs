using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Use_Case_Carte.Models;

namespace Use_Case_Carte.Services
{
    public class ToastService
    {
        public event Action<ToastModel>? OnShow;

        public void ShowSuccess(string message, string title = "Succès")
        {
            Console.WriteLine("affichage du toast de succès");
            Show(message, title, ToastType.Success);
        }

        public void ShowError(string message, string title = "Erreur")
        {
            Console.WriteLine("affichage du toast d'erreur.");
            Show(message, title, ToastType.Error);
        }

        public void ShowWarning(string message, string title = "Attention")
        {
            Console.WriteLine("affichage du toast d'attention.");
            Show(message, title, ToastType.Warning);
        }

        public void ShowInfo(string message, string title = "Information")
        {
            Console.WriteLine("affichage du toast d'information."); 
            Show(message, title, ToastType.Info);
        }

        private void Show(string message, string title, ToastType type)
        {
            
            OnShow?.Invoke(new ToastModel
            {
                Message = message,
                Title = title,
                Type = type
            });
        }
    }
}