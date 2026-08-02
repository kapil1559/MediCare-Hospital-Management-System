namespace MediCare.Domain.Responses;

public class LoginResponse
{
    public int UserID { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string RoleName { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;
}