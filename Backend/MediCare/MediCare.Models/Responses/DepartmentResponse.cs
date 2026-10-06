namespace MediCare.Domain.Responses;

public class DepartmentResponse
{
    public int DepartmentID { get; set; }

    public int HospitalID { get; set; }

    public int LocationID { get; set; }

    public string DepartmentCode { get; set; } = string.Empty;

    public string DepartmentName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool RowStatus { get; set; }
}