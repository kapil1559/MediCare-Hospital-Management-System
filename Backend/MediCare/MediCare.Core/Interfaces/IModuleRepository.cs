using MediCare.Domain.Responses;

namespace MediCare.Core.Interfaces;

public interface IModuleRepository
{
    Task<IEnumerable<ModuleResponse>> GetActiveModulesAsync();
}           