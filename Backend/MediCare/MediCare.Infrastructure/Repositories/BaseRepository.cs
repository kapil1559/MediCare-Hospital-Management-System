using Dapper;
using MediCare.Infrastructure.Data;
using System.Data;
using Dapper;
using MediCare.Infrastructure.Data;
using System.Data;

namespace MediCare.Infrastructure.Repositories;

public abstract class BaseRepository
{
    protected readonly DapperContext _context;

    protected BaseRepository(DapperContext context)
    {
        _context = context;
    }

    protected async Task<int> ExecuteAsync(
        string sp,
        DynamicParameters parameters)
    {
        using var connection = _context.CreateConnection();

        return await connection.ExecuteAsync(
            sp,
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    protected async Task<IEnumerable<T>> QueryAsync<T>(
        string sp,
        DynamicParameters parameters)
    {
        using var connection = _context.CreateConnection();

        return await connection.QueryAsync<T>(
            sp,
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    protected async Task<T?> QueryFirstOrDefaultAsync<T>(
        string sp,
        DynamicParameters parameters)
    {
        using var connection = _context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<T>(
            sp,
            parameters,
            commandType: CommandType.StoredProcedure);
    }
}