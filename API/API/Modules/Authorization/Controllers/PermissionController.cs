using Authorization.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        // GET ALL
        [HttpGet]
        //[Authorize(Policy = "VOIR_ROLE")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _permissionService.GetAllAsync();

            return Ok(response);
        }
    }
}
