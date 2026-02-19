namespace PMS.Utilities;

public static class Roles
{
    public const string Admin = "Admin";
    public const string Agent = "Agent";
    public const string Tenant = "Tenant";

    public static readonly string[] AllRoles = { Admin, Agent, Tenant };
}
