using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClothersShop.Models;
using ClothersShop.Common;
using WebDocTruyenOnline.Common;

namespace ClothersShop.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Products
        public ActionResult Index(string searchString)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            model = model.Include(x => x.ProductCategory);
            return View(model.OrderBy(x => x.Name).ThenBy(x => x.CreatedDate).ToList());
        }


        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,Name,Code,MetaTitle,Description,Image,Image1,Image2,Price,PromotionPrice,IncludeVAT,Quantity,CategoryID,Detail,Warranty,CreatedDate,CreatedBy,ModifiledDate,ModifiledBy,MetaKeywords,MetaDescriptions,Status,TopHot,ViewCount")] Product product)
        {
            if (ModelState.IsValid)
            {
                var sess = (UserLogin)Session[CommonConstans.USER_SESSION];
                product.CreatedDate = DateTime.Now;
                product.CreatedBy = sess.Email;
                product.MetaTitle = ConvertToUnSign.convertToUnSign(product.Name);
                product.Status = true;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "Id", "Name");
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "Id", "Name");
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,Name,Code,MetaTitle,Description,Image,Image1,Image2,Price,PromotionPrice,IncludeVAT,Quantity,CategoryID,Detail,Warranty,CreatedDate,CreatedBy,ModifiledDate,ModifiledBy,MetaKeywords,MetaDescriptions,Status,TopHot,ViewCount")] Product product)
        {
            if (ModelState.IsValid)
            {

                var sess = (UserLogin)Session[CommonConstans.USER_SESSION];
                product.ModifiledDate = DateTime.Now;
                product.ModifiledBy = sess.Email;
                product.MetaTitle = ConvertToUnSign.convertToUnSign(product.Name);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "Id", "Name");
            return View(product);
        }



        // POST: Admin/Products/Delete/5
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            try
            {
                Product product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
