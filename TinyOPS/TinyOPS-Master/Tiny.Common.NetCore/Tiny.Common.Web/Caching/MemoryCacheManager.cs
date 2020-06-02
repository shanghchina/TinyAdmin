using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Tiny.Common.Web
{
    /// <summary>
    /// 缓存实现类
    /// </summary>
    public class MemoryCacheManager : IMemoryCacheManager
    {
        private static readonly MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());

        #region 验证缓存是否存在
        /// <summary>
        /// 验证缓存是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return Cache.TryGetValue(key, out _);
        }
        #endregion 验证缓存是否存在

        #region 获取缓存
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public T Get<T>(string key) where T : class
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return Cache.Get(key) as T;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public object Get(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return Cache.Get(key);
        }

        /// <summary>
        /// 获取所有缓存键
        /// 没有查询全部键(key) 的扩展，要想查询可通过反射查找
        /// </summary>
        /// <returns></returns>
        public List<string> GetCacheKeys()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var entries = Cache.GetType().GetField("_entries", flags).GetValue(Cache);
            var cacheItems = entries as IDictionary;
            var keys = new List<string>();
            if (cacheItems == null) return keys;
            foreach (DictionaryEntry cacheItem in cacheItems)
            {
                keys.Add(cacheItem.Key.ToString());
            }
            return keys;
        }
        #endregion 获取缓存

        #region 添加缓存
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存value</param>
        /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        public void Insert(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Cache.Set(key, value, new MemoryCacheEntryOptions().SetSlidingExpiration(expiresSliding).SetAbsoluteExpiration(expiressAbsoulte));

        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存value</param>
        /// <param name="expiresIn">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        public void Insert(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Cache.Set(key, value, isSliding ? new MemoryCacheEntryOptions().SetSlidingExpiration(expiresIn) : new MemoryCacheEntryOptions().SetAbsoluteExpiration(expiresIn));

        }
        #endregion 添加缓存

        #region 删除缓存
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        public void Remove(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            Cache.Remove(key);
        }

        /// <summary>
        /// 删除所有缓存
        /// </summary>
        /// <returns></returns>
        public void RemoveCacheAll()
        {
            var l = GetCacheKeys();
            foreach (var s in l)
            {
                Remove(s);
            }
        }
        #endregion 删除缓存
    }
}
