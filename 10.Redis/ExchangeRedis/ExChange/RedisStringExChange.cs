using ExchangeRedis.Init;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace ExchangeRedis.ExChange
{
    internal class RedisStringExChange : RedisBase
    {
        #region String同步方法
        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        public void SetString(string key, string value)
        {
            base.ClientRedis.StringSet(key, value);
        }
        /// <summary>
        /// 追加value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AppendString(string key, string value)
        {
            base.ClientRedis.StringAppend(key, value);
        }
        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="timeSpan">过期时间</param>
        public void SetString(string key, string value, TimeSpan timeSpan)
        {
            base.ClientRedis.StringSet(key, value, timeSpan);
        }
        /// <summary>
        /// 保存多个key value
        /// </summary>
        /// <param name="keyValues"></param>
        public void SetString(Dictionary<string, string> keyValues)
        {
            List<KeyValuePair<RedisKey, RedisValue>> list = new List<KeyValuePair<RedisKey, RedisValue>>();
            foreach (var item in keyValues)
            {
                list.Add(new KeyValuePair<RedisKey, RedisValue>(item.Key, item.Value));
            }
            base.ClientRedis.StringSet(list.ToArray());
        }
        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="timeSpan"></param>
        public void SetString<T>(string key, T t, TimeSpan? timeSpan = default(TimeSpan?))
        {
            string json =JsonConvert.SerializeObject(t);
            base.ClientRedis.StringSet(key, json,timeSpan);
        }
        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public string GetString(string key)
        {
            return base.ClientRedis.StringGet(key);
        }
        /// <summary>
        /// 获取多个key的值
        /// </summary>
        /// <param name="redisKeys"></param>
        /// <returns></returns>
        public List<RedisValue> GetString(List<string> redisKeys)
        {
            RedisKey[] Keys = redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray();
            return base.ClientRedis.StringGet(Keys).ToList();
        }

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public double StringIncrement(string key, double val = 1)
        {
            return base.ClientRedis.StringIncrement(key, val);
        }
        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public double StringDecrement(string key, double val = 1)
        {
            return base.ClientRedis.StringDecrement(key, val);
        }
        #endregion
        #region String异步方法
        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        public async void SetStringAsync(string key, string value)
        {
           await base.ClientRedis.StringSetAsync(key, value);
        }
        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="timeSpan">过期时间</param>
        public async void SetStringAsync(string key, string value, TimeSpan timeSpan)
        {
            await base.ClientRedis.StringSetAsync(key, value, timeSpan);
        }
        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public async Task<string> GetStringAsync(string key)
        {
            return await base.ClientRedis.StringGetAsync(key);
        }
        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public async Task<double> StringIncrementAsync(string key, double val = 1)
        {
            return await base.ClientRedis.StringIncrementAsync(key, val);
        }
        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public async Task<double> StringDecrementAsync(string key, double val = 1)
        {
            return await base.ClientRedis.StringDecrementAsync(key, val);
        }
        #endregion
    }
}
