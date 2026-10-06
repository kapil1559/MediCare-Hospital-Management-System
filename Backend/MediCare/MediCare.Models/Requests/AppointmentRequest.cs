namespace MediCare.Domain.Requests;

public class AppointmentRequest
{
    public string Mode { get; set; } = string.Empty;

    public int AppointmentID { get; set; }

    public string AppointmentCode { get; set; } = string.Empty;

    public int PatientID { get; set; }

    public int DoctorID { get; set; }

    public int DepartmentID { get; set; }

    public DateTime AppointmentDate { get; set; }

    public TimeSpan AppointmentTime { get; set; }

    public string? Reason { get; set; }

    public string Status { get; set; } = "Scheduled";
}