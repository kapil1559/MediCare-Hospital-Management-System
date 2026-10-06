namespace MediCare.Domain.Responses;

public class DeleteAppointmentResponse
{
    public int Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public int AppointmentID { get; set; }
}