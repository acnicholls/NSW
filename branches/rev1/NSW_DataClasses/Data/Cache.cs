using System.Runtime.Caching;

namespace NSW.Data
{
    public class Cache
    {
        private static System.Runtime.Caching.ObjectCache DataStore = MemoryCache.Default;

        public static void Add(string key, object itemToInsert)
        {
            DataStore[key] = itemToInsert;
        }

        public static object Get(string key)
        {
            return DataStore[key];
        }

        public static void Remove(string key)
        {
            DataStore.Remove(key);
        }
    }
}
