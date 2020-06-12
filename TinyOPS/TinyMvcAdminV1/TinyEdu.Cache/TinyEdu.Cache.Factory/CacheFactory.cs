using TinyEdu.Cache.Interface;
using TinyEdu.MemoryCache;

namespace TinyEdu.Cache.Factory
{
    public class CacheFactory
    {
        public static ICache Cache()
        {
            return new MemoryCacheImp();
        }
    }
}
