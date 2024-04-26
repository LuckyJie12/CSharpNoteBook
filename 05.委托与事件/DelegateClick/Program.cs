using DeledateClick.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DeledateClick.Event.Cats;

namespace DeledateClick
{
    public class Program
    {
        static void Main(string[] args)
        {
            MyDeleDateOne myDeleDateOne = new MyDeleDateOne();
            myDeleDateOne.Show();
            Console.WriteLine("-----------------多播委托-------------------");
            {
                Cats cats = new Cats();
                cats.CatDele += new CatDeledate(new Father().CatFather);
                cats.CatDele += new CatDeledate(new Mother().CatMother);
                //委托可以
                {
                    //cats.CatDele.Invoke();
                    //cats.CatDele = null;
                }
                cats.CatDele += new CatDeledate(new Miao().CatMiao);
                cats.CatDele += new CatDeledate(new Mouse().CatMouse);
                cats.ShowDeleDate();
            }
            {
                Console.WriteLine("-----------------事件-------------------");
                Cats cats = new Cats();
                cats.CatEvent += new CatDeledate(new Father().CatFather);
                cats.CatEvent += new CatDeledate(new Mother().CatMother);
                //事件在外部不能Invoke和赋值只有声明者可以用，子类也不行
                {
                    //cats.CatEvent.Invoke();
                    //cats.CatEvent=null;
                }
                cats.CatEvent += new CatDeledate(new Miao().CatMiao);
                cats.CatEvent += new CatDeledate(new Mouse().CatMouse);
                cats.ShowDeleDate();//此时ShowDeleDate还是为null
            }
        }
    }
}
