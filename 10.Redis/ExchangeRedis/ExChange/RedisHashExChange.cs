using ExchangeRedis.Init;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRedis.ExChange
{
    internal class RedisHashExChange:RedisBase
    {
        #region Hash同步方法
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="dataKey">数据Key</param>
        /// <returns></returns>
        public bool HashExists(string key, string dataKey)
        {
            return base.ClientRedis.HashExists(key, dataKey);
        }
        // 存储数据到hash表
        public bool HashSet<T>(string key, string dataKey, T t)
        {
            string json = JsonConvert.SerializeObject(t);
            return base.ClientRedis.HashSet(key, dataKey, json);
        }
        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashRemove(string key, string dataKey)
        {
            return base.ClientRedis.HashDelete(key, dataKey);
        }
        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public long HashRemove(string key, List<RedisValue> dataKeys)
        {
            return base.ClientRedis.HashDelete(key, dataKeys.ToArray());
        }
        /// <summary>
        /// 从hash表获取某个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public string HashGet(string key, string dataKey)
        {
            string value = base.ClientRedis.HashGet(key, dataKey);
            if (string.IsNullOrEmpty(value))
            {
                return default(string);
            }
            return value;
        }
        /// <summary>
        /// 从hash表获取Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public T HashGet<T>(string key, string dataKey)
        {
            string value = base.ClientRedis.HashGet(key, dataKey);
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }
        /// <summary>
        /// 从hash表获取List值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public RedisValue[] HashGet(string key, List<RedisValue> dataKeys)
        {
            RedisValue[] values = base.ClientRedis.HashGet(key, dataKeys.ToArray());
            return values;
        }
        /// <summary>
        /// 从hash表获取所有值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object HashGetAll(string key)
        {
            return base.ClientRedis.HashValues(key);
        }
        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public double HashIncrement(string key, string dataKey, double val = 1)
        {
            return base.ClientRedis.HashIncrement(key, dataKey, val);
        }
        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public double HashDecrement(string key, string dataKey, double val = 1)
        {
            return base.ClientRedis.HashDecrement(key, dataKey, val);
        }
        // 获取hashkey所有Redis key
        public RedisValue[] HashKeys(string key)
        {
            return base.ClientRedis.HashKeys(key);
        }
        #endregion
        #region Hash异步方法
        //此处省略一万个字
        #endregion
    }
}
