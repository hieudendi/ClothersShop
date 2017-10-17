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
    public class ProductCategoriesController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProductCategories
        public ActionResult Index(string searchString)
        {
            IQueryable<ProductCategory> model = db.ProductCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }

            return View(model.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Name).ToList());
        }


        // GET: Admin/ProductCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,MetaTitle,ParentID,DisplayOrder,SeoTitle,CreatedDate,CreatedBy,ModifiledDate,ModifiledBy,MetaKeywords,MetaDescriptions,Status,ShowOnHome")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                var sess = (UserLogin)Session[CommonConstans.USER_SESSION];
                productCategory.CreatedDate = DateTime.Now;
                productCategory.CreatedBy = sess.Email;
                productCategory.MetaTitle = ConvertToUnSign.convertToUnSign(productCategory.Name);
                productCategory.Status = true;
                productCategory.ShowOnHome = true;
                db.ProductCategories.Add(productCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productCategory);
        }

        // GET: Admin/ProductCategories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // POST: Admin/ProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,MetaTitle,ParentID,DisplayOrder,SeoTitle,CreatedDate,CreatedBy,ModifiledDate,ModifiledBy,MetaKeywords,MetaDescriptions,Status,ShowOnHome")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                var sess = (UserLogin)Session[CommonConstans.USER_SESSION];
                productCategory.ModifiledDate = DateTime.Now;
                productCategory.ModifiledBy = sess.Email;
                productCategory.MetaTitle = ConvertToUnSign.convertToUnSign(productCategory.Name);


                db.Entry(productCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productCategory);
        }

        // POST: Admin/ProductCategories/Delete/5
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            try
            {
                ProductCategory story = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(story);
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
