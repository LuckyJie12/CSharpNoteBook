using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Generic
{
    public class Generic
    {
        public static void Show()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(Generic<int>.GetCache());
                Thread.Sleep(10);
                Console.WriteLine(Generic<long>.GetCache());
                Thread.Sleep(10);
                Console.WriteLine(Generic<string>.GetCache());
                Thread.Sleep(10);
                Console.WriteLine(Generic<DateTime>.GetCache());
                Thread.Sleep(10);
                Console.WriteLine(Generic<Generic>.GetCache());
                Thread.Sleep(10);
            }
        }
    }
    //泛型的缓存，适合不同类型缓存一份数据
    public class Generic<T>
    {
        static  Generic()
        {
            Console.WriteLine( "这是一个静态构造函数！");
            _TypeTime = string.Format("{0}-{1}", DateTime.Now, typeof(T).FullName);
        }
        private static string _TypeTime;

        public static string GetCache()
        {
            return _TypeTime;
        }
    }
}
