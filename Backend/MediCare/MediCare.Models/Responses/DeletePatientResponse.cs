namespace MediCare.Domain.Responses;

public class DeletePatientResponse
{
    public int Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public int PatientID { get; set; }
}