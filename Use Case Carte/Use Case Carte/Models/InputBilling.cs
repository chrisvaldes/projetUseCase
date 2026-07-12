namespace Use_Case_Carte.Models
{
    public class InputBilling
    {
        public DateTimeOffset Debut = DateTimeOffset.Now;
        public DateTimeOffset Fin = DateTimeOffset.Now;
        public string? NumeroCompte;
    }
}