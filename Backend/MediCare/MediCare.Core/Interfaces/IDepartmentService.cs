using MediCare.Domain.Common;
using MediCare.Domain.Requests;
using MediCare.Domain.Responses;

namespace MediCare.Core.Interfaces;

public interface IDepartmentService
{
    Task<int> SaveDepartmentAsync(DepartmentRequest request);

    Task<PagedResponse<DepartmentResponse>> GetDepartmentsAsync(
        string search,
        int pageNumber,
        int pageSize);

    Task<DepartmentResponse?> GetDepartmentByIdAsync(
        int departmentId);

    Task<DeleteDepartmentResponse> DeleteDepartmentAsync(
        int departmentId);
}