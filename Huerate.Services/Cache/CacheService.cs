/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using Microsoft.ApplicationServer.Caching;

namespace Huerate.Services.Cache
{
    internal class CacheService : ICacheService
    {
        private DataCache DataCache { get; set; }

        public CacheService(DataCache dataCache)
        {
            DataCache = dataCache;
        }

        public void Add(string key, object value)
        {
            DataCache.Add(key, value);
        }

        public object Get(string key)
        {
            return DataCache.Get(key);
        }

        public bool Remove(string key)
        {
            return DataCache.Remove(key);
        }
    }
}