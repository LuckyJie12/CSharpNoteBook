using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Structure.Program;

namespace Structure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new ShowArray().Show();
            List<string> NameList = new List<string>()
            {
                "张三","李四","王五","赵六","张三丰","张巨鹿","张伟","王帅","Jack","杰克","孙悟空","阿凡达","玲芽","李星云","旺仔","焊缝","萧炎","萧薰儿","美杜莎","海波东",
            };
            {
                //延迟读取，需要一个查找一个
                IEnumerable<string> Names = NameList.Where(n => n.Contains("张"));
                //foreach (string name in Names)
                //{
                //    Console.WriteLine(name);
                //}
                IEnumerable<string> Names2 = NameList.Where(n => n.Contains("张"));
                //foreach (string name in Names2)
                //{
                //    Console.WriteLine(name);
                //}
            }
            KFC[] kfcList = new KFC[]
            {
                new KFC() { Name = "KFC1", Address = "Address1" },
                new KFC() { Name = "KFC2", Address = "Address2" },
                new KFC() { Name = "KFC3", Address = "Address3" },
                new KFC() { Name = "KFC4", Address = "Address4" },
                new KFC() { Name = "KFC5", Address = "Address5" }
            };

            IETable<KFC> kfcTable = new IETable<KFC>(kfcList);
            while (kfcTable.MoveNext())
            {
                KFC kFC = kfcTable.Current;
                Console.WriteLine(kFC.Name);
            }
            string CX=Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine(CX);
        }
        public class KFC
        {
            public string Name { get; set; }
            public string Address { get; set; }
        }
        public class IETable<T>
        {
            private T[] _FoodList = null;
            public IETable(T[] ListT)
            {
                _FoodList = ListT;
            }
            private int _FoodCount = -1;
            public T Current
            {
                get
                {
                    return _FoodList[_FoodCount];
                }
            }
            public bool MoveNext()
            {
                return this._FoodList.Length > ++_FoodCount;
            }
            public void Reset()
            {
                _FoodCount = -1;
            }
        }
    }
}
