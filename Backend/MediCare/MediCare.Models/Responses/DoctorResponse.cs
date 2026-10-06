namespace MediCare.Domain.Responses;

public class DoctorResponse
{
    public int DoctorID { get; set; }

    public int HospitalID { get; set; }

    public int LocationID { get; set; }

    public string DoctorCode { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; }

    public string FullName =>
        string.Join(
            " ",
            new[] { FirstName, LastName }
                .Where(x => !string.IsNullOrWhiteSpace(x)));

    public string? Gender { get; set; }

    public DateTime? DOB { get; set; }

    public string? Specialization { get; set; }

    public string? Qualification { get; set; }

    public string? MobileNo { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? PinCode { get; set; }

    public bool RowStatus { get; set; }
}