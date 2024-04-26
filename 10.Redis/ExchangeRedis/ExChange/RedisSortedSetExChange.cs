using ExchangeRedis.Init;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRedis.ExChange
{
    internal class RedisSortedSetExChange:RedisBase
    {
        //对应Redis：Zset
        #region SortedSet 有序集合 同步方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">ZSet的Key</param>
        /// <param name="Value">Member</param>
        /// <param name="score">Score</param>
        /// <returns>是否添加成功</returns>
        public bool SortedSetAdd<T>(string key, T Value, double score)
        {
            //string Json = JsonConvert.SerializeObject(t);
            string Json = Value.ToString();
            return base.ClientRedis.SortedSetAdd(key, Json, score);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool SortedSetRemove<T>(string key, T Value)
        {
            string Json = Value.ToString();
            return base.ClientRedis.SortedSetRemove(key, Json);
        }
        /// <summary>
        /// 获取全部，根据Score排序
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public RedisValue[] SortedSetRangeByRank(string key)
        {
            //<T>标准是泛型写法
            //List<T> list = new List<T>();
            var Result = base.ClientRedis.SortedSetRangeByRank(key);
            //foreach (var item in Result)
            //{
            //    list.Add((T)Convert.ChangeType(item, typeof(T)));
            //}
            return Result;
        }
        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns>返回集合对应的数量</returns>
        public long SortedSetLength(string key)
        {
            return ClientRedis.SortedSetLength(key);
        }
        // 为数字增长val
        public double SortedSetIncrement(string key, string member, double val)
        {
            return ClientRedis.SortedSetIncrement(key, member, val);
        }
        // 为数字减少val
        public double SortedSetDecrement(string key, string member, double val)
        {
            return ClientRedis.SortedSetDecrement(key, member, val);
        }
        // 获取指定member的score
        public double? SortedSetScore(string key, string member)
        {
            return ClientRedis.SortedSetScore(key, member);
        }
        // 获取指定member的排名
        public long? SortedSetRank(string key, string member)
        {
            return ClientRedis.SortedSetRank(key, member);
        }
        //等等.....
        #endregion
        #region SortedSet异步方法
        //此处省略一万个字
        #endregion
    }
}
