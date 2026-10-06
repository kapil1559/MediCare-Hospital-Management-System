using Dapper;
using MediCare.Core.Context;
using MediCare.Core.Interfaces;
using MediCare.Domain.Common;
using MediCare.Domain.Requests;
using MediCare.Domain.Responses;
using MediCare.Infrastructure.Data;
using System.Data;

namespace MediCare.Infrastructure.Repositories;

public class AppointmentRepository : BaseRepository, IAppointmentRepository
{
    private readonly HeaderContext _headerContext;

    public AppointmentRepository(
        DapperContext context,
        HeaderContext headerContext)
        : base(context)
    {
        _headerContext = headerContext;
    }

    public async Task<int> SaveAppointmentAsync(
        AppointmentRequest request)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@Mode", request.Mode);
        parameters.Add("@AppointmentID", request.AppointmentID);

        // Header values
        parameters.Add(
            "@HospitalID",
            _headerContext.HospitalID);

        parameters.Add(
            "@LocationID",
            _headerContext.LocationID);

        parameters.Add(
            "@AppointmentCode",
            request.AppointmentCode);

        parameters.Add(
            "@PatientID",
            request.PatientID);

        parameters.Add(
            "@DoctorID",
            request.DoctorID);

        parameters.Add(
            "@DepartmentID",
            request.DepartmentID);

        parameters.Add(
            "@AppointmentDate",
            request.AppointmentDate);

        parameters.Add(
            "@AppointmentTime",
            request.AppointmentTime);

        parameters.Add(
            "@Reason",
            request.Reason);

        parameters.Add(
            "@Status",
            request.Status);

        // UserID from header
        parameters.Add(
            "@CreationID",
            _headerContext.UserID);

        parameters.Add(
            "@LastModificationID",
            _headerContext.UserID);

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(
            "USP_IUAppointment",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<PagedResponse<AppointmentResponse>> GetAppointmentsAsync(
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
            "GetAppointments",
            parameters,
            commandType: CommandType.StoredProcedure);

        var appointments =
            (await multi.ReadAsync<AppointmentResponse>())
            .ToList();

        var totalRecords =
            await multi.ReadFirstOrDefaultAsync<int>();

        return new PagedResponse<AppointmentResponse>
        {
            Data = appointments,
            TotalRecords = totalRecords,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<AppointmentResponse?> GetAppointmentByIdAsync(
        int appointmentId)
    {
        var parameters = new DynamicParameters();

        // Only AppointmentID
        parameters.Add(
            "@AppointmentID",
            appointmentId);

        return await QueryFirstOrDefaultAsync<AppointmentResponse>(
            "GetAppointmentById",
            parameters);
    }

    public async Task<DeleteAppointmentResponse> DeleteAppointmentAsync(
        int appointmentId)
    {
        var parameters = new DynamicParameters();

        parameters.Add(
            "@AppointmentID",
            appointmentId);

        parameters.Add(
            "@HospitalID",
            _headerContext.HospitalID);

        parameters.Add(
            "@LocationID",
            _headerContext.LocationID);

        parameters.Add(
            "@LastModificationID",
            _headerContext.UserID);

        return await QueryFirstOrDefaultAsync<DeleteAppointmentResponse>(
            "DeleteAppointment",
            parameters);
    }
}