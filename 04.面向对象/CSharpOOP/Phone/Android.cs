using CSharpOOP.Inherit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace CSharpOOP.Phone
{
    public  class Android : InterFace
    {
        static string In = null;
        public void SomeMethod()
        {
            string remark = GetPrivateFieldValue(); // 获取私有属性 Remark 的值
            In = remark;
            Console.WriteLine(remark);
        }
        public string Sop { get; set; } = In;
        public override void System()
        {
            Console.WriteLine($"这是在{this.GetType().Name} System is Android！");
        }
    }
}
