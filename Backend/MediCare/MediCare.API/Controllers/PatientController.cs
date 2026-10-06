using MediCare.Core.Interfaces;
using MediCare.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MediCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpPost("IUPatient")]
    public async Task<IActionResult> IUPatient(
        [FromBody] PatientRequest request)
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
            request.PatientID <= 0)
        {
            return BadRequest("PatientID is required for Edit.");
        }

        var patientId = await _patientService.SavePatientAsync(request);

        return Ok(new
        {
            success = true,
            message = request.Mode.Equals("Add", StringComparison.OrdinalIgnoreCase)
                ? "Patient added successfully."
                : "Patient updated successfully.",
            patientID = patientId
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        string search = "",
        int pageNumber = 1,
        int pageSize = 20)
    {
        var result = await _patientService.GetPatientsAsync(
            search,
            pageNumber,
            pageSize);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _patientService.GetPatientByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _patientService.DeletePatientAsync(id);

        if (result.Success == 0)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}