using Dapper;
using MediCare.Core.Context;
using MediCare.Core.Interfaces;
using MediCare.Domain.Common;
using MediCare.Domain.Requests;
using MediCare.Domain.Responses;
using MediCare.Infrastructure.Data;
using System.Data;

namespace MediCare.Infrastructure.Repositories;

public class DepartmentRepository : BaseRepository, IDepartmentRepository
{
    private readonly HeaderContext _headerContext;

    public DepartmentRepository(
        DapperContext context,
        HeaderContext headerContext)
        : base(context)
    {
        _headerContext = headerContext;
    }

    public async Task<int> SaveDepartmentAsync(
        DepartmentRequest request)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@Mode", request.Mode);
        parameters.Add("@DepartmentID", request.DepartmentID);

        // Header values
        parameters.Add("@HospitalID", _headerContext.HospitalID);
        parameters.Add("@LocationID", _headerContext.LocationID);

        parameters.Add(
            "@DepartmentCode",
            request.DepartmentCode);

        parameters.Add(
            "@DepartmentName",
            request.DepartmentName);

        parameters.Add(
            "@Description",
            request.Description);

        // UserID from header
        parameters.Add(
            "@CreationID",
            _headerContext.UserID);

        parameters.Add(
            "@LastModificationID",
            _headerContext.UserID);

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(
            "USP_IUDepartment",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<PagedResponse<DepartmentResponse>> GetDepartmentsAsync(
        string search,
        int pageNumber,
        int pageSize)
    {
        var parameters = new DynamicParameters();

        parameters.Add(
            "@HospitalID",
            _headerContext.HospitalID);

        parameters.Add(
            "@LocationID",
            _headerContext.LocationID);

        parameters.Add("@Search", search);
        parameters.Add("@PageNumber", pageNumber);
        parameters.Add("@PageSize", pageSize);

        using var connection = _context.CreateConnection();

        using var multi = await connection.QueryMultipleAsync(
            "GetDepartments",
            parameters,
            commandType: CommandType.StoredProcedure);

        var departments =
            (await multi.ReadAsync<DepartmentResponse>())
            .ToList();

        var totalRecords =
            await multi.ReadFirstOrDefaultAsync<int>();

        return new PagedResponse<DepartmentResponse>
        {
            Data = departments,
            TotalRecords = totalRecords,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<DepartmentResponse?> GetDepartmentByIdAsync(
        int departmentId)
    {
        var parameters = new DynamicParameters();

        // Get By ID only requires DepartmentID
        parameters.Add(
            "@DepartmentID",
            departmentId);

        return await QueryFirstOrDefaultAsync<DepartmentResponse>(
            "GetDepartmentById",
            parameters);
    }

    public async Task<DeleteDepartmentResponse> DeleteDepartmentAsync(
        int departmentId)
    {
        var parameters = new DynamicParameters();

        parameters.Add(
            "@DepartmentID",
            departmentId);

        parameters.Add(
            "@HospitalID",
            _headerContext.HospitalID);

        parameters.Add(
            "@LocationID",
            _headerContext.LocationID);

        parameters.Add(
            "@LastModificationID",
            _headerContext.UserID);

        return await QueryFirstOrDefaultAsync<DeleteDepartmentResponse>(
            "DeleteDepartment",
            parameters);
    }
}