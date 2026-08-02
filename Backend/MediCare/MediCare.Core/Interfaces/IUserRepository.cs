using MediCare.Domain.Requests;
//using MediCare.Models.Requests;
using MediCare.Domain.Responses;

namespace MediCare.Core.Interfaces;

public interface IUserRepository
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}