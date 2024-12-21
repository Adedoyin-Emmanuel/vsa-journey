using dotenv.net;
namespace vsa_journey.Utils;

 public static class EnvConfig
 {
     public static string DatabaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

 }