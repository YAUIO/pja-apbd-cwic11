using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using pja_apbd_cwic11.DTOs;
using pja_apbd_cwic11.Exceptions;
using pja_apbd_cwic11.Services;

namespace pja_apbd_cwic11.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly IDbService _service;

    public PrescriptionController(IDbService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> PostNewPrescription(PostPrescriptionDTO prescription)
    {
        try
        {
            return Ok(await _service.AddNewPrescriptionAsync(prescription));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (KeyExistsException e)
        {
            return BadRequest(e.Message);
        }
        catch (MaximumSizeExceededException e)
        {
            return BadRequest(e.Message);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
    }
}