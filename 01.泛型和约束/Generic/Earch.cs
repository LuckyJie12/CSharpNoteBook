using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Generic
{
    public class Earch
    {
        public interface IDog
        {
            void ShowMe();
        }
        public class People: IDog
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public void Hi()
            {
                Console.WriteLine("Hello World!");
            }

            public void ShowMe()
            {
                Console.WriteLine("Look At Me!");
            }
        }

        public class Chinese : People
        {
            public void China()
            {
                Console.WriteLine("我们都是中国人");
            }
        }
        public class HeNan : People
        {
            public void HuLaTanf()
            {
                Console.WriteLine("我们的代表是胡辣汤！");
            }
        }

        public class Japnese : IDog
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public void Dogs()
            {
                Console.WriteLine("我们是狗屎！");
            }
            public void ShowMe()
            {
                Console.WriteLine("我是一直狗！");
            }
        }
    }
}
