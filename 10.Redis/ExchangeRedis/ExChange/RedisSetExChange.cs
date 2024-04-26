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
    internal class RedisSetExChange:RedisBase
    {
        #region Set 集合 同步方法
        /// <summary>
        /// 删除单个Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool SetRemove<T>(string key, T Value)
        {
            string Json = Value.ToString();
            return ClientRedis.SetRemove(key, Json);
        }
        /// <summary>
        /// 删除多个Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="Values"></param>
        /// <returns></returns>
        public long SetRemove<T>(string key, List<T> Values)
        {
            List<RedisValue> list = new List<RedisValue>();
            foreach (var item in Values)
            {
                list.Add(item.ToString());
            }
            return ClientRedis.SetRemove(key, list.ToArray());
        }
        /// <summary>
        /// 添加单个Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool SetAdd<T>(string key, T Value)
        {
            string Json = Value.ToString();
            return ClientRedis.SetAdd(key, Json);
        }
        /// <summary>
        /// 添加多个Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="Values"></param>
        /// <returns></returns>
        public long SetAdd<T>(string key, List<T> Values)
        {
            List<RedisValue> list = new List<RedisValue>();
            foreach (var item in Values)
            {
                list.Add(item.ToString());
            }
            return ClientRedis.SetAdd(key, list.ToArray());
        }
        /// <summary>
        /// 获取集合中所有的Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> SetMembers(string key)
        {
            var Result = ClientRedis.SetMembers(key);
            List<string> list = new List<string>();
            foreach (var item in Result)
            {
                list.Add(item.ToString());
            }
            return list;
        }
        /// <summary>
        /// 判断Value是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool SetContains<T>(string key, T Value)
        {
            string Json = Value.ToString();
            return ClientRedis.SetContains(key, Json);
        }
        /// <summary>
        /// 获取集合中Value的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long SetLength(string key)
        {
            return ClientRedis.SetLength(key);
        }
        /// <summary>
        /// 随机获取集合中的一个Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public string SetRandomMember(string key)
        {
            var Result = ClientRedis.SetRandomMember(key);
            //return JsonConvert.DeserializeObject<T>(Result);
            return Result.ToString();
        }
        /// <summary>
        /// 随机获取集合中的多个Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<T> SetRandomMembers<T>(string key, int count)
        {
            var Result = ClientRedis.SetRandomMembers(key, count);
            List<T> list = new List<T>();
            foreach (var item in Result)
            {
                list.Add(JsonConvert.DeserializeObject<T>(item));
            }
            return list;
        }
        /// <summary>
        /// 交叉
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns>返回交叉的值</returns>
        public List<string> SetCombine(string key1, string key2)
        {
            var Result = ClientRedis.SetCombine(SetOperation.Intersect, key1, key2);
            List<string> list = new List<string>();
            foreach (var item in Result)
            {
                list.Add(item);
            }
            return list;
        }
        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        public List<string> SetCombineUnion(string key1, string key2)
        {
            var Result = ClientRedis.SetCombine(SetOperation.Union, key1, key2);
            List<string> list = new List<string>();
            foreach (var item in Result)
            {
                list.Add(item);
            }
            return list;
        }
        /// <summary>
        /// Key1减去Key2
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns>返回Key1有而Key2没有的值</returns>
        public List<string> SetCombineDifference(string key1, string key2)
        {
            var Result = ClientRedis.SetCombine(SetOperation.Difference, key1, key2);
            List<string> list = new List<string>();
            foreach (var item in Result)
            {
                list.Add(item);
            }
            return list;
        }
        /// <summary>
        /// 计算交叉值并创建新的Key
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <param name="key3"></param>
        /// <returns>返回新的Key的Length</returns>
        public long SetCombineAndStore(string key1, string key2, string key3)
        {
            return ClientRedis.SetCombineAndStore(SetOperation.Intersect, key3, key1, key2);
        }

        #endregion
        #region Set 集合 异步方法
        //.......
        #endregion
    }
}
