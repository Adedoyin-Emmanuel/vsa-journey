namespace vsa_journey.Features.Authentication.Policies;

public static class PolicyNames
{
    public const string SuperAdmin = "SuperAdmin";
    public const string Admin = "Admin";
    public const string SalesRepresentative = "SalesRepresentative";
    public const string User = "User";
    public const string SuperAdminOrAdmin = "SuperAdminOrAdmin";
    public const string SalesRepresentativeOrUser = "SalesRepresentativeOrUser";
    public const string AdminOrSalesRepresentative = "AdminOrSalesRepresentative";
    public const string SuperAdminOrAdminOrSalesRepresentativeOrUser = "SuperAdminOrAdminOrSalesRepresentativeOrUser";
}