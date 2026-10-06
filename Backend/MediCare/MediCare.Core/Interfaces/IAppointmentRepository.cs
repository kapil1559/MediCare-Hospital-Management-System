using MediCare.Domain.Common;
using MediCare.Domain.Requests;
using MediCare.Domain.Responses;

namespace MediCare.Core.Interfaces;

public interface IAppointmentRepository
{
    Task<int> SaveAppointmentAsync(
        AppointmentRequest request);

    Task<PagedResponse<AppointmentResponse>> GetAppointmentsAsync(
        string search,
        int pageNumber,
        int pageSize);

    Task<AppointmentResponse?> GetAppointmentByIdAsync(
        int appointmentId);

    Task<DeleteAppointmentResponse> DeleteAppointmentAsync(
        int appointmentId);
}