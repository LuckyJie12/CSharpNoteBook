using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Homework2
{
    internal class Program
    {
        private static readonly object Console_lock = new object();
        static void Main(string[] args)
        {
            //Console.WriteLine(File.ReadAllText("..\\..\\PeopleConfig.json"));
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            bool IsOpen = false;
            #region 监控线程
            Task.Run(() =>
            {
                int Year=DateTime.Now.Year;
                int RanYear = 0;
                while (Year!=RanYear&&!tokenSource.IsCancellationRequested)
                {

                    RanYear=new Random().Next(2000,2030);
                    Thread.Sleep(1000);
                    //lock (Console_lock)
                    //{
                    //    Thread.Sleep(1000);
                    //    if (!tokenSource.IsCancellationRequested)
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Black;
                    //        Console.Write(RanYear);
                    //    }
                    //}
                }
                if (!tokenSource.IsCancellationRequested)
                {
                    lock (Console_lock)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"天降雷霆灭世，天龙八部的故事到此结束。。。");
                    }
                    tokenSource.Cancel();
                }
                else
                {
                    Console.WriteLine("监控取消！");
                }
            });
            #endregion
            var ListPeople = new JSONHelper().JsonToList<List<People>>("ConfigJSON\\PeopleConfig.json");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<Task> tasks = new List<Task>();
            foreach (var person in ListPeople)
            {
                tasks.Add(Task.Factory.StartNew((t) =>
                {
                    Thread.Sleep(1000);
                    for (int i = 0; i < person.Experience.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (!IsOpen)
                            {
                                if (tokenSource.IsCancellationRequested)
                                {
                                    break;
                                }
                                lock (Console_lock)
                                {
                                    if (!IsOpen)
                                    {
                                        if (tokenSource.IsCancellationRequested)
                                        {
                                            break;
                                        }
                                        Console.ForegroundColor = person.Color;
                                        Console.WriteLine($"{person.Name}遇到了{person.Experience[i]}");
                                        Console.ForegroundColor =ConsoleColor.Black;
                                        Console.WriteLine("天龙八部就此拉开帷幕！！");
                                        IsOpen = true;
                                    }
                                    else
                                    {
                                        if (tokenSource.IsCancellationRequested)
                                        {
                                            break;
                                        }
                                        Console.ForegroundColor = person.Color;
                                        Console.WriteLine($"{person.Name}遇到了{person.Experience[i]}");
                                    }
                                }
                            }
                            else
                            {
                                lock (Console_lock)
                                {
                                    if (tokenSource.IsCancellationRequested)
                                    {
                                        break;  
                                    }
                                    Console.ForegroundColor = person.Color;
                                    Console.WriteLine($"{person.Name}遇到了{person.Experience[i]}");
                                }
                            }
                        }
                        else
                        {
                            lock (Console_lock)
                            {
                                if (tokenSource.IsCancellationRequested)
                                {
                                    break;
                                }
                                Console.ForegroundColor = person.Color;
                                Console.WriteLine($"{person.Name}遇到了{person.Experience[i]}");
                            }
                        }
                    }
                },person.Name, tokenSource.Token));
            }
            tasks.Add( Task.WhenAny(tasks.ToArray()).ContinueWith(t =>
            {
                lock (Console_lock)
                {
                    if (!tokenSource.IsCancellationRequested)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"{t.Result.AsyncState}已经做好准备啦！！");
                    }
                }
            }));
            tasks.Add(Task.WhenAll(tasks.ToArray()).ContinueWith((t) =>
            {
                lock (Console_lock)
                {
                    if (!tokenSource.IsCancellationRequested)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("中原群雄大战辽兵，忠义两难一死谢天！！");
                    }
                }
            }));
            Task.WhenAll(tasks.ToArray()).ContinueWith(t =>
            {
                stopwatch.Stop();
                lock (Console_lock)
                {
                    if (!tokenSource.IsCancellationRequested)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"天龙八部的故事一共发费了{stopwatch.ElapsedMilliseconds}ms");
                        tokenSource.Cancel();
                    }
                }
            });
            Console.ReadKey();
        }
    }
}
