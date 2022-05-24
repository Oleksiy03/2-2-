using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistrationCertificateAPI.Data;
using RegistrationCertificateAPI.Models;
using RegistrationCertificateAPI.Services;

namespace RegistrationCertificateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationCertificateController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ICertificateService _certificateService;

        public RegistrationCertificateController(ICertificateService service, UserManager<User> userManager)
        {
            _userManager = userManager;
            _certificateService = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<RegistrationCertificateModel>> GetAllCertificates(
            string? sort_by = null, string? sort_type = null, string? search_value = null)
        {
            var user = await _userManager.GetUserAsync(User);

            var certificates = await _certificateService.GetAllCertificates(user, sort_by, sort_type, search_value);

            return certificates;
        }

        [Authorize]
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetCertificateById(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            RegistrationCertificateModel certificate = _certificateService.GetCertificateById(user, id);

            if (certificate is null)
                return NotFound();

            return Ok(certificate);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(RegistrationCertificateModel certificate)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = _certificateService.AddCertificate(user, certificate);

            if(result.Errors != null)
                return BadRequest(result.Errors);
            else
                return Created("", result.Certificate);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult> Update(int id, RegistrationCertificateModel certificate)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = _certificateService.UpdateCertificate(user, id, certificate);

            if (result == null)
                return NotFound();
            else if (result.Errors != null)
                return BadRequest(result.Errors);
            else
                return Ok(result.Certificate);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = _certificateService.DeleteCertificate(user, id);

            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }
    }
}
