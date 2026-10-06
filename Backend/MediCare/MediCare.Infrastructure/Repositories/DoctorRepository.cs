using Dapper;
using MediCare.Core.Context;
using MediCare.Core.Interfaces;
using MediCare.Domain.Common;
using MediCare.Domain.Requests;
using MediCare.Domain.Responses;
using MediCare.Infrastructure.Data;
using System.Data;

namespace MediCare.Infrastructure.Repositories;

public class DoctorRepository : BaseRepository, IDoctorRepository
{
    private readonly HeaderContext _headerContext;

    public DoctorRepository(
        DapperContext context,
        HeaderContext headerContext)
        : base(context)
    {
        _headerContext = headerContext;
    }

    public async Task<int> SaveDoctorAsync(DoctorRequest request)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@Mode", request.Mode);
        parameters.Add("@DoctorID", request.DoctorID);

        // Header values
        parameters.Add("@HospitalID", _headerContext.HospitalID);
        parameters.Add("@LocationID", _headerContext.LocationID);

        parameters.Add("@DoctorCode", request.DoctorCode);
        parameters.Add("@FirstName", request.FirstName);
        parameters.Add("@LastName", request.LastName);
        parameters.Add("@Gender", request.Gender);
        parameters.Add("@DOB", request.DOB);
        parameters.Add("@Specialization", request.Specialization);
        parameters.Add("@Qualification", request.Qualification);
        parameters.Add("@MobileNo", request.MobileNo);
        parameters.Add("@Email", request.Email);
        parameters.Add("@Address", request.Address);
        parameters.Add("@City", request.City);
        parameters.Add("@State", request.State);
        parameters.Add("@PinCode", request.PinCode);

        // UserID from header
        parameters.Add("@CreationID", _headerContext.UserID);
        parameters.Add("@LastModificationID", _headerContext.UserID);

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(
            "IUDoctor",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<PagedResponse<DoctorResponse>> GetDoctorsAsync(
        string search,
        int pageNumber,
        int pageSize)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@HospitalID", _headerContext.HospitalID);
        parameters.Add("@LocationID", _headerContext.LocationID);
        parameters.Add("@Search", search);
        parameters.Add("@PageNumber", pageNumber);
        parameters.Add("@PageSize", pageSize);

        using var connection = _context.CreateConnection();

        using var multi = await connection.QueryMultipleAsync(
            "GetDoctors",
            parameters,
            commandType: CommandType.StoredProcedure);

        var doctors = (await multi.ReadAsync<DoctorResponse>()).ToList();

        var totalRecords = await multi.ReadFirstOrDefaultAsync<int>();

        return new PagedResponse<DoctorResponse>
        {
            Data = doctors,
            TotalRecords = totalRecords,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<DoctorResponse?> GetDoctorByIdAsync(int doctorId)
    {
        var parameters = new DynamicParameters();

        // Only DoctorID for Get By ID
        parameters.Add("@DoctorID", doctorId);

        return await QueryFirstOrDefaultAsync<DoctorResponse>(
            "GetDoctorById",
            parameters);
    }

    public async Task<DeleteDoctorResponse> DeleteDoctorAsync(int doctorId)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@DoctorID", doctorId);
        parameters.Add("@HospitalID", _headerContext.HospitalID);
        parameters.Add("@LocationID", _headerContext.LocationID);
        parameters.Add("@LastModificationID", _headerContext.UserID);

        return await QueryFirstOrDefaultAsync<DeleteDoctorResponse>(
            "DeleteDoctor",
            parameters);
    }
}