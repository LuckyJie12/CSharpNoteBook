using CSharpOOP.Inherit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpOOP.Phone
{
    public class IOS:InterFace
    {
        public override void System()
        {
            Console.WriteLine($"这是在{this.GetType().Name} System is IOS！");
        }
        public new void Sound()
        {
            Console.WriteLine("Dog is barking.");
        }
    }
}
