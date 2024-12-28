using Microsoft.AspNetCore.Mvc;

namespace DynamicBooking.UseCases.Signup;

public class ParticipantFileDto
{
    [BindProperty]
    public IFormFile FormFile { get; set; }

    public Guid EventFieldId { get; set; }
}
