using MediCare.Core.Interfaces;
using MediCare.Domain.Common;
using MediCare.Domain.Requests;
using MediCare.Domain.Responses;


namespace MediCare.Core.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository;

    public PatientService(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> SavePatientAsync(PatientRequest request)
    {
        return await _repository.SavePatientAsync(request);
    }

    public async Task<PagedResponse<PatientResponse>> GetPatientsAsync(
        string search,
        int pageNumber,
        int pageSize)
    {
        return await _repository.GetPatientsAsync(
            search,
            pageNumber,
            pageSize);
    }

    public async Task<PatientResponse?> GetPatientByIdAsync(int patientId)
    {
        return await _repository.GetPatientByIdAsync(patientId);
    }

    public async Task<DeletePatientResponse> DeletePatientAsync(int patientId)
    {
        return await _repository.DeletePatientAsync(patientId);
    }
}