namespace MediCare.Domain.Responses;

public class AppointmentResponse
{
    public int AppointmentID { get; set; }

    public int HospitalID { get; set; }

    public int LocationID { get; set; }

    public string AppointmentCode { get; set; } = string.Empty;

    public int PatientID { get; set; }

    public string? PatientName { get; set; }

    public int DoctorID { get; set; }

    public string? DoctorName { get; set; }

    public int DepartmentID { get; set; }

    public string? DepartmentName { get; set; }

    public DateTime AppointmentDate { get; set; }

    public TimeSpan AppointmentTime { get; set; }

    public string? Reason { get; set; }

    public string Status { get; set; } = string.Empty;

    public bool RowStatus { get; set; }
}