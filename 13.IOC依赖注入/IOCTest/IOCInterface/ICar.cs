using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCTest.IOCInterface
{
    public interface  ICar
    {
        /// <summary>
        /// 跑
        /// </summary>
        void Run();
        /// <summary>
        /// 停
        /// </summary>
        void Stop();
        /// <summary>
        /// 加速
        /// </summary>
        void SpeedUp();
        /// <summary>
        /// 减速
        /// </summary>
        void SpeedDown();
    }
}
