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
     public static string ValidAudience => Environment.GetEnvironmentVariable("VALID_AUDIENCE");
     public static string ValidIssuer => Environment.GetEnvironmentVariable("VALID_ISSUER");
     public static string JwtSecret => Environment.GetEnvironmentVariable("JWT_SECRET");
     public static string SenderEmail => Environment.GetEnvironmentVariable("SENDER_EMAIL");
     public static string SenderName => Environment.GetEnvironmentVariable("SENDER_NAME");
     public static string EmailHost => Environment.GetEnvironmentVariable("EMAIL_HOST");
     public static int EmailPort => int.Parse(Environment.GetEnvironmentVariable("EMAIL_PORT"));
     
 }