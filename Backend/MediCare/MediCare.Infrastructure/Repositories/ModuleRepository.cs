using Dapper;
using MediCare.Core.Interfaces;
using MediCare.Domain.Responses;
using MediCare.Infrastructure.Data;

namespace MediCare.Infrastructure.Repositories;

public class ModuleRepository : BaseRepository, IModuleRepository
{
    public ModuleRepository(DapperContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<ModuleResponse>> GetActiveModulesAsync()
    {
        var parameters = new DynamicParameters();

        return await QueryAsync<ModuleResponse>(
            "GetActiveModules",
            parameters);
    }
}