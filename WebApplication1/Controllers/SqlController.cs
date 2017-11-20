using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Linq.Dynamic;


namespace WebApplication1.Controllers
{
    public class SqlController : Controller
    {
     
        public ActionResult Index()
        {

            return View();
        }

     
        public ActionResult WebGrid(int page = 1, int rowsPerPage = 10, string sortCol = "ProductID", string sortDir = "ASC")
        {
            List<MyModel> res;
            int count;
            string sql;
            HashSet<string> sortColList = new HashSet<string>() {
            "CategoryName", "CompanyName", "ContactName", "Country", "ProductID", "ProductName", "SupplierID", "CategoryID",
                "UnitPrice", "UnitsInStock", "UnitsOnOrder", "CategoryID"
            };
            HashSet<string> sortDirList = new HashSet<string>() {
            "ASC", "DESC"
            };

            if (sortColList.Contains(sortCol) && sortDirList.Contains(sortDir))
            {
                if (sortCol == "CategoryName")
                {
                    sortCol = "Category.CategoryName";
                }
                else if (sortCol == "CompanyName")
                {
                    sortCol = "Supplier.CompanyName";
                }
                else if (sortCol == "ContactName")
                {
                    sortCol = "Supplier.ContactName";
                }
                else if (sortCol == "Country")
                {
                    sortCol = "Supplier.Country";
                }
            }
            else
            {
                sortCol = "ProductID";
                sortDir = "ASC";
                ViewBag.sortCol = sortCol;
                ViewBag.sortDir = sortDir;
            }



            using (var nwd = new NorthwindEntities())
            {
                
                
                var _res = nwd.Products
                    .OrderBy(sortCol + " " + sortDir + ", " + "ProductID" + " " + sortDir)
                    .Skip((page - 1) * rowsPerPage)
                    .Take(rowsPerPage)
                    .Select(o => new MyModel
                    {
                        ProductID = o.ProductID,
                        ProductName = o.ProductName,
                        SupplierID = !o.SupplierID.HasValue ? (int?)null : (int?)o.SupplierID,
                        CategoryID = !o.CategoryID.HasValue ? (int?)null : (int?)o.CategoryID,
                        CategoryName = !o.CategoryID.HasValue ? (string)null : (string)o.Category.CategoryName,
                        UnitPrice = !o.UnitPrice.HasValue ? (int?)null : (int)o.UnitPrice,
                        UnitsInStock = !o.UnitsInStock.HasValue ? (short?)null : (short)o.UnitsInStock,
                        UnitsOnOrder = !o.UnitsOnOrder.HasValue ? (short?)null : (short)o.UnitsOnOrder,
                        CompanyName = !o.SupplierID.HasValue ? (string)null : (string)o.Supplier.CompanyName,
                        ContactName = !o.SupplierID.HasValue ? (string)null : (string)o.Supplier.ContactName,
                        Country = o.Supplier.Country,

                    });
                
                res = _res.ToList();
                count = nwd.Products.Count();
                sql = nwd.Products.AsQueryable().OrderBy(sortCol + " " + sortDir + ", " + "ProductID" + " " + sortDir).Skip((page - 1) * rowsPerPage).Take(rowsPerPage).ToString();
                ViewBag.sql = sql;
                
            }

            ViewBag.sortDir = sortDir;
            ViewBag.sql = sql;
            ViewBag.sortCol = sortCol;
            ViewBag.rowsPerPage = rowsPerPage;
            ViewBag.count = count;
            return View(res);
        }
    }
}



