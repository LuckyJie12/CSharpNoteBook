using CSharpOOP.Inherit;
using CSharpOOP.Phone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpOOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //封装：指将数据和行为进行封装，防止外部程序直接访问类内部的数据，只能通过类的方法进行访问。
            //特点：这种封装可以有效地保护类的数据，使其不会被错误地修改或破坏。
            {
                //继承：指一个类可以继承另一个类的属性和方法，并且可以增加自己的新特性。
                //特点：继承可以提高代码的复用性，并且使代码更具有结构性和层次性。
                InterFace And = new Android();
                And.UseCall();
                And.ID = 1;
                Android And2 = new Android();
                Console.WriteLine("----------------");
                Console.WriteLine(And2.Sop);
                Console.WriteLine("----------------");
                IOS iOS = new IOS();
                iOS.UseCall();
                iOS.System();
            }
            {
                Console.WriteLine("---------------多态----------------");
                //多态：指同一种类型的对象在不同的情况下可以具有不同的表现形式和行为方式。
                //特点：多态可以使代码更灵活，更容易扩展，同时也可以提高代码的可读性和可维护性。
                InterFace Or = new Android();
                Or.System();
            }
            {
                Console.WriteLine("---------------虚方法----------------");
                InterFace Or = new Android();
                Or.System();
                Or.Sound();
                IOS iOS = new IOS();
                iOS.Sound();
            }
        }
    }
}
