using MediCare.Domain.Common;
using MediCare.Domain.Requests;
using MediCare.Domain.Responses;

namespace MediCare.Core.Interfaces;

public interface IDoctorService
{
    Task<int> SaveDoctorAsync(DoctorRequest request);

    Task<PagedResponse<DoctorResponse>> GetDoctorsAsync(
        string search,
        int pageNumber,
        int pageSize);

    Task<DoctorResponse?> GetDoctorByIdAsync(int doctorId);

    Task<DeleteDoctorResponse> DeleteDoctorAsync(int doctorId);
}