using System.Text.RegularExpressions;

namespace CustomerProfileCenter.Domain.ValueObjects;

public record PhoneNumber
{
    public PhoneNumber(string number)
    {
        Number = number.Trim();
        const string phoneNumberPattern = @"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$";
        IsValid = Regex.IsMatch(Number, phoneNumberPattern, RegexOptions.None, TimeSpan.FromSeconds(10));
    }

    public string Number { get; }
    public bool IsValid { get; }
}