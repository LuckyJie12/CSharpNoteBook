using ExchangeRedis.ExChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeRedis.SmallExample
{
    internal class _12306BuyingTickets
    {
        //先进先出
        public void Show()
        {
            //模拟客户端点击买票
            //订单量一直增加
            #region 客户端
            Task.Run(() =>
            {
                int k = 1;
                using (RedisListExChange redisListEx = new RedisListExChange())
                {
                    while (true)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            redisListEx.ListRightPush("12306", i);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine($"第{k}轮，添加任务{i}");
                        }
                        Thread.Sleep(100);//停一秒添加10条任务
                        k++;
                    }
                }
            });
            Task.Run(() =>
            {
                int k = 1;
                using (RedisListExChange redisListEx = new RedisListExChange())
                {
                    while (true)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            redisListEx.ListRightPush("12306", i);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine($"第{k}轮，添加任务{i}");
                        }
                        Thread.Sleep(100);//停一秒添加10条任务
                        k++;
                    }
                }
            });
            #endregion
            //模拟服务端处理买票信息
            Task.Run(() =>
            {
                while (true)
                {
                    using (RedisListExChange redisListEx = new RedisListExChange())
                    {
                        var Result = redisListEx.ListLeftPop("12306");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"完成任务{Result}");
                    }
                    Thread.Sleep(5);
                    //    for (int i = 0; i < 10; i++)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Blue;
                    //        Console.WriteLine("11111");
                    //    }
                    //Thread.Sleep(100);
                }
            });
        }
    }
}
