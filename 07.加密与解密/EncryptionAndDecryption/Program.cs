using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EncryptionAndDecryption
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MD5Extend mD5Extend = new MD5Extend();
            //mD5Extend.Show();
            RSAExtend rsaExtend = new RSAExtend();
            rsaExtend.Show();
        }
    }
}
