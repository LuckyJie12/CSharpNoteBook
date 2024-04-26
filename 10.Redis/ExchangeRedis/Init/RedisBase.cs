using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRedis.Init
{
    //IDisposable接口是用于支持清理非托管资源的一种机制
    internal class RedisBase : IDisposable
    {
        public IDatabase ClientRedis { get; private set; }
        public ConnectionMultiplexer ClientConn { get; private set; }
        /// <summary>
        /// 构造时打开链接
        /// </summary>
        public RedisBase()
        {
            if (ClientRedis == null)
            {
                RedisHelper redis = new RedisHelper();
                ClientRedis = redis.GetRedisClient();
                ClientConn=redis.GetConnectionMultiplexer();
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ClientRedis = null;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);//避免执行不必要的清理操作
        }
        /// <summary>
        /// 根据Key删除RedisKey
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool KeyDelete(string Key)
        {
            return ClientRedis.KeyDelete(Key);
        }
        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys">rediskey</param>
        /// <returns>成功删除的个数</returns>
        public long KeyDelete(List<string> redisKeys)
        {
            RedisKey[] Keys = redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray();
            return ClientRedis.KeyDelete(Keys);
        }
        /// <summary>
        /// 判断key是否存储
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            return ClientRedis.KeyExists(key);
        }
        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="key">就的redis key</param>
        /// <param name="newKey">新的redis key</param>
        /// <returns></returns>
        public bool KeyRename(string key, string newKey)
        {
            return ClientRedis.KeyRename(key, newKey);
        }
        /// <summary>
        /// 设置Key的时间
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, TimeSpan? expiry = default(TimeSpan?))
        {
            return ClientRedis.KeyExpire(key, expiry);
        }
        #region 发布订阅
        /// <summary>
        /// Redis发布订阅  订阅
        /// </summary>
        /// <param name="subChannel"></param>
        /// <param name="handler"></param>
        public void Subscribe(string subChannel, Action<RedisChannel, RedisValue> handler = null)
        {
            ISubscriber sub = ClientConn.GetSubscriber();
            sub.Subscribe(subChannel, (channel, message) =>
            {
                //接收订阅消息，处理逻辑
                if (handler == null)
                {
                    Console.WriteLine(subChannel + " 订阅收到消息：" + message);
                }
                else
                {
                    handler(channel, message);
                }
            });
            //写法二
            sub.Subscribe(subChannel, new Action<RedisChannel, RedisValue>((channel, message) =>
            {
                Console.WriteLine(subChannel + " 订阅收到消息：" + message);
            }));
            //写法三
            sub.Subscribe(subChannel, new Action<RedisChannel, RedisValue>(GetMessage));
        }

        /// <summary>
        /// 获取订阅消息（写法三）
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        static void GetMessage(RedisChannel channel, RedisValue message)
        {
            Console.WriteLine(channel + " 订阅收到消息：" + message);
        }

        /// <summary>
        /// Redis发布订阅  发布
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public long Publish<T>(string channel, T msg)
        {
            ISubscriber sub = ClientConn.GetSubscriber();
            return sub.Publish(channel, msg.ToString());
        }

        /// <summary>
        /// Redis发布订阅  取消订阅
        /// </summary>
        /// <param name="channel"></param>
        public void Unsubscribe(string channel)
        {
            ISubscriber sub = ClientConn.GetSubscriber();
            sub.Unsubscribe(channel);
        }

        /// <summary>
        /// Redis发布订阅  取消全部订阅
        /// </summary>
        public void UnsubscribeAll()
        {
            ISubscriber sub = ClientConn.GetSubscriber();
            sub.UnsubscribeAll();
        }
        #endregion 发布订阅
    }
}
