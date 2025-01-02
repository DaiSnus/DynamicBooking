using DynamicBooking.Doomain;

namespace DynamicBooking.UseCases.GetParticipants;

public class ParticipantsDto
{
    public IEnumerable<Registration>? Registrations { get; set; }
}