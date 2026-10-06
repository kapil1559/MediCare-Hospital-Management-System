using MediCare.Core.Interfaces;
using MediCare.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MediCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    // GET: api/Doctor
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string search = "",
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _doctorService.GetDoctorsAsync(
            search,
            pageNumber,
            pageSize);

        return Ok(result);
    }

    // GET: api/Doctor/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _doctorService.GetDoctorByIdAsync(id);

        if (result == null)
        {
            return NotFound(new
            {
                success = false,
                message = "Doctor not found."
            });
        }

        return Ok(result);
    }

    // POST: api/Doctor/IUDoctor
    [HttpPost("IUDoctor")]
    public async Task<IActionResult> Save(
        [FromBody] DoctorRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Mode))
        {
            return BadRequest("Mode is required.");
        }

        if (!request.Mode.Equals("Add", StringComparison.OrdinalIgnoreCase) &&
            !request.Mode.Equals("Edit", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Mode must be Add or Edit.");
        }

        if (request.Mode.Equals("Edit", StringComparison.OrdinalIgnoreCase) &&
            request.DoctorID <= 0)
        {
            return BadRequest("DoctorID is required for Edit.");
        }

        var doctorId = await _doctorService.SaveDoctorAsync(request);

        return Ok(new
        {
            success = true,
            message = request.Mode.Equals(
                "Add",
                StringComparison.OrdinalIgnoreCase)
                ? "Doctor added successfully."
                : "Doctor updated successfully.",
            doctorID = doctorId
        });
    }

    // DELETE: api/Doctor/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _doctorService.DeleteDoctorAsync(id);

        if (result.Success == 0)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}