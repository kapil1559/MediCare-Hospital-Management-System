
using MediCare.Core.Interfaces;
using MediCare.Domain.Common;
using MediCare.Domain.Requests;
using MediCare.Domain.Responses;

namespace MediCare.Core.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repository;

    public DoctorService(IDoctorRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> SaveDoctorAsync(DoctorRequest request)
    {
        return await _repository.SaveDoctorAsync(request);
    }

    public async Task<PagedResponse<DoctorResponse>> GetDoctorsAsync(
        string search,
        int pageNumber,
        int pageSize)
    {
        return await _repository.GetDoctorsAsync(
            search,
            pageNumber,
            pageSize);
    }

    public async Task<DoctorResponse?> GetDoctorByIdAsync(int doctorId)
    {
        return await _repository.GetDoctorByIdAsync(doctorId);
    }

    public async Task<DeleteDoctorResponse> DeleteDoctorAsync(int doctorId)
    {
        return await _repository.DeleteDoctorAsync(doctorId);
    }
}