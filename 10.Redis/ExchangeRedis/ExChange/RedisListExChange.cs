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
    internal class RedisListExChange:RedisBase
    {
        #region List同步方法
        /// <summary>
        /// 移除指定ListId的内部List的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="value">Value值</param>
        /// <returns></returns>
        public long ListRemove<T>(string key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            string json2 = value.ToString();
            return base.ClientRedis.ListRemove(key, json2);
        }
        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> ListRange(string key)
        {
            List<string> list = new List<string>();
            var list2 = base.ClientRedis.ListRange(key);
            foreach (var item in list2)
            {
                //list.Add(JsonConvert.DeserializeObject<T>(item));
                list.Add(item.ToString());
            }
            return list;
        }
        /// <summary>
        /// 入队,从列表右边插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>返回数据数</returns>
        public long ListRightPush<T>(string key, T value)
        {
            //string json = JsonConvert.SerializeObject(value);
            string str = value.ToString();
            return base.ClientRedis.ListRightPush(key, str);
        }
        /// <summary>
        /// 出队,从列表右边取出并删除取出的内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns>返回取出的内容</returns>
        public string ListRightPop(string key)
        {
            return base.ClientRedis.ListRightPop(key);
        }
        /// <summary>
        /// 入栈 从数据左边插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>返回数据数</returns>
        public long ListLeftPush(string key, string value)
        {
            return base.ClientRedis.ListLeftPush(key, value);
        }
        /// <summary>
        /// 出栈 从数据左边取出并删除取出的内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns>返回取出的内容</returns>
        public string ListLeftPop(string key)
        {
            return base.ClientRedis.ListLeftPop(key);
        }
        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns>返回Key对应数据量</returns>
        public long ListLength(string key)
        {
            return base.ClientRedis.ListLength(key);
        }
        /// <summary>
        /// 找出开始下标，到结束下标的List
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public List<string> ListRange(string key, long start, long stop)
        {
            List<string> list = new List<string>();
            var list2 = base.ClientRedis.ListRange(key, start, stop);
            foreach (var item in list2)
            {
                //list.Add(JsonConvert.DeserializeObject<T>(item));
                list.Add(item.ToString());
            }
            return list;
        }
        #endregion
        #region List异步方法
        //此处省略一万个字
        #endregion
    }
}
