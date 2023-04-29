using System.Net.Mail;

namespace TravelAcrossUkraine.WebApi.Helpers;

public static class EmailHelper
{
    public static bool IsValid(string emailAddress)
    {
        return MailAddress.TryCreate(emailAddress, out _);
    }
}
