using Dapper;
using MediCare.Core.Interfaces;
using MediCare.Domain.Responses;
using MediCare.Infrastructure.Data;

namespace MediCare.Infrastructure.Repositories;

public class ModuleListConfigurationRepository
    : BaseRepository, IModuleListConfigurationRepository
{
    public ModuleListConfigurationRepository(
        DapperContext context)
        : base(context)
    {
    }

    public async Task<ModuleListConfigurationResponse?> GetByModuleCodeAsync(
        string moduleCode)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@ModuleCode", moduleCode);

        return await QueryFirstOrDefaultAsync<ModuleListConfigurationResponse>(
            "GetModuleListConfiguration",
            parameters);
    }
}