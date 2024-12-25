namespace vsa_journey.Utils;

public class UsernameGenerator
{
    public string GenerateUsername(string firstName, string lastName)
    {
        var firstPart = firstName.Length > 6 ? firstName.Substring(0, 6) : firstName;
        var lastPart = lastName.Length > 4 ? lastName.Substring(0, 4) : lastName;
        
        var randomSuffix = new Random().Next(1000, 9999).ToString();
        
        var userName = $"{firstPart}{lastPart}{randomSuffix}";

        if (userName.Length > 16)
        {
            userName = userName.Substring(0, 16);
        }
        
        //TODO Check for uniqueness using UserRepository 

        return userName;
    }
}