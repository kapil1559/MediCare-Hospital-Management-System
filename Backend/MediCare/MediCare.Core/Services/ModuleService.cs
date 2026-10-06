using MediCare.Core.Interfaces;
using MediCare.Domain.Responses;

namespace MediCare.Core.Services;

public class ModuleService : IModuleService
{
    private readonly IModuleRepository _repository;

    public ModuleService(IModuleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ModuleResponse>> GetActiveModulesAsync()
    {
        return await _repository.GetActiveModulesAsync();
    }
}