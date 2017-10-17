using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothersShop.Models;
using Model.Dao;
using PagedList;
using ClothersShop.ViewModel;

namespace ClothersShop.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index()
        {
            ViewBag.Slides = new SlideDao().ListAll();
            var dao = new ProductDao();
            ViewBag.ListAllProduct = dao.ListAllProduct(20);
            ViewBag.ListFeatureProduct = dao.ListFeatureProduct(16);
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }
        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Category(long cateId, int? pageIndex)
        {
            var category = db.ProductCategories.Find(cateId);
            ViewBag.Title = category.Name;
            var _pageIndex = pageIndex ?? 1;
            var model = db.Products.Where(x => x.CategoryID == cateId);

            return View(model.OrderBy(x => x.Name).ToPagedList(_pageIndex, 1));
        }


        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProduct(id);
            return View(product);
        }
        public ActionResult Search(string keyword, int? pageIndex)
        {
            ViewBag.Keyword = keyword;
            var _pageIndex = pageIndex ?? 1;
            var model = db.Products.Where(x => x.Name == keyword);
            return View(model.OrderBy(x => x.Name).ToPagedList(_pageIndex, 10));
        }
        //public ActionResult ListByCateId(long id)
        //{

        //    var model = new ProductDao().ListByCategoryId(id);
        //    return View(model);
        //}
    }
}