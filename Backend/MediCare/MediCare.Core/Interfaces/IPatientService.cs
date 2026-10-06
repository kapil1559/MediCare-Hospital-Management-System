using MediCare.Domain.Requests;
using MediCare.Domain.Responses;
using MediCare.Domain.Common;

namespace MediCare.Core.Interfaces;

public interface IPatientService
{
    Task<int> SavePatientAsync(PatientRequest request);

    Task<PagedResponse<PatientResponse>> GetPatientsAsync(
        string search,
        int pageNumber,
        int pageSize);

    Task<PatientResponse?> GetPatientByIdAsync(int patientId);

    Task<DeletePatientResponse> DeletePatientAsync(int patientId);
}