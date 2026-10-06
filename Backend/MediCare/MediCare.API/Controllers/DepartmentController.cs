using MediCare.Core.Interfaces;
using MediCare.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MediCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    // GET: api/Department
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string search = "",
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _departmentService.GetDepartmentsAsync(
            search,
            pageNumber,
            pageSize);

        return Ok(result);
    }

    // GET: api/Department/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result =
            await _departmentService.GetDepartmentByIdAsync(id);

        if (result == null)
        {
            return NotFound(new
            {
                success = false,
                message = "Department not found."
            });
        }

        return Ok(result);
    }

    // POST: api/Department/IUDepartment
    [HttpPost("IUDepartment")]
    public async Task<IActionResult> Save(
        [FromBody] DepartmentRequest request)
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
            request.DepartmentID <= 0)
        {
            return BadRequest(
                "DepartmentID is required for Edit.");
        }

        var departmentId =
            await _departmentService.SaveDepartmentAsync(request);

        return Ok(new
        {
            success = true,

            message = request.Mode.Equals(
                "Add",
                StringComparison.OrdinalIgnoreCase)
                ? "Department added successfully."
                : "Department updated successfully.",

            departmentID = departmentId
        });
    }

    // DELETE: api/Department/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result =
            await _departmentService.DeleteDepartmentAsync(id);

        if (result.Success == 0)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}