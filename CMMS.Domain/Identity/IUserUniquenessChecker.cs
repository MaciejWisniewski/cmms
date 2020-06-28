namespace CMMS.Domain.Identity
{
    public interface IUserUniquenessChecker
    {
        bool IsUnique(string userName, string email);
    }
}
