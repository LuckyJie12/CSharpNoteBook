using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCTest.IOCInterface
{
    public abstract class AbstractPhone
    {
        /// <summary>
        /// 打电话
        /// </summary>
        public abstract void Call();
        /// <summary>
        /// 发短信
        /// </summary>
        public abstract void Text();
        /// <summary>
        /// 玩游戏
        /// </summary>
        public abstract void PlayGame();
    }
}
