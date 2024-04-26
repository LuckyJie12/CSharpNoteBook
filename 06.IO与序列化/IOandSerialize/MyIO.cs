using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IOandSerialize
{
    public class MyIO
    {
        static string FilePath = "C:\\Users\\Jie\\Desktop\\Desk\\1.txt";
        static string FilePath2 = "C:\\Users\\Jie\\Desktop\\Desk\\2.txt";
        public void Show()
        {
            //判断文件是否存在
            if (File.Exists(FilePath))
            {
                Console.WriteLine("存在！");
            }
            else
            {
                Console.WriteLine("不存在！");
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                sb.AppendLine(i.ToString());
            }
            File.WriteAllText(FilePath, sb.ToString());
            string Name = File.ReadAllText(FilePath, encoding: Encoding.UTF8);
            IEnumerable<string> lines = "Hello6World".Split('6');
            Console.WriteLine(string.Join(",", lines).ToString());
            File.AppendAllLines(FilePath2, lines);
            File.AppendAllText(FilePath2,"你好！");
            Console.WriteLine(Path.GetDirectoryName("C:\\Users\\Jie\\Desktop\\Desk"));
        }

    }
}
