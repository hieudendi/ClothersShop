using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using ClothersShop.Common;
using ClothersShop.Models;

namespace ClothersShop.Controllers
{
    public class HomeController : Controller
    {
        private const string CartSession = "CartSession";
        public ActionResult Index()
        {
           
            var dao = new ProductDao();
            ViewBag.ListNewProduct = dao.ListNewProduct(16);
            ViewBag.ListFeatureProduct = dao.ListFeatureProduct(16);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [ChildActionOnly]
        public ActionResult Slide()
        {
            ViewBag.Slides = new SlideDao().ListAll();
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstans.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }
    }
}