using MediCare.Core.Interfaces;
using MediCare.Domain.Responses;

namespace MediCare.Core.Services;

public class ModuleListConfigurationService
    : IModuleListConfigurationService
{
    private readonly IModuleListConfigurationRepository _repository;

    public ModuleListConfigurationService(
        IModuleListConfigurationRepository repository)
    {
        _repository = repository;
    }

    public async Task<ModuleListConfigurationResponse?> GetByModuleCodeAsync(
        string moduleCode)
    {
        return await _repository.GetByModuleCodeAsync(moduleCode);
    }
}