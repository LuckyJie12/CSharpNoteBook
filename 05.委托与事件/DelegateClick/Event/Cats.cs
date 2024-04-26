using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeledateClick.Event
{
    internal class Cats
    {
        public void Catss()
        {
            new Father().CatFather();
            new Mother().CatMother();
            new Miao().CatMiao();
            new Mouse().CatMouse();
        }
        public delegate void CatDeledate();
        public CatDeledate CatDele = null;
        public void ShowDeleDate()
        {
            Console.WriteLine($"{this.GetType().Name} ShowDeleDate");
            if (this.CatDele!=null)
            {
                CatDele.Invoke();
            }
        }
        //事件
        public event CatDeledate CatEvent;
        public void ShowEvent()
        {
            Console.WriteLine($"{this.GetType().Name} ShowEvent");
            if (this.CatEvent != null)
            {
                CatEvent.Invoke();
            }
        }
    }
}
