using IOCTest.IOCInterface;
using IOCTest.IOCService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace IOCTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                Console.WriteLine("---------------------------------普通调用----------------------------------");
                VWCar car = new AudiCar();
                car.Run();
                AbstractPhone Android = new AndroidPhone();
                Android.Call();
                AbstractPhone Apple = new ApplePhone();
                Apple.Call();
            }
            {
                Console.WriteLine("---------------------------------简单工厂调用----------------------------------");
                AbstractPhone NoneType = FactoryCreate.Create<ApplePhone>();
                NoneType.Call();
            }
            #region IOC依赖注入
            {
                Console.WriteLine("---------------------------------Unity容器的初步应用一----------------------------------");
                IUnityContainer container = new UnityContainer();//生命一个容器
                container.RegisterType<ICar,VWCar>();//初始化容器
                ICar Car=container.Resolve<ICar>();//创建对象
                Car.Stop();
            }
            {
                Console.WriteLine("---------------------------------Unity容器的初步应用二----------------------------------");
                IUnityContainer container = new UnityContainer();//生命一个容器
                container.RegisterType<ICar, VWCar>();//接口
                container.RegisterType<AbstractPhone, ApplePhone>();//抽象
                //container.RegisterType<VWCar, AudiCar>();//父子,如果有多个父子关系，会递归到底
                container.RegisterType<AbstractPhone, ApplePhone>("Apple");//一对多
                container.RegisterType<AbstractPhone, AndroidPhone>("Android");//一对多
                //创建对象
                ICar Car = container.Resolve<ICar>();
                Car.Stop();
                AbstractPhone DefaultPhone=container.Resolve<AbstractPhone>();
                AbstractPhone Apple = container.Resolve<AbstractPhone>("Apple");
                AbstractPhone Android = container.Resolve<AbstractPhone>("Android");
            }
            {
                Console.WriteLine("---------------------------------依赖注入  对层架构----------------------------------");
                try
                {
                    IUnityContainer container = new UnityContainer();//生命一个容器
                    container.RegisterType<ICar, AudiCar>();
                    //创建对象
                    ICar Car = container.Resolve<ICar>();
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
            {
                Console.WriteLine("---------------------------------生命周期管理----------------------------------");
                IUnityContainer container = new UnityContainer();//生命一个容器
                //container.RegisterType<AbstractPhone, ApplePhone>(new TransientLifetimeManager());//瞬时模式
                //container.RegisterType<AbstractPhone, ApplePhone>(new ContainerControlledLifetimeManager());//单例模式
                //AbstractPhone Phone1 = container.Resolve<AbstractPhone>();
                //AbstractPhone Phone2 = container.Resolve<AbstractPhone>();
                ////确认两个实例是否相同（是否是同一个）
                //Console.WriteLine(object.ReferenceEquals(Phone1,Phone2));
                //线程单例模式（在同一线程同一单例，不同线程不同实例------风险很大）
                //举例有误
                container.RegisterType<AbstractPhone, ApplePhone>(new TransientLifetimeManager());
                List<Task> tasks = new List<Task>();
                AbstractPhone Phone1 = null;
                tasks.Add(Task.Run(() =>
                {
                    Phone1 = container.Resolve<AbstractPhone>();
                    Console.WriteLine($"Phone1是由线程：{Thread.CurrentThread.ManagedThreadId}");
                }));
                AbstractPhone Phone2 = null;
                AbstractPhone Phone3 = null;
                tasks.Add(Task.Run(() =>
                {
                    Phone2 = container.Resolve<AbstractPhone>();
                    Phone3 = container.Resolve<AbstractPhone>();
                    Console.WriteLine($"Phone2是由线程：{Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"Phone3是由线程：{Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"object.ReferenceEquals(Phone2, Phone3)={object.ReferenceEquals(Phone3, Phone2)}");
                }));
                Task.WaitAll(tasks.ToArray());
                Console.WriteLine($"object.ReferenceEquals(Phone1, Phone2)={object.ReferenceEquals(Phone1, Phone2)}");
                
            }
            #endregion
        }
    }
}
