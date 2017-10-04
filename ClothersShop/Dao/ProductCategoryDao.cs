using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothersShop.Models;


namespace Model.Dao
{
    public class ProductCategoryDao
    {
        ApplicationDbContext db = null;
        public ProductCategoryDao()
        {
            db = new ApplicationDbContext();
        }

        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status == true && x.ShowOnHome == true).OrderBy(x => x.DisplayOrder).ToList();
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public long Insert(ProductCategory entity)
        {
            db.ProductCategories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public ProductCategory GetByID(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public bool Update(ProductCategory entity)
        {
            try
            {
                var category = db.ProductCategories.Find(entity.ID);
                category.Name = entity.Name;
                category.MetaTitle = entity.MetaTitle;
                category.DisplayOrder = entity.DisplayOrder;
                category.MetaDescriptions = entity.MetaDescriptions;
                category.ModifiledDate = DateTime.Now;
                category.ParentID = entity.ParentID;
                category.Status = entity.Status;
                category.ShowOnHome = entity.ShowOnHome;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var productCategory = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(productCategory);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //public IEnumerable<ProductCategory> ListAllPaging(string searchString, int page, int pageSize)
        //{
        //    IQueryable<ProductCategory> model = db.ProductCategories;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.Name.Contains(searchString)|| x.CreatedBy.Contains(searchString));
        //    }

        //    return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        //}
        public bool ChangeStatus(long id)
        {
            var productcategory = db.ProductCategories.Find(id);
            productcategory.Status = !productcategory.Status;
            db.SaveChanges();
            return productcategory.Status;
        }
    }
}
