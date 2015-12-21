using System;
using System.Data.Entity;

namespace TestEF
{
    public class Movie
    {
        public int ID { get; set; }      //电影编号
        public string Title { get; set; }     //电影名称
        public DateTime ReleaseDate { get; set; }    //上映时间
        public string Genre { get; set; }      //电影类型
        public decimal Price { get; set; }    //电影票价
        public string Rating { get; set; }     //电影分级
    }

    public class MovieDbC : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }

}
