using ExchangeRedis.ExChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRedis.SmallExample
{
    internal class Oversold
    {
        //超卖小例子
        public void Show()
        {
            //模拟超卖
            using (RedisStringExChange exChange=new RedisStringExChange())
            {
                //写入10件商品
                exChange.SetString("Number", 10);
            }
            bool IsHave = true;
            //模拟500个人去抢
            for (int i = 0; i < 500; i++)
            {
                int k = i;
                Task.Run(() =>
                {
                    using (RedisStringExChange exChange = new RedisStringExChange())
                    {
                        if (IsHave)
                        {
                            double index = exChange.StringDecrement("Number");
                            if (index >= 0)
                            {
                                Console.WriteLine($"{k}秒杀商品成功，秒杀商品的索引为{index}");
                            }
                            else
                            {
                                if (IsHave)
                                {
                                    IsHave = false;
                                }
                                Console.WriteLine($"{k}秒杀商品是失败，秒杀商品的索引为{index}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{k}来的时候秒杀已经停止了！");
                        }
                    }
                });
            }
        }
    }
}
