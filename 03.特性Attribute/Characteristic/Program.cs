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
            //Console.WriteLine(Custom.GetRemarks(UserState.Noemal));
            //Console.WriteLine(Custom.GetRemarks(UserState.Freeze));
            //Console.WriteLine(Custom.GetRemarks(UserState.Delete));
            Console.WriteLine(UserState.Noemal.GetRemarks());
            Student student = new Student();
            student.ID = 1;
            student.Name = "Test";
            student.QQNumber = 1234567;
            Console.WriteLine(student.ValueIsok());
            student.QQNumber = 123;
            Console.WriteLine(student.ValueIsok());

            //---------------------个人小练习------------------------
            List<int> Nums = new List<int>();
            for (int i = 0; i < 11000; i++)
            {
                Nums.Add(i);
            }
            Console.WriteLine(Nums.TOrF());

        }
        public static bool TOrF(this List<int> Num)
        {
            foreach (var item in Num)
            {
                Console.WriteLine(item);
                if (item == 5)
                {

                    return true;
                }
            }
            return false;
        }
    }
}
