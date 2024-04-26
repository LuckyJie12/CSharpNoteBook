using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;

namespace IOCTest.IOCService
{
    public class AudiCar:VWCar
    {
        [Unity.Dependency]
        public ApplePhone Phone { get; set; }
        public AndroidPhone AndroidPhone { get; set; }
        
        public BMWCar BMW { get; set; }
        public AudiCar()
        {

        }
        //构造函数注入
        //默认找参数最多的构造函数
        [InjectionConstructor]
        public AudiCar(BMWCar BaoMa)
        {
            BMW = BaoMa;
        }
        //方法注入
        [InjectionMethod]
        public void Init(AndroidPhone android)
        {
            AndroidPhone = android;
        }
    }
}
