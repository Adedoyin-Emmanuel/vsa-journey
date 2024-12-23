using dotenv.net;
namespace vsa_journey.Utils;

 public static class EnvConfig
 {

     static  EnvConfig()
     {
         DotEnv.Load();
     }
     public static string DatabaseUrl =>  Environment.GetEnvironmentVariable("DATABASE_URL");
     public static bool IsProduction => string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "Production", StringComparison.OrdinalIgnoreCase);

 }