using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCLabs.Models
{
    public class Movie
    {
        [Display(Name = "电影编号")]
        public int ID { get; set; }      //电影编号

        [Display(Name = "电影名称")]
        [Required(ErrorMessage ="必填")]
        [StringLength(60,MinimumLength = 3, ErrorMessage ="必须是3到60个字符")]
        public string Title { get; set; }     //电影名称

        [Display(Name = "上映时间")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "必填")]
        public DateTime ReleaseDate { get; set; }    //上映时间

        [Display(Name = "电影类型")]
        [Required(ErrorMessage = "必填")]
        public string Genre { get; set; }     //电影类型

        [Display(Name = "电影票价")]
        [Range(1,100)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }    //电影票价

        [Display(Name = "电影分级")]
        [StringLength(5)]
        [Required(ErrorMessage = "必填")]
        public string Rating { get; set; }     //电影分级
    }

    public class MovieDbC : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}