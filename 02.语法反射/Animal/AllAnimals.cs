using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal
{
    public class AndAnimals
    {
        public class BirdS
        {
            public void MaQue()
            {
                Console.WriteLine("我是小麻雀");
            }
        }
        public void Dogs()
        {
            Console.WriteLine("小狗汪汪叫！！！");
        }
        public void Cats()
        {
            Console.WriteLine("小猫喵喵叫！！！");
        }
    }
    public class Show
    {
        public void Show1()
        {
            Console.WriteLine("我是显示1");
        }
        public void Show2(int Nums)
        {
            Console.WriteLine($"我是有参显示2，参数是{Nums}");
        }
        public void Show3(string Name, int ID)
        {
            Console.WriteLine($"我叫{Name}，我的编号是{ID}");
        }
        private void Show4()
        {
            Console.WriteLine("我是私有方法Show4！");
        }
        public static void Show5(string Name)
        {
            Console.WriteLine($"我是静态有参，我是：{Name}");
        }
    }
    public class Generic<T>
    {
        public void Gen<M, S>(T t, M m, S s)
        {
            Console.WriteLine($"M的类型：{m.GetType().Name}，值是：{m},T的类型：{t.GetType().Name},值是：{t},S的类型：{s.GetType().Name},值是：{s}");
        }
    }
    public class People
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description;
        public void User()
        {
            Console.WriteLine("lalla");
        }
    }
}
