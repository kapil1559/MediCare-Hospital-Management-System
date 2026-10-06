namespace MediCare.Domain.Requests;

public class PatientRequest
{
    public string Mode { get; set; } = "ADD";

    public int PatientID { get; set; }

    //public int HospitalID { get; set; }

    //public int LocationID { get; set; }

    public string PatientCode { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; }

    public string Gender { get; set; } = string.Empty;

    public DateTime DOB { get; set; }

    public int Age { get; set; }

    public string? BloodGroup { get; set; }

    public string? MobileNo { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? PinCode { get; set; }

    public int CreationID { get; set; }

    public DateTime CreationDate { get; set; }

    public int? LastModificationID { get; set; }

    public DateTime? LastModificationDate { get; set; }

    public bool RowStatus { get; set; }
}