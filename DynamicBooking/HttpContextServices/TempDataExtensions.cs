using DynamicBooking.UseCases.Signup;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace DynamicBooking.HttpContextServices;

public static class TempDataExtensions
{
    public static void AddRegistrationSeccess(this ITempDataDictionary tempData, IEnumerable<RegistrationSuccessDto> registrationSuccessDtos)
    {
        var serialized = JsonConvert.SerializeObject(registrationSuccessDtos);

        tempData["registrationResultDtos"] = serialized;
    }

    public static IEnumerable<RegistrationSuccessDto> GetRegistrationSuccessDtos(this ITempDataDictionary tempData)
    {
        if (tempData.TryGetValue("registrationResultDtos", out object value))
        {
            tempData.Keep("registrationResultDtos");
            return JsonConvert.DeserializeObject<IEnumerable<RegistrationSuccessDto>>((string)value);
        }
        return Enumerable.Empty<RegistrationSuccessDto>();
    }
}
