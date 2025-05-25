using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pja_apbd_cwic11.Services;

namespace pja_apbd_cwic11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IDbService _service;

        public PatientController(IDbService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientByIdAsync(int id)
        {
            try
            {
                return Ok(await _service.GetPatientByIdAsync(id));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
