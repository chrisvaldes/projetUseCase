namespace Use_Case_Carte.Models;

public class InputModel
{
    public IFormFile Apprint {get; set;} = default!;
    public IFormFile OpenAccount {get; set;} = default!;
    public IFormFile ActiveAccount {get; set;} = default!;
    public IFormFile DateLastSouPackEchu {get; set;} = default!;
    public IFormFile ActivePackage {get; set;} = default!;
    public IFormFile CtxAccount {get; set;} = default!;
    public IFormFile AccountHisDebiteByRedevCard {get; set;} = default!;
    public string TypeMag {get; set;} = default!;
    public DateTime? StartPeriod {get; set;} = default!;
    public DateTime? EndPeriod {get; set;} = default!;
}