using DynamicBooking.Doomain;
using Microsoft.VisualBasic;

namespace DynamicBooking.UseCases.Signup;

public class RegistrationSuccessDto
{
    public string EventTitle { get; set; }

    public bool IsSuccess { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
}