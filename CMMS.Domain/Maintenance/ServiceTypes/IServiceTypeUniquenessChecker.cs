namespace CMMS.Domain.Maintenance.ServiceTypes
{
    public interface IServiceTypeUniquenessChecker
    {
        bool IsUnique(string typeName);
    }
}
