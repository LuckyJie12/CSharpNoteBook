using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public class SqlHelperDelegate
    {
        public string Connstr = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        /// <summary>
        /// 查找单个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        public T GetUser<T>(int ID) where T : Common
        {
            Type type = typeof(T);
            string GetField = string.Join(",", type.GetProperties().Select(C => $"[{C.GetPropName()}]"));
            string Sql = $"select {GetField} from [{type.Name}] where Id={ID} ";
            Func<SqlCommand, T> Fun = (S) =>
            {
                SqlDataReader Read = S.ExecuteReader();
                List<T> ResT = ReaderToList<T>(Read);
                return ResT.FirstOrDefault();
            };
            return GetDataSource<T>(Sql, Fun);
        }
        /// <summary>
        /// 查找全部
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAllUser<T>() where T : Common
        {
            Type type = typeof(T);
            string GetField = string.Join(",", type.GetProperties().Select(C => $"[{C.GetPropName()}]"));
            string Sql = $"select {GetField} from [{type.Name}]";
            Func<SqlCommand, List<T>> Fun = (S) =>
            {
                SqlDataReader Read = S.ExecuteReader();
                List<T> ResT = ReaderToList<T>(Read);
                return ResT;
            };
            return GetDataSource<List<T>>(Sql, Fun);
        }
        public void UpdateDate<T>(T t) where T : Common
        {
            Type type = typeof(T);
            //去掉修改ID
            var PropArray = type.GetProperties().Where(C => !C.Name.Equals("Id"));
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
            Func<SqlCommand, int> func = (S) =>
            {
                S.Parameters.AddRange(Paramar);
                return S.ExecuteNonQuery();
            };
            int i=GetDataSource<int>(Sql, func);
            if (i >= 1)
            {
                Console.WriteLine("修改成功！");
            }
            else
            {
                Console.WriteLine("修改失败！");
            }
        }
        public List<T> ReaderToList<T>(SqlDataReader sqlData){
            Type type = typeof(T);
            List<T> Result =new List<T>();
            while (sqlData.Read())
            {
                T One=(T)Activator.CreateInstance(type);
                foreach (var prop in type.GetProperties())
                {
                    prop.SetValue(One, sqlData[prop.GetPropName()] is DBNull ? null : sqlData[prop.GetPropName()]);
                }
                Result.Add(One);
            }
            return Result;
        }
        public T GetDataSource<T>(string SQL,Func<SqlCommand,T> func)
        {
            using (SqlConnection Conn = new SqlConnection(Connstr))
            {
                Conn.Open();
                SqlTransaction Tran = Conn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand(SQL, Conn);
                    cmd.Transaction = Tran;
                    T Res = func.Invoke(cmd);
                    //Tran.Commit();
                    return Res;
                }
                catch (Exception EX)
                {
                    Tran.Rollback();
                    throw new Exception("数据库执行出现了错误"+EX.Message);
                }
            }
        }
    }
}
