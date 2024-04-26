using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //读取配置数据
            //string Connstr = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            //string KeySetting = ConfigurationManager.AppSettings["Lucky"];
            {
                Console.WriteLine("-------------------------普通DBHelper-------------------------");
                DBHelper dBHelper = new DBHelper();
                List<User> users = dBHelper.GetDate<User>(2);
                foreach (User user in users)
                {
                    Console.WriteLine($"姓名：{user.UserName},性别：{user.Sex},描述：{user.Remark}");
                }
                User Wang = users[0];
                Wang.Sex = false;
                Wang.UserName = "zhangwei";
                dBHelper.UpdateDate<User>(Wang);
            }
            {
                Console.WriteLine("-------------------------优化DBHelper之委托-------------------------");
                SqlHelperDelegate sqlHelper = new SqlHelperDelegate();
                User users = sqlHelper.GetUser<User>(2);
                Console.WriteLine($"姓名：{users.UserName},性别：{users.Sex},描述：{users.Remark}");
                Console.WriteLine("---------------------------------查找全部-----------------------------------");
                List<User> ListUser = sqlHelper.GetAllUser<User>();
                foreach (User user in ListUser)
                {
                    Console.WriteLine($"姓名：{user.UserName},性别：{user.Sex},描述：{user.Remark}");
                }
                Console.WriteLine("---------------修改----------------------------------------------");
                users.Sex = true;
                sqlHelper.UpdateDate<User>(users);
                Console.ReadKey();
            }
        }
    }
}
