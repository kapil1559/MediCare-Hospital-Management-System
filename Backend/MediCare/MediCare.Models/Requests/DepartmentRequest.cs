namespace MediCare.Domain.Requests;

public class DepartmentRequest
{
    public string Mode { get; set; } = string.Empty;

    public int DepartmentID { get; set; }

    public string DepartmentCode { get; set; } = string.Empty;

    public string DepartmentName { get; set; } = string.Empty;

    public string? Description { get; set; }
}