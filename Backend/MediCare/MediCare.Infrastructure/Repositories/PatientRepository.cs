using Dapper;
using MediCare.Core.Context;
using MediCare.Core.Interfaces;
using MediCare.Domain.Common;
using MediCare.Domain.Requests;
using MediCare.Domain.Responses;
using MediCare.Infrastructure.Data;

namespace MediCare.Infrastructure.Repositories;

public class PatientRepository : BaseRepository, IPatientRepository
{
    private readonly HeaderContext _headerContext;

    public PatientRepository(
        DapperContext context,
        HeaderContext headerContext)
        : base(context)
    {
        _headerContext = headerContext;
    }

    public async Task<int> SavePatientAsync(PatientRequest request)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@Mode", request.Mode);
        parameters.Add("@PatientID", request.PatientID);

        // Header values
        parameters.Add("@HospitalID", _headerContext.HospitalID);
        parameters.Add("@LocationID", _headerContext.LocationID);

        parameters.Add("@PatientCode", request.PatientCode);
        parameters.Add("@FirstName", request.FirstName);
        parameters.Add("@LastName", request.LastName);
        parameters.Add("@Gender", request.Gender);
        parameters.Add("@DOB", request.DOB);
        parameters.Add("@Age", request.Age);
        parameters.Add("@BloodGroup", request.BloodGroup);
        parameters.Add("@MobileNo", request.MobileNo);
        parameters.Add("@Email", request.Email);
        parameters.Add("@Address", request.Address);
        parameters.Add("@City", request.City);
        parameters.Add("@State", request.State);
        parameters.Add("@PinCode", request.PinCode);

        // Header UserID
        parameters.Add("@CreationID", _headerContext.UserID);
        parameters.Add("@LastModificationID", _headerContext.UserID);

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(
            "USP_IUPatient",
            parameters,
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<PagedResponse<PatientResponse>> GetPatientsAsync(
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
            "GetPatients",
            parameters,
            commandType: System.Data.CommandType.StoredProcedure);

        var patients = (await multi.ReadAsync<PatientResponse>()).ToList();

        var totalRecords = await multi.ReadFirstOrDefaultAsync<int>();

        return new PagedResponse<PatientResponse>
        {
            Data = patients,
            TotalRecords = totalRecords,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<PatientResponse?> GetPatientByIdAsync(int patientId)
    {
        var parameters = new DynamicParameters();

        // GetById only requires PatientID
        parameters.Add("@PatientID", patientId);

        return await QueryFirstOrDefaultAsync<PatientResponse>(
            "GetPatientById",
            parameters);
    }

    public async Task<DeletePatientResponse> DeletePatientAsync(int patientId)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@PatientID", patientId);
        parameters.Add("@HospitalID", _headerContext.HospitalID);
        parameters.Add("@LocationID", _headerContext.LocationID);
        //parameters.Add("@LastModificationID", _headerContext.UserID);

        return await QueryFirstOrDefaultAsync<DeletePatientResponse>(
            "DeletePatient",
            parameters);
    }
}