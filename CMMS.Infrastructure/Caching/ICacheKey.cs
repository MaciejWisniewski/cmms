namespace CMMS.Infrastructure.Caching
{
    public interface ICacheKey<TItem>
    {
        string CacheKey { get; }
    }
}
