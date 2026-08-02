using Dapper;
using MediCare.Core.Interfaces;
using MediCare.Domain.Requests;
using MediCare.Infrastructure.Data;
using MediCare.Domain.Responses;

namespace MediCare.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;

    public UserRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        using var connection = _context.CreateConnection();

        var parameters = new
        {
            request.UserName,
            PasswordHash = request.Password
        };

        return await connection.QueryFirstOrDefaultAsync<LoginResponse>(
            "USP_Login",
            parameters,
            commandType: System.Data.CommandType.StoredProcedure);
    }
}