using MediCare.Core.Interfaces;
using MediCare.Domain.Common;
using MediCare.Domain.Requests;
using MediCare.Domain.Responses;

namespace MediCare.Core.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;

    public DepartmentService(IDepartmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> SaveDepartmentAsync(
        DepartmentRequest request)
    {
        return await _repository.SaveDepartmentAsync(request);
    }

    public async Task<PagedResponse<DepartmentResponse>> GetDepartmentsAsync(
        string search,
        int pageNumber,
        int pageSize)
    {
        return await _repository.GetDepartmentsAsync(
            search,
            pageNumber,
            pageSize);
    }

    public async Task<DepartmentResponse?> GetDepartmentByIdAsync(
        int departmentId)
    {
        return await _repository.GetDepartmentByIdAsync(
            departmentId);
    }

    public async Task<DeleteDepartmentResponse> DeleteDepartmentAsync(
        int departmentId)
    {
        return await _repository.DeleteDepartmentAsync(
            departmentId);
    }
}