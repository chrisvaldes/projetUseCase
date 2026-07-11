

using API.Application.DTO;
using API.Application.Services.IServices;
using API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SYSGES_MAGs.Helpers;

namespace API.Controllers
{
    [ApiController]
    [Route("api")]
    public class NouveauMagController : ControllerBase
    {
        // private readonly IProfilService _profilService;
        private readonly ILogger<NouveauMagController> _logger;

        private readonly IMagProcessingService _magProcessingService;
        private readonly IBkmvtiService _bkmvtiService;
        private readonly MagProcessingHelper _magProcessingHelper;

        public NouveauMagController(IMagProcessingService magProcessingService, IBkmvtiService bkmvtiService, ILogger<NouveauMagController> logger, MagProcessingHelper magProcessingHelper)
        {
            _magProcessingService = magProcessingService;
            _bkmvtiService = bkmvtiService;
            _magProcessingHelper = magProcessingHelper;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("nouveauMag")]
        [RequestSizeLimit(500 * 1024 * 1024)]
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        public async Task<IActionResult> Nouveau([FromForm] InputModel inputModel)
        {
            _logger.LogInformation($"========>>>>>>>>type mag : {inputModel.TypeMag} ");
            _logger.LogInformation($"========>>>>>>>>type mag : {inputModel.StartPeriod} ");
            //Vérification des paramètres obligatoires
            if (inputModel.Apprint == null || inputModel.Apprint.Length == 0 ||
                inputModel.OpenAccount == null || inputModel.OpenAccount.Length == 0 ||
                inputModel.ActiveAccount == null || inputModel.ActiveAccount.Length == 0 ||
                inputModel.DateLastSouPackEchu == null || inputModel.DateLastSouPackEchu.Length == 0 ||
                inputModel.ActivePackage == null || inputModel.ActivePackage.Length == 0 ||
                inputModel.AccountHisDebiteByRedevCard == null || inputModel.AccountHisDebiteByRedevCard.Length == 0 ||
                string.IsNullOrEmpty(inputModel.TypeMag) || inputModel.CtxAccount.Length == 0 ||
                inputModel.StartPeriod == default || inputModel.EndPeriod == default)
            {
                _logger.LogInformation("Tous les champs sont obligatoires");
                return Ok(ApiResponse<InputModel>.Fail("Tous les champs du formulaire sont requis"));
            }
            try
            {
                // Appel du service async correctement
                var result = await _magProcessingService.ProcessTxtExcelFiles(inputModel);
                _logger.LogInformation($"========>>>>>>>>type mag : {inputModel.TypeMag} ");
                if (!result.Success)
                {
                    _logger.LogWarning("Erreur lors du traitement des fichiers : " + result.Message);
                    return Ok(result);
                }

                _logger.LogInformation("Tous les fichiers ont été traités avec succès");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur inattendue lors du traitement des fichiers MAG");
                return Ok(ApiResponse<bool>.Fail("Une erreur est survenue lors du traitement des fichiers. " + ex.Message));
            }
        }

        public async Task<IActionResult> Synthese(Guid id)
        {
            var result = await _magProcessingHelper.GetTypeMagWithSynthese(id);
            // check le type de retour de la synthèse
            return Ok(result);
        }

        public async Task<IActionResult> DownloadBkmvti([FromBody] DownloadRequest request)
        {
            if (request.TypeMag == Guid.Empty)
                return Ok(ApiResponse<bool>.Fail("Identifiant du MAG invalide!!!"));

            var bkmvtis = await _bkmvtiService.BkmvtisByMagType(request.TypeMag);


            await _magProcessingHelper.IsDownloadAsync(request.TypeMag);

            if (bkmvtis == null || !bkmvtis.Any())
                return Ok(ApiResponse<bool>.SuccessResponse(true, "Le fichier ne contient aucun manque à gagner à récupérer."));

            var fileBytes =
                await _magProcessingHelper.GenerateFile(bkmvtis);

            var fileName =
                $"BKMVTI_{DateTime.Now:yyyyMMddHHmmss}";

            return File(
                fileBytes,
                "application/octet-stream",
                fileName);
        }

        [HttpPost]
        public async Task<IActionResult> TelechargerCarteARegulerExcel([FromBody] DownloadRequest request)
        {
            try
            {

                var result = await _bkmvtiService.DashboardResult();

                if (request.TypeMag == Guid.Empty)
                    return Ok(ApiResponse<bool>.Fail("Identifiant du MAG invalide!!!"));


                var carteAReguler = await _bkmvtiService.CarteAReguler(request.TypeMag);
                // Génération du fichier Excel
                byte[] fichier = _magProcessingHelper.TxtToExcel(
                    carteAReguler
                );

                string nomFichier =
                    $"BKMBTI_{DateTime.Today}.xlsx";

                return File(
                    fichier,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    nomFichier
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Erreur lors de la génération du fichier",
                    Error = ex.Message
                });
            }
        }

    }

}

