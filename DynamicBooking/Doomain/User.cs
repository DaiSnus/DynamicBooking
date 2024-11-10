using System.Security.Principal;

namespace DynamicBooking.Doomain;

public class User 
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public string PhoneNumber {  get; set; }

    public string Email { get; set; }
}
