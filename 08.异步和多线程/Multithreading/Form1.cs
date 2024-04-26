using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multithreading
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        ///1. Thread提供了太多的API
        ///2.性能问题：无限使用线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"------------------Thread Start【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】----------------------");
            ThreadStart threadStart = () => DoNoting("Hello World");
            Thread thread = new Thread(threadStart);
            thread.Start();
            // 等待线程结束
            thread.Join(500);
            // 终止线程
            //thread.Abort();
            if (thread.ThreadState == System.Threading.ThreadState.Stopped)
            {
                Thread.Sleep(100);//当前线程，休息100ms
            }
            Thread t = new Thread(() => DoNoting("大家好！"));
            // 设置线程优先级为高
            //t.Priority = ThreadPriority.Highest;
            t.Start();
            Console.WriteLine($"------------------Thread End【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】----------------------");
        }
        public void DoNoting(string Value)
        {
            //for (int i = 0; i < 100000; i++)
            //{
            //    Console.WriteLine(i);
            //}
            Console.WriteLine($"DoNoting   {Value}     Start+【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】");
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < 1000000; i++)
            {
                stringBuilder.Append(DateTime.Now);
            }
            Thread.Sleep(100);//当前线程，休息100ms
            Console.WriteLine($"DoNoting   {Value}     End+【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】");
        }

        private void ThreadPool_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"------------------------------ThreadPool Start【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】-------------------------------");
            ThreadPool.QueueUserWorkItem(t => DoNoting("你好ThreadPool"));
            //{
            //    //获取最大线程，IO线程
            //    ThreadPool.GetMaxThreads(out int workerThreads, out int completionPortThreads);
            //    Console.WriteLine($"workerThreads:{workerThreads},completionPortThreads:{completionPortThreads}");
            //}
            //{
            //    //获取最小线程，IO线程
            //    ThreadPool.GetMinThreads(out int workerThreads, out int completionPortThreads);
            //    Console.WriteLine($"workerThreads:{workerThreads},completionPortThreads:{completionPortThreads}");
            //}
            //{
            //    //设置最大最小
            //    ThreadPool.SetMaxThreads(16, 16);
            //    ThreadPool.SetMinThreads(8, 8);
            //}
            //线程等待
            //Flase---Waitone等待-----Set之后为True
            //True----Waitone直接过去-----Reset之后吧为flase
            ManualResetEvent manual = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(t =>
            {
                DoNoting("等待执行，我执行之后你才能执行");
                manual.Set();
            });
            manual.WaitOne();
            Console.WriteLine("我是你执行之后执行的！");

            ThreadPool.QueueUserWorkItem(t => { DoNoting("ThreadPoolClick1！"); });
            ThreadPool.QueueUserWorkItem(t => { DoNoting("ThreadPoolClick2！"); });
            ThreadPool.QueueUserWorkItem(t => { DoNoting("ThreadPoolClick3！"); });
            ThreadPool.QueueUserWorkItem(t => { DoNoting("ThreadPoolClick4！"); });
            ThreadPool.QueueUserWorkItem(t => { DoNoting("ThreadPoolClick5！"); });
            Thread.Sleep(1000 * 10);
            Console.WriteLine("等待10S");
            ThreadPool.QueueUserWorkItem(t => { DoNoting("ThreadPoolClick1！"); });
            ThreadPool.QueueUserWorkItem(t => { DoNoting("ThreadPoolClick2！"); });
            ThreadPool.QueueUserWorkItem(t => { DoNoting("ThreadPoolClick3！"); });
            ThreadPool.QueueUserWorkItem(t => { DoNoting("ThreadPoolClick4！"); });
            ThreadPool.QueueUserWorkItem(t => { DoNoting("ThreadPoolClick5！"); });
            Console.WriteLine($"------------------------------ThreadPool End【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】-------------------------------");
        }

        private void TaskButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"------------------------------Task Start【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】-------------------------------");
            Task.Run(() =>
            {
                //{
                //    //几种执行方式，效果一样
                //    Task task = new Task(() => DoNoting("Task"));
                //    task.Start();
                //    Task.Run(() =>
                //    {
                //        DoNoting("Task");
                //    });
                //    TaskFactory taskFactory = Task.Factory;
                //    taskFactory.StartNew(() => DoNoting("TaskFactory"));
                //}
                List<Task> TaskList = new List<Task>();
                Console.WriteLine("创建一个项目。。。。。。。");
                Console.WriteLine("前置准备工作！");
                Console.WriteLine("开始编程：");
                TaskList.Add(Task.Run(() => DoNoting("刘道鈺--前端！！！")));
                TaskList.Add(Task.Run(() => DoNoting("万龙飞--后端端！！！")));
                TaskList.Add(Task.Run(() => DoNoting("赵健--测试！！！")));
                TaskList.Add(Task.Run(() => DoNoting("刘祥--数据库！！！")));
                //Task.Run(() =>
                //{
                //    TaskList.Add(Task.Run(() => DoNoting("刘道鈺--前端！！！")));
                //    TaskList.Add(Task.Run(() => DoNoting("万龙飞--后端端！！！")));
                //    TaskList.Add(Task.Run(() => DoNoting("赵健--测试！！！")));
                //    TaskList.Add(Task.Run(() => DoNoting("刘祥--数据库！！！")));
                //});
                {
                    //线程等待，会阻塞当前线程全部任务完成才会进入下一行---卡页面
                    //Task.WaitAll(TaskList.ToArray());
                    //Task.WaitAll(TaskList.ToArray(),50);
                    //线程等待，会阻塞当前线程,当某有一个线程完成后，就会执行下一行---卡页面
                    //Task.WaitAny(TaskList.ToArray());
                    //Task.WaitAny(TaskList.ToArray(),1000);
                    //Console.WriteLine("完成了一个任务。。。。");
                    //Console.WriteLine("通知客户上线验收。。。。");
                }
                {
                    //TaskFactory taskFactory = new TaskFactory();
                    //taskFactory.ContinueWhenAll(TaskList.ToArray(), t =>
                    //{
                    //    Console.WriteLine($"通知客户上线测试！！【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】");
                    //});
                    //线程等待，一个任务完成之后执行可执行什么-----不卡界面
                    Task.WhenAny(TaskList.ToArray()).ContinueWith((t) =>
                    {
                        Console.WriteLine($"得意的笑！！【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】");
                    });
                    //线程等待，全部任务完成之后执行可执行什么-----不卡界面
                    Task.WhenAll(TaskList.ToArray()).ContinueWith((t) =>
                    {
                        Console.WriteLine($"通知客户上线测试！！【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】");
                    });
                }
                Task.WaitAll(TaskList.ToArray());
                {
                    Console.WriteLine("-----------------TaskFactoryList--------------------");
                    //使用TaskFactory，可以返回完成任务的返回信息，标识
                    List<Task> TaskFactoryList = new List<Task>();
                    TaskFactory taskFactory = new TaskFactory();
                    TaskFactoryList.Add(taskFactory.StartNew((t) => DoNoting("刘道鈺--前端！！！"), "刘道鈺"));
                    TaskFactoryList.Add(taskFactory.StartNew((t) => DoNoting("万龙飞--后端端！！！"), "万龙飞"));
                    TaskFactoryList.Add(taskFactory.StartNew((t) => DoNoting("赵健--测试！！！"), "赵健"));
                    TaskFactoryList.Add(taskFactory.StartNew((t) => DoNoting("刘祥--数据库！！！"), "刘祥"));
                    //线程等待，一个任务完成之后执行可执行什么-----不卡界面
                    taskFactory.ContinueWhenAny(TaskFactoryList.ToArray(), t =>
                    {
                        Console.WriteLine(t.AsyncState);
                        Console.WriteLine($"得意的笑！！【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】");
                    });
                    //线程等待，全部任务完成之后执行可执行什么-----不卡界面
                    taskFactory.ContinueWhenAll(TaskFactoryList.ToArray(), tlist =>
                    {
                        Console.WriteLine(tlist[0].AsyncState);
                        Console.WriteLine($"通知客户上线测试！！【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】");
                    });
                }
                //{
                //    //完成10000个任务，但是只要10个线程
                //    Action<int> action = (i) =>
                //    {
                //        Console.WriteLine($"【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】");
                //        Thread.Sleep(new Random(i).Next(100,300));
                //    };
                //    List<Task> taskList=new List<Task>();
                //    for (int i = 0; i < 10000; i++)
                //    {
                //        int k = i;
                //        taskList.Add(Task.Run(() => action.Invoke(i)));
                //        if (taskList.Count>10)
                //        {
                //            Task.WaitAny(taskList.ToArray());
                //            taskList=taskList.Where(t => t.Status!=TaskStatus.RanToCompletion).ToList();
                //        }
                //    }
                //    Task.WaitAll();
                //    Console.WriteLine("10000个任务全部已完成！！！");
                //}
                {
                    //延迟和等待
                    //Task.Delay(2000);//延迟  不卡
                    //Thread.Sleep(2000);//等待 卡页面
                    {
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        stopwatch.Start();
                        Thread.Sleep(2000);
                        stopwatch.Stop();
                        Console.WriteLine(stopwatch.ElapsedMilliseconds);
                    }
                    {
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        stopwatch.Start();
                        Task.Delay(2000).ContinueWith(t =>
                        {
                            stopwatch.Stop();
                            Console.WriteLine(stopwatch.ElapsedMilliseconds);
                        });
                    }
                    {
                        //类似于
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        stopwatch.Start();
                        Task.Run(() =>
                        {
                            Thread.Sleep(2000);
                            stopwatch.Stop();
                            Console.WriteLine(stopwatch.ElapsedMilliseconds);
                        });
                    }
                }
            });
            Console.WriteLine($"------------------------------Task End【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】-------------------------------");
        }
        private void ParallelButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"------------------------------Parallel Start【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】-------------------------------");
            //{
            //    //卡界面 节省一个线程，主线程参与计算
            //    Parallel.Invoke(() =>{DoNoting("刘道鈺--前端！！！");}, () => { DoNoting("万龙飞--后端端！！！"); }, () => { DoNoting("赵健--测试！！！"); }, () => { DoNoting("刘祥--数据库！！！"); }) ;
            //}
            {
                //ParallelOptions parallelOptions = new ParallelOptions();
                //parallelOptions.MaxDegreeOfParallelism = 3;//控制线程数量
                //Parallel.For(0, 10, parallelOptions, (i) =>
                //{
                //    DoNoting("Parallel" + i.ToString());
                //});
                //Parallel.For(0, 5, (i) => {
                //    DoNoting("Parallel"+  i.ToString());
                //});
                //Parallel.ForEach(new string[] { "1", "2", "3", "4", "5" }, i =>
                //{
                //    DoNoting("Parallel" + i);
                //});
            }
            {
                //结束当前任务
                // 创建行 Parallel.For 循环
                Parallel.For(0, 10, (i, state) =>
                {
                    // 如果i等于5，则标记状态为 Break，结束循环
                    if (i == 5)
                    {
                        state.Break();
                        Console.WriteLine("任务已取消。");
                        return;
                    }
                    if (state.IsStopped)
                    {
                        Console.WriteLine("任务已中止。");
                        return;
                    }
                    if (state.IsExceptional)
                    {
                        Console.WriteLine("任务异常终止。");
                        return;
                    }
                    // 执行某些操作
                    Console.WriteLine(i);
                    Thread.Sleep(1000);
                });
            }
            Console.WriteLine($"------------------------------Parallel End【{System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00")}】-------------------------------");
        }
        //private防止外面也去lock static 全场唯一 readonly不要改动object表示引用
        private static readonly object ThreadCoreOBJ = new object();
        private int TotalCountOut = 0;
        private void ThreadCore_Click(object sender, EventArgs e)
        {
            List<Task> taskList = new List<Task>();
            try
            {
                #region 异常处理
                //for (int i = 0; i < 40; i++)
                //{
                //    string Str="线程"+i.ToString();
                //    Action<object> act = t =>
                //    {
                //        try
                //        {
                //            if (t.ToString().Equals("线程11"))
                //            {
                //                throw new Exception(t.ToString() + "执行失败");
                //            }
                //            if (t.ToString().Equals("线程12"))
                //            {
                //                throw new Exception(t.ToString() + "执行失败");
                //            }
                //            Console.WriteLine(t.ToString()+"执行成功！");
                //        }
                //        catch (Exception ex)
                //        {
                //            Console.WriteLine(ex.Message);
                //        }
                //    };
                //    taskList.Add(Task.Run(() => act.Invoke(Str)));
                //}
                #endregion
                #region 线程取消
                //多个线程并发，某个失败后，希望通知别的线程，都停下来
                //task是外部无法中止，Thread.Abort不靠谱，因为线程是OS的资源，无法掌控啥时候取消//线程自己停止自己一公共的访问变量--修改它---线程不断的检测它
                //CancellationTokentrHH任务是否取消Cancel取消
                //CancellationTokenSource tokenSource = new CancellationTokenSource();//类似于bool值
                //for (int i = 1; i < 41; i++)
                //{
                //    string Name= "线程" + i.ToString();
                //    Action<object> action = (t) =>
                //    {
                //        try
                //        {
                //            Thread.Sleep(2000);
                //            if (t.ToString().Equals("线程11"))
                //            {
                //                throw new Exception(t.ToString() + "执行失败");
                //            }
                //            if (t.ToString().Equals("线程12"))
                //            {
                //                throw new Exception(t.ToString() + "执行失败");
                //            }
                //            if (tokenSource.IsCancellationRequested)//检测是否取消
                //            {
                //                Console.WriteLine(t.ToString() + "放弃执行！");
                //                return;
                //            }
                //            else
                //            {
                //                Console.WriteLine(t.ToString() + "执行成功！");
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //            tokenSource.Cancel();
                //            Console.WriteLine(ex.Message);
                //        } 
                //    };
                //    taskList.Add(Task.Factory.StartNew(action, Name, tokenSource.Token));
                //}
                //Task.WaitAll(taskList.ToArray());
                //Console.WriteLine("执行完毕");
                #endregion
                #region 多线程临时变量
                //全都会输出i=5
                //for (int i = 0; i < 5; i++)
                //{
                //    Task.Run(() =>
                //    {
                //        Console.WriteLine($"i={i}");
                //    });
                //}
                ////正常输出，因为k为临时变量
                //for (int i = 0; i < 5; i++)
                //{
                //    int k = i;
                //    Task.Run(() =>
                //    {
                //        Console.WriteLine($"k={k},i={i}");
                //    });
                //}
                #endregion
                #region 线程安全Lock
                int TotalCountIn = 0;//写到内部其实所有问题的，要写成外面
                List<int> ListCount = new List<int>();//也是一样有问题，要写到外面
                for (int i = 0; i < 10000; i++)
                {
                    int Newi = i;
                    taskList.Add(Task.Run(() =>
                    {
                        //lock后的方法块，任意时刻只有一个线程可以进入
                        //只能锁引用类型,占用这个引用链接不要用string因为享元
                        //lock (ThreadCoreOBJ)
                        //{
                        TotalCountIn = TotalCountIn + 1;
                        TotalCountOut += 1;
                        ListCount.Add(Newi);
                        //}
                        //类似于
                        //Monitor.Enter(ThreadCoreOBJ);
                        //Monitor.Exit(ThreadCoreOBJ);
                    }));
                }
                Task.WaitAll(taskList.ToArray());
                Console.WriteLine($"TotalCountIn:{TotalCountIn}");
                Console.WriteLine($"TotalCountOut:{TotalCountOut}");
                Console.WriteLine($"ListCount:{ListCount.Count}");
                #endregion
            }
            catch (AggregateException aex)
            {
                //Task.WaitAll(taskList.ToArray());
                //没有使用waitall的时候，如果有异常，不会到这一步，因为超出了范围
                foreach (var item in aex.InnerExceptions)
                {
                    Console.WriteLine(item.Message);
                }
            }
        }

        private void btnAwait_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"------------------------------btnAwait Start【{Thread.CurrentThread.ManagedThreadId.ToString("00")}】-------------------------------");
            NoAwait();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"我是{i}线程ID是：{Thread.CurrentThread.ManagedThreadId.ToString("00")}");
            }
            Console.WriteLine($"------------------------------btnAwait End【{Thread.CurrentThread.ManagedThreadId.ToString("00")}】-------------------------------");
        }
        private async void NoAwait()
        {
            Console.WriteLine($"------------------------------NoAwait Out Start【{Thread.CurrentThread.ManagedThreadId.ToString("00")}】-------------------------------");
            TaskFactory taskFactory = new TaskFactory();
            Task task = taskFactory.StartNew(() =>
            {
                DoNighing("NoAwait");
            });
            await task;
            Console.WriteLine($"------------------------------NoAwait Out End【{Thread.CurrentThread.ManagedThreadId.ToString("00")}】-------------------------------");
        }
        private static void DoNighing(string Vaule)
        {
            Task.Run(() =>
            {
                Console.WriteLine($"------------------------------{Vaule} In Start【{Thread.CurrentThread.ManagedThreadId.ToString("00")}】-------------------------------");
                Thread.Sleep(3000);
                Console.WriteLine($"------------------------------{Vaule} In End【{Thread.CurrentThread.ManagedThreadId.ToString("00")}】-------------------------------");
            });
        }
    }
}
