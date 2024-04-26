using IOCTest.IOCInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCTest.IOCService
{
    public class ApplePhone : AbstractPhone
    {
        public override void Call()
        {
            Console.WriteLine($"使用{this.GetType().Name}打电话！！");
        }

        public override void PlayGame()
        {
            Console.WriteLine($"使用{this.GetType().Name}玩游戏！！");
        }

        public override void Text()
        {
            Console.WriteLine($"使用{this.GetType().Name}发短信！！");
        }
    }
}
