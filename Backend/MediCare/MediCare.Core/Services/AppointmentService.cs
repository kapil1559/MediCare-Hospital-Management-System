using MediCare.Core.Interfaces;
using MediCare.Domain.Common;
using MediCare.Domain.Requests;
using MediCare.Domain.Responses;

namespace MediCare.Core.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;

    public AppointmentService(
        IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> SaveAppointmentAsync(
        AppointmentRequest request)
    {
        return await _repository.SaveAppointmentAsync(request);
    }

    public async Task<PagedResponse<AppointmentResponse>> GetAppointmentsAsync(
        string search,
        int pageNumber,
        int pageSize)
    {
        return await _repository.GetAppointmentsAsync(
            search,
            pageNumber,
            pageSize);
    }

    public async Task<AppointmentResponse?> GetAppointmentByIdAsync(
        int appointmentId)
    {
        return await _repository.GetAppointmentByIdAsync(
            appointmentId);
    }

    public async Task<DeleteAppointmentResponse> DeleteAppointmentAsync(
        int appointmentId)
    {
        return await _repository.DeleteAppointmentAsync(
            appointmentId);
    }
}