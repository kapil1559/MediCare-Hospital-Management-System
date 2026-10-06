using MediCare.Core.Interfaces;
using MediCare.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MediCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(
        IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    // GET: api/Appointment
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string search = "",
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _appointmentService.GetAppointmentsAsync(
            search,
            pageNumber,
            pageSize);

        return Ok(result);
    }

    // GET: api/Appointment/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result =
            await _appointmentService.GetAppointmentByIdAsync(id);

        if (result == null)
        {
            return NotFound(new
            {
                success = false,
                message = "Appointment not found."
            });
        }

        return Ok(result);
    }

    // POST: api/Appointment/IUAppointment
    [HttpPost("IUAppointment")]
    public async Task<IActionResult> Save(
        [FromBody] AppointmentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Mode))
        {
            return BadRequest("Mode is required.");
        }

        if (!request.Mode.Equals(
                "Add",
                StringComparison.OrdinalIgnoreCase) &&
            !request.Mode.Equals(
                "Edit",
                StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Mode must be Add or Edit.");
        }

        if (request.Mode.Equals(
                "Edit",
                StringComparison.OrdinalIgnoreCase) &&
            request.AppointmentID <= 0)
        {
            return BadRequest(
                "AppointmentID is required for Edit.");
        }

        if (request.PatientID <= 0)
        {
            return BadRequest("PatientID is required.");
        }

        if (request.DoctorID <= 0)
        {
            return BadRequest("DoctorID is required.");
        }

        if (request.DepartmentID <= 0)
        {
            return BadRequest("DepartmentID is required.");
        }

        var appointmentId =
            await _appointmentService.SaveAppointmentAsync(request);

        return Ok(new
        {
            success = true,

            message = request.Mode.Equals(
                "Add",
                StringComparison.OrdinalIgnoreCase)
                ? "Appointment added successfully."
                : "Appointment updated successfully.",

            appointmentID = appointmentId
        });
    }

    // DELETE: api/Appointment/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result =
            await _appointmentService.DeleteAppointmentAsync(id);

        if (result.Success == 0)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}