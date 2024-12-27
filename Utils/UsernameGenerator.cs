namespace vsa_journey.Utils;

public class UsernameGenerator
{

    private readonly Random _random;

    public UsernameGenerator()
    {
       _random = new Random();
    }


    public string GenerateUsername(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            throw new ArgumentException("First name and last name cannot be null or empty.");
        }

        var firstPart = firstName.Length > 6 ? firstName.Substring(0, 6) : firstName;
        var lastPart = lastName.Length > 4 ? lastName.Substring(0, 4) : lastName;

        var randomSuffix = _random.Next(1000, 1000000);

        var userName = $"{firstPart}{lastPart}{randomSuffix}";

        if (userName.Length > 16)
        {
            userName = userName.Substring(0, 16);
        }

        // TODO: Check for uniqueness using UserRepository 

        return userName;
    }
}