using MediCare.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListConfigurationController : ControllerBase
{
    private readonly IModuleListConfigurationService _service;

    public ListConfigurationController(
        IModuleListConfigurationService service)
    {
        _service = service;
    }

    [HttpGet("{moduleCode}")]
    public async Task<IActionResult> GetByModuleCode(string moduleCode)
    {
        var configuration =
            await _service.GetByModuleCodeAsync(moduleCode);

        if (configuration is null)
        {
            return NotFound(new
            {
                message = "List configuration not found."
            });
        }

        return Ok(configuration);
    }
}