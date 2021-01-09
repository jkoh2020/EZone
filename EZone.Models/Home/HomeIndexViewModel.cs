using EZone.Data;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZone.Models.Home
{
    public class HomeIndexViewModel
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        public IPagedList<Product> ListOfProducts { get; set; }
        public HomeIndexViewModel CreateModel(string search, int? page, int pageSize)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@search", search??(object)DBNull.Value)
            };
            IPagedList<Product> data = _db.Database.SqlQuery<Product>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);

            return new HomeIndexViewModel
            {

                ListOfProducts = data
            };

        }
       
        //Use this code when not using IpagedList
        //private ApplicationDbContext _db = new ApplicationDbContext();
        //public IEnumerable<Product> ListOfProducts { get; set; }
        //public HomeIndexViewModel CreateModel()
        //{
        //    return new HomeIndexViewModel
        //    {
        //        ListOfProducts = _db.Products
        //    };
        //}



    }
}
