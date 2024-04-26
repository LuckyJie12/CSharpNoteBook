using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRedis.Init
{
    internal class RedisHelper
    {
        #region Redis
        //Redis
        //Redis是一个开源的使用ANSI C语言编写、遵守BSD协议、支持网络、可基于内存亦可持久化的日志型、Key-Value数据库，并提供多种语言的API。
        //Redis 与其他 key - value 缓存产品有以下三个特点：
        //1.支持数据的持久化
        //2.原子性
        //3.丰富的数据类型
        //Redis支持的数据类型有：
        //1.String
        //2.List
        //3.Set
        //4.Hash
        //5.Sorted Set
        //6.BitMap
        //7.HyperLogLog
        //8.Stream
        //9.Geo
        //10.其他
        #endregion
        /// <summary>
        /// 创建Redis连接，返回DB0
        /// 全局唯一实例
        /// </summary>
        protected static ConnectionMultiplexer ConnRedis = null;
        public RedisHelper()
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["RedisExchangeHosts"].ConnectionString;
            if (ConnRedis == null)
            {
                ConnRedis = ConnectionMultiplexer.Connect(ConnStr);
            }
        }
        public IDatabase GetRedisClient()
        {
            return ConnRedis.GetDatabase(0);
        }
        public ConnectionMultiplexer GetConnectionMultiplexer()
        {
            return ConnRedis;
        }
    }
}
