using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Users.Application.DTO;
using Users.Application.Interfaces;

namespace Users.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize(Policy = "VOIR_USER")]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        //[Authorize(Policy = "VOIR_USER")]
        public async Task<IActionResult> GetById(Guid id) => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        //[Authorize(Policy = "CREER_USER")]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var response = await _service.CreateAsync(dto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("{id}")]
        //[Authorize(Policy = "MODIFIER_USER")]
        public async Task<IActionResult> Update(Guid id, UpdateUserDto dto)
        {
            var response = await _service.UpdateAsync(id, dto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        //[Authorize(Policy = "SUPPRIMER_USER")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _service.DeleteAsync(id);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string term)
        {
            var users = await _service.Search(term ?? "");
            return Ok(users);
        }
    }
}
