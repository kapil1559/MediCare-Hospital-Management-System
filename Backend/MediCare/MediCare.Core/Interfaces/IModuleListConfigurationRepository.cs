using MediCare.Domain.Responses;

namespace MediCare.Core.Interfaces;

public interface IModuleListConfigurationRepository
{
    Task<ModuleListConfigurationResponse?> GetByModuleCodeAsync(
        string moduleCode);
}