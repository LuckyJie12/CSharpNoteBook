using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EncryptionAndDecryption
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MD5Extend mD5Extend = new MD5Extend();
            //mD5Extend.Show();
            RSAExtend rsaExtend = new RSAExtend();
            rsaExtend.Show();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("红色");
            Console.WriteLine("Hello World");
            Console.ForegroundColor = ConsoleColor.Black;
            Show();
            Console.WriteLine("---------------------");
            Start += () => Console.WriteLine("找到一个门！");
            Show();
        }
        public static event Action Start;
        public static void Show()
        {
            Console.WriteLine("电影开始了！");
            Console.WriteLine("小铃芽出场了！");
            Start?.Invoke();
            Console.WriteLine("地震出现了！");
            Console.WriteLine("地震解除了！");
            Console.WriteLine("结束！");
        }
    }
}
