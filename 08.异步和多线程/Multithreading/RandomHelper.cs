using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    internal class RandomHelper
    {
        public int GetNumLong(int Min, int Max)
        {
            Thread.Sleep(new Random().Next(1000));
            return GetNums(Min, Max);
        }
        public int GetNums(int Min,int Max)
        {
            Thread.Sleep(20);
            Guid guid = new Guid();
            string SGuid=guid.ToString();
            int seed = DateTime.Now.Millisecond;
            for (int i = 0; i < SGuid.Length; i++)
            {
                switch (SGuid[i])
                {
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                        seed = seed + 1;
                        break;
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                        seed = seed +2;
                        break;
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                        seed = seed + 3;
                        break;
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z':
                        seed = seed + 4;
                        break;
                    default:
                        seed = seed + 1; break;
                }
            }
            Random random = new Random(seed);
            return random.Next(Min, Max);
            //return seed;
        }
    }
}
