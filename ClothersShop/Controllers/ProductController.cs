﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothersShop.Models;
using Model.Dao;

namespace ClothersShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            ViewBag.Slides = new SlideDao().ListAll();
            var dao = new ProductDao();
            ViewBag.ListAllProduct = dao.ListAllProduct(100);
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
        public ActionResult Category(long id)
        {
            var category = new CategoryDao().ViewDetail(id);
            ViewBag.Category = category;
            
            var model = new ProductDao().ListByCategoryId(id);

            return View(model);
        }
        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProduct(id);
            return View(product);
        }
        //    public ActionResult Search(string keyword, int page = 1, int pageSize = 1)
        //    {
        //        int totalRecord = 0;
        //        var model = new ProductDao().Search(keyword, ref totalRecord, page, pageSize);

        //        ViewBag.Total = totalRecord;
        //        ViewBag.Page = page;
        //        ViewBag.Keyword = keyword;
        //        int maxPage = 5;
        //        int totalPage = 0;

        //        totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
        //        ViewBag.TotalPage = totalPage;
        //        ViewBag.MaxPage = maxPage;
        //        ViewBag.First = 1;
        //        ViewBag.Last = totalPage;
        //        ViewBag.Next = page + 1;
        //        ViewBag.Prev = page - 1;

        //        return View(model);
        //    }
        //    public ActionResult ListByCateId(long id)
        //    {

        //        var model = new ProductDao().ListByCategoryId(id);
        //        return View(model);
        //    }
    }
}