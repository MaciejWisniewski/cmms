namespace CMMS.Domain.Identity
{
    public interface IRoleValidator
    {
        string GetValidOrDefault(string roleName);
    }
}
