using ExchangeRedis.ExChange;
using ExchangeRedis.SmallExample;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRedis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                //Console.WriteLine("---------------------String Show-------------------------");
                //RedisStringExChange Client = new RedisStringExChange();
                //Client.SetString("Hello", "World");
                //Client.SetString("hello","dajiahao", TimeSpan.FromSeconds(5));
                //List<string> list = new List<string>()
                //{
                //    "Hello","name"
                //};
                //var Result= Client.GetString(list);
                ////自增
                //Client.StringIncrement("Number", 100);
                //Client.StringIncrement("Number",1);
                //Console.WriteLine(Client.GetString("Number"));
                //Client.Dispose();
            }
            {
                //Console.WriteLine("---------------------Hsah Show-------------------------");
                //RedisHashExChange Client = new RedisHashExChange();
                //Client.HashSet("HashTest", "Remark", "我是ID2");
                //Console.WriteLine(Client.HashExists("HashTest", "Remark"));
                //Client.HashRemove("HashTest", "Remark");
                //List<RedisValue> values = new List<RedisValue>()
                //{
                //    new RedisValue("ID"),
                //    new RedisValue("NA")
                //};
                ////Console.WriteLine(Client.HashRemove("HashTest", values));
                //Console.WriteLine(Client.HashGet("HashTest", "Name"));
                //var Result = Client.HashGetAll("HashTest");
                //var Keys = Client.HashKeys("HashTest");
            }
            {
                //Console.WriteLine("---------------------List Show-------------------------");
                //RedisListExChange Client = new RedisListExChange();
                ////Console.WriteLine(Client.ListRemove("ListTest", "Test2"));
                //var Result = Client.ListRange("ListTest");
                ////Console.WriteLine(Client.ListRightPush<string>("ListTest","Dest"));
                ////Console.WriteLine(Client.ListRightPop("ListTest"));
                //Console.WriteLine(Client.ListLength("ListTest"));
                //Client.ListLeftPush("ListTest", "index1");
                //var Result1 = Client.ListRange("ListTest",0,2);
            }
            {
                //Console.WriteLine("---------------------SortedSet Show-------------------------");
                //RedisSortedSetExChange Client = new RedisSortedSetExChange();
                ////Console.WriteLine(Client.SortedSetAdd("ZSetTest", "888", 1));
                ////Console.WriteLine(Client.SortedSetRemove("ZSetTest", "888"));
                //var Result = Client.SortedSetRangeByRank("ZSetTest");
                //Console.WriteLine(Client.SortedSetLength("ZSetTest"));
                //Client.KeyDelete()
            }
            {
                //Console.WriteLine("---------------------Set Show-------------------------");
                //RedisSetExChange Client = new RedisSetExChange();
                //Console.WriteLine(Client.SetLength("SetTest"));
                //var Result = Client.SetMembers("SetTest");
                ////Console.WriteLine(Client.SetRandomMember("SetTest"));
                //Client.SetAdd<string>("SetTest2", new List<string>() { "Set3", "Set4", "Set2" });
                //var Result2 = Client.SetCombineDifference("SetTest", "SetTest2");
                //var Result3 = Client.SetCombineAndStore("SetTest", "SetTest2","SetTest3");
            }
            {
                Console.WriteLine("---------------------例子 Show-------------------------");
                //超卖
                //new Oversold().Show();
                //买票
                new _12306BuyingTickets().Show();
                Task.WaitAll();
                Console.WriteLine("已经结束");
            }
            Console.ReadKey();
        }
    }
}
