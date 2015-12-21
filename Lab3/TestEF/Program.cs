using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEF
{
    class Program
    {
        static MovieDbC db = new MovieDbC();
        static void Main(string[] args)
        {
            //Movie m1 = new Movie();
            //m1.Title = "Title";
            //m1.Genre = "Genre";
            //m1.Price = 100;
            //m1.ReleaseDate = DateTime.Now;
            //m1.Rating = "Rating";
            //db.Movies.Add(m1);
            //db.SaveChanges();

            //var result = from m in db.Movies select m;
            //foreach(var i in result)
            //{
            //    Console.WriteLine("{0}", i.ReleaseDate);
            //}

            bool flag = true;
            while(flag)
            {
                Console.WriteLine("请输入数字选择功能");
                Console.WriteLine(" 1	根据ID查询电影的信息");
                Console.WriteLine(" 2	根据电影名称模糊查询");
                Console.WriteLine(" 3	查询尚未上映电影的信息");
                Console.WriteLine(" 4	查询票价在某个区间的电影信息");
                Console.WriteLine(" 0	退出");
                int op = int.Parse(Console.ReadLine());
                if (op == 0) break;

                IQueryable<Movie> res = null;
                switch(op)
                {
                    case 1:
                        Console.WriteLine("  请输入要查询的ID");
                        int id = int.Parse(Console.ReadLine());
                        res = from m in db.Movies where m.ID == id select m;
                        break;
                    case 2:
                        string tit = Console.ReadLine();
                        res = from m in db.Movies where m.Title.Contains(tit) select m;
                        break;
                    default:
                        break;
                }
                if(res != null) foreach (var i in res)
                {
                    Console.WriteLine("{0} {1} {2} {3}", i.ID, i.Title, i.ReleaseDate, i.Price);
                }

                Console.WriteLine("是否继续?(y/n)");
                string s =Console.ReadLine();
                flag = s[0] == 'Y' || s[0] == 'y';
            }
        }
    }
}
