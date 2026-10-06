using MediCare.Domain.Responses;

namespace MediCare.Core.Interfaces;

public interface IModuleListConfigurationService
{
    Task<ModuleListConfigurationResponse?> GetByModuleCodeAsync(
        string moduleCode);
}