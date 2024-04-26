using IOCTest.IOCInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCTest.IOCService
{
    public class BMWCar : ICar
    {
        public void Run()
        {
            Console.WriteLine($"{this.GetType().Name}开动了");
        }

        public void SpeedDown()
        {
            Console.WriteLine($"{this.GetType().Name}减速了");
        }

        public void SpeedUp()
        {
            Console.WriteLine($"{this.GetType().Name}加速了");
        }

        public void Stop()
        {
            Console.WriteLine($"{this.GetType().Name}停下了");
        }
    }
}
