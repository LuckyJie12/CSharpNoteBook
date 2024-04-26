using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DeledateClick
{
    internal class MyDeleDateOne
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        private List<Student>  InitDate()
        {
            List<Student> students = new List<Student>()
            {
                new Student(){ID=1, Name="Jack",Age=32,ClassID=2},
                new Student(){ID=2, Name="Lucy",Age=24,ClassID=2},
                new Student(){ID=3, Name="Kangkang",Age=38,ClassID=2},
                new Student(){ID=4, Name="Mary",Age=49,ClassID=1},
                new Student(){ID=5, Name="Flace",Age=23,ClassID=1},
                new Student(){ID=6, Name="Rafer",Age=33,ClassID=1},
                new Student(){ID=7, Name="Alinda",Age=22,ClassID=1},
                new Student(){ID=8, Name="Tom",Age=19,ClassID=2},
            };
            return students;
        }
        /// <summary>
        /// 根据不同条件找出相应的学生
        /// </summary>
        public void Show()
        {
            //找出名字字符长度大于5的学生
            {
                Console.WriteLine("------------------名字字符长度大于5的学生--------------------");
                List<Student> Result= new List<Student>();
                Result = GetStudents(this.InitDate(), 1);
                Console.WriteLine($"普通方法查找：一共有{Result.Count}个学生！");
                //委托查找
                GetIsok Isok = new GetIsok(GetThanName);
                Result= GetStudentsDeleDate(this.InitDate(), Isok);
                Console.WriteLine($"委托方法查找：一共有{Result.Count}个学生！");
            }
            //找出二班的学生
            {
                Console.WriteLine("------------------二班的学生--------------------");
                List<Student> Result = new List<Student>();
                Result = GetStudents(this.InitDate(), 2);
                Console.WriteLine($"普通方法查找：一共有{Result.Count}个学生！");
                //委托查找
                GetIsok Isok = new GetIsok((Student S) =>
                {
                    return S.ClassID == 2;
                });
                Result = GetStudentsDeleDate(this.InitDate(), Isok);
                Console.WriteLine($"委托方法查找：一共有{Result.Count}个学生！");
                //还可以这样写
                Func<Student,string, bool> func =(Student S,string Str) =>
                {
                    return S.ClassID == 2;
                };
                Console.WriteLine("输出结果："+func.Invoke(this.InitDate()[0],"12"));
            }
            //找出二班的名字字符长度大于5的学生
            {
                Console.WriteLine("------------------二班的名字字符长度大于5的学生--------------------");
                List<Student> Result = new List<Student>();
                Result = GetStudents(this.InitDate(), 3);
                Console.WriteLine($"普通方法查找：一共有{Result.Count}个学生！");
                //委托查找
                GetIsok Isok = new GetIsok((Student S) =>
                {
                    return S.ClassID == 2 && S.Name.Length > 5;
                });
                Result = GetStudentsDeleDate(this.InitDate(), Isok);
                Console.WriteLine($"委托方法查找：一共有{Result.Count}个学生！");
            }
        }
        /// <summary>
        /// 普通方法：根据type的不用进入不同的判断
        /// </summary>
        /// <param name="student"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<Student> GetStudents(List<Student> student,int Type)
        {
            List<Student> Result = new List<Student>();
            foreach (Student item in student)
            {

                if (Type == 1)
                {
                    if (item.Name.Length > 5)
                    {
                        Result.Add(item);
                    }
                }
                else if (Type == 2)
                {
                    if (item.ClassID == 2)
                    {
                        Result.Add(item);
                    }
                }
                else if (Type == 3)
                {
                    if (item.ClassID == 2&&item.Name.Length>5)
                    {
                        Result.Add(item);
                    }
                }
            }
            return Result;
        }
        //生命委托
        public delegate bool GetIsok(Student Stu);
        public bool GetThanName(Student student)
        {
                return student.Name.Length > 5;
        }
        public List<Student> GetStudentsDeleDate(List<Student> student, GetIsok isok)
        {
            List<Student> Result = new List<Student>();
            foreach (var item in student)
            {
                if (isok(item))
                {
                    Result.Add(item);
                }
            }
            return Result;
        }
    }
}
