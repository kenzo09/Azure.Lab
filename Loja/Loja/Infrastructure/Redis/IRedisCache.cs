namespace Loja.Infrastructure.Redis
{
    public interface IRedisCache
    {
        string Get(string key);
        void Set(string key, string value);
    }
}