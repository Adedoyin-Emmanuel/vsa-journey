using vsa_journey.Features.Users.Repository;

namespace vsa_journey.Utils;

public class UsernameGenerator
{

    private readonly Random _random;
    private readonly IUserRespository _userRespository;
    private readonly int MaxUsernameLength = 15; 

    public UsernameGenerator(IUserRespository userRespository)
    {
       _random = new Random();
       _userRespository = userRespository;
    }

    public async Task<string> GenerateUsernameAsync(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            throw new ArgumentException("First name and last name cannot be null or empty.");
        }

        var firstPart = firstName.Length > 6 ? firstName.Substring(0, 6) : firstName;
        var lastPart = lastName.Length > 4 ? lastName.Substring(0, 4) : lastName;

        var randomSuffix = _random.Next(1000, 1000000);

        var userName = $"{firstPart}{lastPart}{randomSuffix}";

        if (userName.Length > MaxUsernameLength)
        {
            userName = userName.Substring(0, MaxUsernameLength);
        }

        while (await _userRespository.GetUserByUsernameAsync(userName))
        {
            randomSuffix = _random.Next(1000, 1000000);
            userName = $"{firstPart}{lastPart}{randomSuffix}";

            if (userName.Length > MaxUsernameLength)
            {
                userName = userName.Substring(0, MaxUsernameLength);
            }
        }

        return userName.ToLower();
    }
}