using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structure
{
    public class ShowArray
    {
        public void Show()
        {
            #region Array
            //{
            //    //ArrayList
            //    int[] Nums = new int[10];
            //    ArrayList arrayList = new ArrayList();
            //    arrayList.Add(0);
            //    arrayList.Add(1);
            //    arrayList.Add(2);
            //    arrayList.Add("无类型限制");
            //    arrayList.Remove(0);//移除之后内容下标会往前顶
            //    arrayList.RemoveAt(1);
            //    var Num=arrayList[0];
            //}
            //{
            //    //List:也是Array 内存上连续摆放；不定长，泛型，保证类型安全，避免装箱拆箱
            //    List<int> list = new List<int>();
            //    list.Add(0);
            //    list.Add(1);
            //    list.Add(2);
            //    int Num = list[0];
            //}
            #endregion
            #region 链表
            //{
            //    LinkedList<string> LinkS = new LinkedList<string>();
            //    LinkS.AddFirst("Hello!");
            //    LinkS.AddLast("End!");
            //    bool IsCunZai = LinkS.Contains("Hello!");
            //    LinkedListNode<string> linked = LinkS.Find("Hello!");
            //    LinkS.AddBefore(linked, "Before");
            //    LinkS.AddAfter(linked, "After");
            //    LinkS.RemoveFirst();
            //    LinkS.Clear();
            //}
            //{
            //    //Queue先进先出
            //    Queue<string> Queues = new Queue<string>();
            //    Queues.Enqueue("One!");
            //    Queues.Enqueue("Two!");
            //    Queues.Enqueue("Three!");
            //    Queues.Enqueue("Four!");
            //    foreach (string s in Queues)
            //    {
            //        Console.WriteLine(s);
            //    }
            //    Console.WriteLine($"取出并移除取出的对象：{Queues.Dequeue()}");
            //    Console.WriteLine($"取出但不移除取出的对象：{Queues.Peek()}");
            //    List<string> Lists=Queues.ToList();
            //    //Stack先进后出
            //    Stack<string> Stacks=new Stack<string>();
            //    Stacks.Push("One!");
            //    Stacks.Push("Two!");
            //    Stacks.Push("Three!");
            //    Stacks.Push("Four!");
            //    Console.WriteLine($"移除并返回{Stacks.Pop()}");
            //    Console.WriteLine($"移除不返回{Stacks.Peek()}");
            //}
            #endregion
            #region Set
            {
                //自动去重
                //HashSet<int> List1 = new HashSet<int>();
                //List1.Add(123);
                //List1.Add(234);
                //List1.Add(567);
                //List1.Add(890);
                //List1.Add(123);
                //HashSet<int> List2 = new HashSet<int>();
                //List2.Add(123);
                //List2.Add(564);
                //List2.Add(984);
                //List2.Add(958);
                //List2.Add(265);
                //补：剔除两边相等的值
                //List1.SymmetricExceptWith(List2);
                //并：返回两个集合中所有的元素
                //List1.UnionWith(List2);
                //交：返回两个集合中共有的
                //List1.IntersectWith(List2);
                //差：移除指定的元素集合
                //List1.ExceptWith(List2);
            }
            {
                //自动去重，排序
                //SortedSet<int> SortS= new SortedSet<int>();
                //SortS.Add(1);
                //SortS.Add(4);
                //SortS.Add(2);
                //SortS.Add(1);
                //SortS.Add(3);
                
            }
            #endregion
            #region Key--Value

            {
                //Hashtable：字典，无序
                Hashtable Table = new Hashtable();
                Table.Add("Hello", "World");
                Table[123] = 456;
                //重复Key则修改
                Table[123] = 789;
                //多个线程读
                Hashtable.Synchronized(Table);
                //Dictionary：有序：字典，有序
                Dictionary<string,string> valuePairs= new Dictionary<string,string>();
                valuePairs.Add("Hello", "World");
                valuePairs.Add("123", "456");
                //添加相同Key会出错valuePairs.Add("123", "789");
                valuePairs["123"] = "789";
                valuePairs.Add("456", "789");

                //SortedList:字典，自动排序
                SortedList sortedList = new SortedList();
                sortedList.Add("Hello", "World");
                sortedList.Add("123", "456");
                sortedList.Add("nihao", "World");
            }
            #endregion
        }
    }
}
