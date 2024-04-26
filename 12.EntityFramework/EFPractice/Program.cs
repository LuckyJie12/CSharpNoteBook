using EFFirst.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using (BookModel db = new BookModel())
            {
                //输出SQL执行日志
                db.Database.Log += c => Console.WriteLine($"SQL:{c}");
                {
                    //// 增加数据
                    //books Book = new books()
                    //{
                    //    title = "西游记",
                    //    author = "吴承恩",
                    //    publisher = "人民邮电出版社",
                    //    publish_year = 2010,
                    //    total_count = 10,
                    //    available_count = 10,
                    //    description = "四大名著"
                    //};
                    //db.books.Add(Book);
                    //db.SaveChanges();
                }
                {
                    //// 查询数据
                    //var query = from b in db.books
                    //            where b.book_id == 1
                    //            select b;
                    //foreach (var item in query)
                    //{
                    //    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", item.book_id, item.title, item.author, item.publisher, item.publish_year, item.total_count, item.available_count, item.description);
                    //}
                    //books GetBook = db.books.FirstOrDefault();
                    //Console.WriteLine(GetBook.title);
                }
                {
                    //// 更新数据
                    //var bookToUpdate = db.books.Where(b => b.book_id == 8).FirstOrDefault();
                    //if (bookToUpdate != null)
                    //{
                    //    bookToUpdate.title = "水浒传";
                    //    bookToUpdate.author = "施耐庵";
                    //    bookToUpdate.publisher = "电子工业出版社";
                    //    db.SaveChanges();
                    //}
                }
                {
                    //// 删除数据
                    //var bookToDelete = db.books.Where(b => b.book_id == 6).FirstOrDefault();
                    //if (bookToDelete != null)
                    //{
                    //    db.books.Remove(bookToDelete);
                    //    db.SaveChanges();
                    //}
                }
                {
                    ////使用SQL查询Books全表
                    //var query = db.Database.SqlQuery<books>("select * from books");
                    //foreach (var book in query)
                    //{
                    //    Console.WriteLine(book);
                    //}
                    ////更新
                    //var rowsAffected = db.Database.ExecuteSqlCommand("UPDATE books SET publisher = @p0 WHERE book_id = @p1", "人民出版社", 8);
                    //Console.WriteLine("Rows affected: " + rowsAffected);
                }
                {
                    //多表查询
                    //查询图书标签
                    var query = from b in db.books
                                where b.book_id == 1
                                select new
                                {
                                    Title = b.title,
                                    Name = b.categories.Select(c => c.name),
                                    Description = b.description
                                };
                    foreach (var item in query)
                    {
                        Console.WriteLine($"书名:{item.Title},标签：{string.Join("/", item.Name)},描述：{item.Description}");
                    }
                    var BorrowInfo = from B in db.books
                                     join BC in db.borrows on B.book_id equals BC.book_id
                                     join C in db.readers on BC.reader_id equals C.reader_id
                                     where (C.name == "张三")
                                     select new
                                     {
                                         Name = C.name,
                                         BorrowDate = BC.borrow_date,
                                         Book = B.title
                                     };
                    foreach (var item in BorrowInfo)
                    {
                        Console.WriteLine($"{item.Name}在{item.BorrowDate}，借阅了{item.Book}");
                    }
                    //左连接查询
                    var Res = (from K in db.books
                               join B in db.borrows on K.book_id equals B.book_id into BorrowJoin
                               from B in BorrowJoin.DefaultIfEmpty()
                               join R in db.readers on B.reader_id equals R.reader_id
                               where (R.name == "张三")
                               select new
                               {
                                   Name = R.name,
                                   Book = K.title,
                                   BorrowDate = B.borrow_date,
                                   ReturnDate = B.return_date
                               }).ToList();
                    string LeftSQL = Res.ToString();
                    foreach (var item in Res)
                    {
                        if (item.ReturnDate == null)
                        {
                            Console.WriteLine($"{item.Name}在{item.BorrowDate}，借阅了{item.Book}，状态：未归还。。。。");
                        }
                        else
                        {
                            Console.WriteLine($"{item.Name}在{item.BorrowDate}，借阅了{item.Book},归还时间：{item.ReturnDate}");
                        }
                    }
                }
            }
        }
    }
}
