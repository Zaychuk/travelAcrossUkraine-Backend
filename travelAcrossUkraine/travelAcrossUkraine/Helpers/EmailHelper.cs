using System.Net.Mail;

namespace TravelAcrossUkraine.WebApi.Helpers;

public static class EmailHelper
{
    public static bool IsValid(string emailaddress)
    {
        return MailAddress.TryCreate(emailaddress, out _);
    }
}
