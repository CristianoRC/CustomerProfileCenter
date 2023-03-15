using System.Text.RegularExpressions;

namespace CustomerProfileCenter.Domain.ValueObjects;

public record EmailAddress
{
    public EmailAddress(string address)
    {
        Address = address.Trim();
        const string emailAddressPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        IsValid = Regex.IsMatch(Address, emailAddressPattern, RegexOptions.None, TimeSpan.FromSeconds(10));
    }

    public string Address { get; }

    public bool IsValid { get; }
}