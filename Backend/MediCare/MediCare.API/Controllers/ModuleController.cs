using MediCare.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModulesController : ControllerBase
{
    private readonly IModuleService _service;

    public ModulesController(IModuleService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetActiveModules()
    {
        var modules = await _service.GetActiveModulesAsync();

        return Ok(modules);
    }
}