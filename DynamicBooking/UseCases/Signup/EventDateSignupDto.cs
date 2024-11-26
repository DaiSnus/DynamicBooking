namespace DynamicBooking.UseCases.Signup;

public class EventDateSignupDto
{
    public int Id { get; set; }

    public TimeSlotSignupDto TimeSlot { get; set; }
}
