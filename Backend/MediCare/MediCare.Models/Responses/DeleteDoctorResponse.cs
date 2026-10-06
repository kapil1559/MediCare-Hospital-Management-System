namespace MediCare.Domain.Responses;

public class DeleteDoctorResponse
{
    public int Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public int DoctorID { get; set; }
}