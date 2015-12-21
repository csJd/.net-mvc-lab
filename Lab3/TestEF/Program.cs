using System;
using System.Linq;

namespace TestEF
{
    class Program
    {
        static MovieDbC db = new MovieDbC();

        static void delete()
        {
            Console.WriteLine("=== 删除 ===");
            Console.WriteLine("  请输入你要删除的记录的ID:");
            int id = int.Parse(Console.ReadLine());

            Movie m = db.Movies.Find(id);
            if (m != null)
            {
                Console.WriteLine("确定要删除这一项吗?(y/n)");
                Console.WriteLine(" | {0} {1} {2} {3:yyyy/MM/dd} ￥{4}|", m.Title, m.Genre, m.Rating, m.ReleaseDate, m.Price);
                string s = Console.ReadLine();
                bool flag = s[0] == 'Y' || s[0] == 'y';
                if (flag)
                {
                    db.Movies.Remove(m);
                    Console.WriteLine("  删除成功!");
                    db.SaveChanges();
                } 
            }
            else Console.WriteLine("没有找到ID为{0}的记录", id);
        }

        static void add()
        {
            Console.WriteLine("=== 添加 ===");
            Movie m = new Movie();

            Console.WriteLine("## 请输入电影名称:");
            m.Title = Console.ReadLine();

            Console.WriteLine("## 请输入电影类型:");
            m.Genre = Console.ReadLine();

            Console.WriteLine("## 请输入电影上映日期(yyyy/mm/dd):");
            m.ReleaseDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("## 请输入电影票价:");
            m.Price = Decimal.Parse(Console.ReadLine());

            Console.WriteLine("## 请输入电影分级:");
            m.Rating = Console.ReadLine();

            db.Movies.Add(m);
            db.SaveChanges();

            Console.WriteLine("  添加成功！:");
        }

        static void modify()
        {
            Console.WriteLine("=== 更新 ===");
            Console.WriteLine("## 请输入你要更新的记录的ID:");
            int id = int.Parse(Console.ReadLine());
            Movie m = db.Movies.Find(id);
            if (m != null)
            {
                Console.WriteLine(" 当前记录如下：");
                Console.WriteLine(" | {0} {1} {2} {3:yyyy/MM/dd} ￥{4}|", m.Title, m.Genre, m.Rating, m.ReleaseDate, m.Price);
                Console.WriteLine("## 请输入新的电影名称:");
                m.Title = Console.ReadLine();

                Console.WriteLine("## 请输入新的电影类型:");
                m.Genre = Console.ReadLine();

                Console.WriteLine("## 请输入新的电影上映日期(yyyy/mm/dd):");
                m.ReleaseDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("## 请输入新的电影票价:");
                m.Price = Decimal.Parse(Console.ReadLine());

                Console.WriteLine("## 请输入新的电影分级:");
                m.Rating = Console.ReadLine();

                //db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                Console.WriteLine("  修改成功!");
            }
            else Console.WriteLine("没有找到ID为{0}的记录", id);

        }

        static void query()
        {
            Console.WriteLine("=== 查询 ===");

            Console.WriteLine("## 请输入数字选择功能");
            Console.WriteLine("  1.	根据ID查询电影的信息");
            Console.WriteLine("  2.	根据电影名称模糊查询");
            Console.WriteLine("  3.	查询尚未上映电影的信息");
            Console.WriteLine("  4.	查询票价在某个区间的电影信息");

            int op = int.Parse(Console.ReadLine());
            IQueryable<Movie> res = null;
            switch (op)
            {
                case 1:
                    Console.WriteLine(" === 根据ID查询电影的信息 ===");
                    Console.WriteLine("### 请输入要查询的ID");
                    int id = int.Parse(Console.ReadLine());
                    res = from m in db.Movies where m.ID == id select m;
                    break;
                case 2:
                    Console.WriteLine(" === 根据电影名称模糊查询 ===");
                    Console.WriteLine("### 请输入关键字");
                    string tit = Console.ReadLine();
                    res = from m in db.Movies where m.Title.Contains(tit) select m;
                    break;
                case 3:
                    Console.WriteLine(" === 查询尚未上映电影的信息 ===");
                    DateTime now = DateTime.Now;
                    res = from m in db.Movies where m.ReleaseDate > now select m;
                    break;
                case 4:
                    Console.WriteLine(" === 查询票价在某个区间的电影信息 ===");
                    Console.WriteLine("### 请输入两个数来表示区间：");
                    Decimal le = Decimal.Parse(Console.ReadLine());
                    Decimal ri = Decimal.Parse(Console.ReadLine());
                    res = from m in db.Movies where le < m.Price && ri > m.Price select m;
                    break;
                default:
                    break;
            }

            if (res != null)
            {
                Console.WriteLine("| ID\t Title\t Genre\t Rating\t ReleaseDate\t Price\t\t|");
                foreach (var i in res)
                {
                    Console.WriteLine("| {0}\t {1}\t {2}\t {3}\t {4:yyyy/MM/dd}\t ￥{5}\t|", i.ID, i.Title, i.Genre,  i.Rating, i.ReleaseDate, i.Price);
                }
            }
        }

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
            while (flag)
            {
                Console.WriteLine("# 请输入数字选择功能");
                Console.WriteLine(" 1.  查询");
                Console.WriteLine(" 2.  添加");
                Console.WriteLine(" 3.  删除");
                Console.WriteLine(" 4.  修改");
                Console.WriteLine(" 0.  退出");

                int op = int.Parse(Console.ReadLine());
                if (op == 0) break;

                switch (op)
                {
                    case 1:
                        query();
                        break;
                    case 2:
                        add();
                        break;
                    case 3:
                        delete();
                        break;
                    case 4:
                        modify();
                        break;
                    default:
                        break;
                }

                Console.WriteLine("是否继续?(y/n)");
                string s = Console.ReadLine();
                flag = s[0] == 'Y' || s[0] == 'y';
            }
        }
    }
}
