namespace MediCare.Domain.Responses;

public class DeleteDepartmentResponse
{
    public int Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public int DepartmentID { get; set; }
}