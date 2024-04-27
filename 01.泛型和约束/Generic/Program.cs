using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Generic.Earch;

namespace Generic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //People people = new People();
            //people.Hi();
            //Chinese chinese = new Chinese();
            //chinese.Hi();
            //HeNan henan = new HeNan();
            //henan.Hi();
            //Japnese japnese = new Japnese();
            //japnese.ShowMe();
            //Shower shower = new Shower();
            //shower.GetListShow(people);
            //shower.GetListShow(chinese);
            //shower.GetListShow(japnese);
            //shower.GetListHide(chinese);

            //原始创建
            //Bird bird1 = new Bird();
            //Bird bird2 = new Sparrow();
            //Sparrow sparrow = new Sparrow();
            //List<Sparrow> sparrows = new List<Sparrow>();
            //List<Bird> bird = new List<Bird>();
            //List<Bird> bird_ = new List<Sparrow>().Select(C => (Bird)C).ToList();
            ////协变
            //IEnumerable<Bird> sparrows_ = new List<Bird>();
            //IEnumerable<Bird> sparrows_1 = new List<Sparrow>();
            Generic.Show();
        }
        class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
