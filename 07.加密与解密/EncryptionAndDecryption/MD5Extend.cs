using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptionAndDecryption
{
    public class MD5Extend
    {
        public void Show()
        {
            string MD5 = GetMD5("213");
            Console.WriteLine(MD5);
            Thread t = new Thread(s =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string FilePath = openFileDialog.FileName;
                    Console.WriteLine(MD5Extend.GetFileMD5(FilePath));
                }
            }
            );
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }
        /// <summary>
        /// MD5字符串加密
        /// X的大小写位转换出来字母大小写，数字2则为转换长度位16X2为32为长度
        /// </summary>
        /// <param name="input">输入需要转换的文字</param>
        /// <returns></returns>
        public static string GetMD5(string input)
        {
            using (MD5 md5 = MD5.Create()) // 创建MD5实例
            {
                //将字符串转换为字节数组
                byte[] inputBytes = Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(input));
                // 计算哈希值
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder(); // 创建StringBuilder实例
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    // 将字节转换为16进制字符串
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                // 返回加密后的结果
                return sb.ToString();
            }
        }
        /// <summary>
        /// 获取文件的MD5加密
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static string GetFileMD5(string FilePath)
        {
            // 创建一个MD5加密算法的实例
            using (var md5 = MD5.Create())
            {
                // 打开要计算MD5值的文件
                using (var stream = File.OpenRead(FilePath))
                {
                    // 计算文件的MD5值
                    var hash = md5.ComputeHash(stream);

                    // 将MD5值转换为字符串形式
                    var md5String = BitConverter.ToString(hash).Replace("-", "").ToLower();
                    return md5String;
                }
            }
        }
    }
}
