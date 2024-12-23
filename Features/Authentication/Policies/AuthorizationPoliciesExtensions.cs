using Microsoft.AspNetCore.Authorization;
using vsa_journey.Domain.Constants;

namespace vsa_journey.Features.Authentication.Policies;

public static class AuthorizationPoliciesExtensions
{
    public static void AddCustomPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(PolicyNames.SuperAdmin, policy=>  policy.RequireRole(Roles.SuperAdmin));
        options.AddPolicy(PolicyNames.Admin, policy => policy.RequireRole(Roles.Admin));
        options.AddPolicy(PolicyNames.SalesRepresentative, policy => policy.RequireRole(Roles.SalesRepresentative));
        options.AddPolicy(PolicyNames.User, policy => policy.RequireRole(Roles.User));
        options.AddPolicy(PolicyNames.SuperAdminOrAdmin, policy=> policy.RequireRole(Roles.SuperAdmin, Roles.Admin));
        options.AddPolicy(PolicyNames.AdminOrSalesRepresentative, policy => policy.RequireRole(Roles.Admin, Roles.SalesRepresentative));
        options.AddPolicy(PolicyNames.SalesRepresentativeOrUser, policy => policy.RequireRole(Roles.SalesRepresentative, Roles.User));
        options.AddPolicy(PolicyNames.SuperAdminOrAdminOrSalesRepresentativeOrUser, policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin, Roles.SalesRepresentative, Roles.User));
    }
}