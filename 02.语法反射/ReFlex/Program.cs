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
            //需要类型转换调用方法
            {
                // 加载名为 "Animal" 的程序集
                Assembly assembly = Assembly.Load("Animal");
                // 遍历程序集中的所有模块，并打印每个模块的完全限定名
                foreach (var item in assembly.GetModules())
                {
                    Console.WriteLine(item.FullyQualifiedName);
                }
                // 遍历程序集中的所有类型，并打印每个类型的完全限定名
                foreach (var type in assembly.GetTypes())
                {
                    Console.WriteLine(type.FullName);
                }
                // 获取名为 "Animal.AndAnimals+BirdS" 的类型
                // 注意：+号表示这是一个嵌套类型，BirdS是AndAnimals类中定义的一个嵌套类
                Type GetType = assembly.GetType("Animal.AndAnimals+BirdS");
                // 使用Activator.CreateInstance动态创建上面获取的类型的实例
                object AllAM = Activator.CreateInstance(GetType);
                // 将创建的对象转换为具体的类型 BirdS，以便可以调用其方法
                BirdS andAnimals = (BirdS)AllAM;
                // 调用BirdS类型的实例的MaQue方法
                andAnimals.MaQue();
            }
            //不进行类型转换利用反射调用方法
            {
                Assembly assembly = Assembly.Load("Animal");
                Type type = assembly.GetType("Animal.Show");
                foreach (var item in type.GetMethods())
                {
                    Console.WriteLine(item.Name);
                }
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
                    Object Ject = Activator.CreateInstance(NewType);
                    //调用泛型方法
                    MethodInfo methodInfo = NewType.GetMethod("Gen");
                    MethodInfo NewMethod = methodInfo.MakeGenericMethod(new Type[] { typeof(DateTime), typeof(string) });
                    NewMethod.Invoke(Ject, new object[] { 123, DateTime.Now, "Jack" });
                }
            }
            {
                // 定义People类的实例并初始化
                People Peo = new People();
                Peo.ID = 1;  // 设置ID属性
                Peo.Name = "test";  // 设置Name属性
                Peo.Description = "test";  // 设置Description属性
                // 获取People类的Type对象
                Type type = typeof(People);
                // 使用反射创建People类的新实例
                object OPeople = Activator.CreateInstance(type);
                // 遍历People类的所有属性
                foreach (var Prop in type.GetProperties())
                {
                    // 打印属性名
                    Console.WriteLine(Prop.Name);
                    // 打印属性的当前值（新创建的实例的初始值）
                    Console.WriteLine(Prop.GetValue(OPeople));

                    // 如果属性名为"ID"，则设置该属性的值为123
                    if (Prop.Name == "ID")
                    {
                        Prop.SetValue(OPeople, 123);
                    }
                    // 如果属性名为"Name"，则设置该属性的值为"Jack"
                    else if (Prop.Name.Equals("Name"))
                    {
                        Prop.SetValue(OPeople, "Jack");
                    }
                }
                // 获取People类的名为"User"的方法的MethodInfo对象
                MethodInfo info = type.GetMethod("User");
                // 调用OPeople实例的"User"方法
                info.Invoke(OPeople, null);
            }
        }
    }
}
