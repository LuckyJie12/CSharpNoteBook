using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexingAttribute
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [StuQQHeFa(123456,9999999999)]
        public long QQNumber { get; set; }
        
    }
    public static class Stu
    {
        public static bool ValueIsok(this object Obj)
        {
            Type type = Obj.GetType();
            foreach (var t in type.GetProperties())
            {
                if (t.IsDefined(typeof(StuQQHeFaAttribute), true))
                {
                    object[] objects = t.GetCustomAttributes(typeof(StuQQHeFaAttribute), true);
                    foreach (StuQQHeFaAttribute obj in objects)
                    {
                        if (!obj.GetIsok(t.GetValue(Obj).ToString()))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
    public class StuQQHeFaAttribute : Attribute
    {
        public StuQQHeFaAttribute(long MinNumber,long MaxNumber)
        {
            this._MinNumber = MinNumber;
            this._MaxNumber = MaxNumber;
        }
        private long _MinNumber;
        private long _MaxNumber;
        public bool GetIsok(string QQNumber)
        {
            if (long.TryParse(QQNumber,out long Value))
            {
                if (Value > _MinNumber && Value < _MaxNumber)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
