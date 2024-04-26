using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static Generic.Earch;

namespace Generic
{
    public class Shower
    {
        /// <summary>
        /// 泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="PType"></param>
        public void GetListShow<T>(T PType)
        {
            Console.WriteLine(PType.GetType().Name);
        }
        
        public void GetListHide<T>(T PType)
            where T : People,IDog
        {
            PType.Hi();
            PType.ShowMe();
        }
        public interface XieChange<out T>
        {
            void OutSess();
        }
    }
}
