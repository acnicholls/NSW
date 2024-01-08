namespace NSW.Data
{
	public class Cache
    {
		//private static System.Runtime.Caching.ObjectCache DataStore = MemoryCache.Default;

		private static Dictionary<string, object> DataStore = new();

		/// <summary>
		/// adds an object to the Data Cache
		/// </summary>
		/// <param name="key">key to locate object with</param>
		/// <param name="itemToInsert">object to insert</param>
		public static void Add(string key, object itemToInsert)
        {
            DataStore[key] = itemToInsert;
        }

        /// <summary>
        /// gets an item from the data store
        /// </summary>
        /// <param name="key">key of object to locate</param>
        /// <returns>requested object</returns>
        public static object Get(string key)
        {
            return DataStore[key];
        }

        /// <summary>
        /// removes an object from the data store
        /// </summary>
        /// <param name="key">key of object to remove</param>
        public static void Remove(string key)
        {
            DataStore.Remove(key);
        }
    }
}
