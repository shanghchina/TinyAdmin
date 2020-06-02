using System;
using System.Collections.Generic;

namespace Tiny.Common.Web
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface IMemoryCacheManager
    {
        /// <summary>
        /// 验证缓存项是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        T Get<T>(string key) where T : class;

        /// <summary>
        /// 根据Key获取缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        /// 获取所有缓存键
        /// </summary>
        /// <returns></returns>
        List<string> GetCacheKeys();

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存value</param>
        /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        void Insert(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存value</param>
        /// <param name="expiresIn">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        void Insert(string key, object value, TimeSpan expiresIn, bool isSliding = false);

        /// <summary>
        /// 根据key删除缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        void Remove(string key);

        /// <summary>
        /// 删除所有缓存
        /// </summary>
        void RemoveCacheAll();

    }
}
