using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public class DBHelper
    {
        public string Connstr = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public List<T> GetDate<T>(int ID)where T : Common
        {
            Type type = typeof(T);
            string GetField = string.Join(",", type.GetProperties().Select(C=>$"[{C.GetPropName()}]"));
            string Sql = $"select {GetField} from [{type.Name}] where Id={ID} ";
            List<T> list =null;
            using (SqlConnection Conn=new SqlConnection(Connstr))
            {
                list = new List<T>();
                SqlCommand cmd = new SqlCommand(Sql, Conn);
                Conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    T t = (T)Activator.CreateInstance(type);
                    foreach (var prop in type.GetProperties())
                    {
                        prop.SetValue(t, reader[prop.GetPropName()] is DBNull ? null : reader[prop.GetPropName()]);
                    }
                    list.Add(t);
                }
            }
            return list;
        }
        public void UpdateDate<T>(T t) where T : Common
        {
            Type type=typeof(T);
            //去掉修改ID
            var PropArray=type.GetProperties().Where(C=>!C.Name.Equals("Id"));
            //简易版拼接，不防SQL注入
            //string Poon = string.Join(",", PropArray.Select(C => $"[{C.GetPropName()}]='{C.GetValue(t)}'"));
            //防SQL注入要这样写
            string Field = string.Join(",", PropArray.Select(C => $"[{C.GetPropName()}]=@{C.GetPropName()}"));
            var Paramar = PropArray.Select(c => new SqlParameter($"@{c.GetPropName()}", c.GetValue(t) ?? DBNull.Value)).ToArray();
            //类似于上面var Paramar
            //SqlParameter[] Paramar = new SqlParameter[]
            //{
            //    new SqlParameter("@UserName","狗蛋"),
            //    new SqlParameter("@Gender",true),
            //    new SqlParameter("@Remark","好兄弟")
            //};
            string Sql = $"Update [{type.Name}] set {Field} where [Id]={t.Id}";
            using (SqlConnection Conn = new SqlConnection(Connstr))
            {
                SqlCommand Command = new SqlCommand(Sql, Conn);
                //AddRange放多个 Add单个
                Command.Parameters.AddRange(Paramar);
                Conn.Open();
                if (Command.ExecuteNonQuery() >= 1)
                {
                    Console.WriteLine("修改成功！");
                }
                else { Console.WriteLine("修改失败！"); }
            }
        }
    }
}
