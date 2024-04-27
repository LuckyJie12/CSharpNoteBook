using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TexingAttribute
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(UserState.Noemal.GetRemarks());//正常
            Student student = new Student();
            student.ID = 1;
            student.Name = "Test";
            student.QQNumber = 1234567;
            Console.WriteLine(student.ValueIsok());//True
            student.QQNumber = 123;
            Console.WriteLine(student.ValueIsok());//False
        }
    }
}
