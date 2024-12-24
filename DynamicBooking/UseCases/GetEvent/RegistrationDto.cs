using DynamicBooking.Doomain;

namespace DynamicBooking.UseCases.GetEvent;

public class RegistrationDto
{
    public Guid Id { get; set; }

    public UserDto Participant { get; set; }
}