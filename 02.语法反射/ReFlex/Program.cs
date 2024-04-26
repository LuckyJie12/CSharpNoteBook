using Animal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Animal.AndAnimals;

namespace ReFlex
{
    internal class Program
    {
        static void Main(string[] args)
        {

            {
                Assembly assembly = Assembly.Load("Animal");
                foreach (var item in assembly.GetModules())
                {
                    Console.WriteLine(item.FullyQualifiedName);
                }
                foreach (var type in assembly.GetTypes())
                {
                    Console.WriteLine(type.FullName);
                }
                Type type1 = assembly.GetType("Animal.AndAnimals+BirdS");
                object AllAM = Activator.CreateInstance(type1);
                //类型转换调用方法
                BirdS andAnimals = (BirdS)AllAM;
                andAnimals.MaQue();
            }
            //不进行类型转换利用反射调用方法
            {
                Assembly assembly = Assembly.Load("Animal");
                Type type = assembly.GetType("Animal.Show");
                //foreach (var item in type.GetMethods())
                //{
                //    Console.WriteLine(item.Name);
                //}
                object Obj = Activator.CreateInstance(type);
                {
                    //调用无参方法
                    MethodInfo methodInfo = type.GetMethod("Show1");
                    methodInfo.Invoke(Obj, null);
                }
                {
                    MethodInfo methodInfo = type.GetMethod("Show1", new Type[] { });
                    methodInfo.Invoke(Obj, new object[] { });
                }
                {
                    //调用有参方法
                    MethodInfo methodInfo = type.GetMethod("Show2");
                    methodInfo.Invoke(Obj, new object[] { 123 });
                }
                {
                    //调用有参方法
                    MethodInfo methodInfo = type.GetMethod("Show3");
                    methodInfo.Invoke(Obj, new object[] { "Jack", 123 });
                }
                {
                    //调用静态方法
                    MethodInfo methodInfo = type.GetMethod("Show5");
                    methodInfo.Invoke(Obj, new object[] { "Jack" });
                    //静态方法也可以这样
                    methodInfo.Invoke(null, new object[] { "Lucy" });
                }
                {
                    //调用私有方法
                    MethodInfo methodInfo = type.GetMethod("Show4", BindingFlags.Instance | BindingFlags.NonPublic);
                    methodInfo.Invoke(Obj, null);
                }
                {
                    //泛型类的调用
                    Assembly Gen = Assembly.Load("Animal");
                    Type type1 = assembly.GetType("Animal.Generic`1");
                    Type NewType = type1.MakeGenericType(new Type[] { typeof(int) });
                    Object O = Activator.CreateInstance(NewType);
                    //调用泛型方法
                    MethodInfo methodInfo = NewType.GetMethod("Gen");
                    MethodInfo NewMethod = methodInfo.MakeGenericMethod(new Type[] { typeof(DateTime), typeof(string) });
                    NewMethod.Invoke(O, new object[] { 123, DateTime.Now, "Jack" });
                }
            }
            {
                Console.WriteLine("-----------------------------------------------------------");
                People Peo = new People();
                Peo.ID = 1;
                Peo.Name = "test";
                Peo.Description = "test";
                Type type = typeof(People);
                object OPeople = Activator.CreateInstance(type);
                foreach (var Prop in type.GetProperties())
                {
                    Console.WriteLine(type.Name);
                    Console.WriteLine(Prop.Name);
                    Console.WriteLine(Prop.GetValue(OPeople));
                    if (Prop.Name == "ID")
                    {
                        Prop.SetValue(OPeople, 123);
                    }
                    else if (Prop.Name.Equals("Name"))
                    {
                        Prop.SetValue(OPeople, "Jack");
                    }
                }
                MethodInfo info = type.GetMethod("User");
                info.Invoke(OPeople, null);
            }
        }
    }
}
