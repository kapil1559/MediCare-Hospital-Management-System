using MediCare.Domain.Responses;

namespace MediCare.Core.Interfaces;

public interface IModuleService
{
    Task<IEnumerable<ModuleResponse>> GetActiveModulesAsync();
}