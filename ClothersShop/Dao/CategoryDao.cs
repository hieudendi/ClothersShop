using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothersShop.Models;


namespace Model.Dao
{
    public class CategoryDao
    {
        ApplicationDbContext db = null;
        public CategoryDao()
        {
            db = new ApplicationDbContext();
        }
        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public Category GetByID(long id)
        {
            return db.Categories.Find(id);
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x=>x.Status== true).ToList(); 
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public bool Update(Category entity)
        {
            try
            {
                var category = db.Categories.Find(entity.ID);
                category.Name = entity.Name;
                category.MetaTitle = entity.MetaTitle;
                category.DisplayOrder = entity.DisplayOrder;
                category.MetaDescriptions = entity.MetaDescriptions;
                category.ModifiledDate= DateTime.Now;
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
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        //{
        //    IQueryable<Category> model = db.Categories;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.Name.Contains(searchString));
        //    }

        //    return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        //}
        public bool ChangeStatus(long id)
        {
            var category = db.Categories.Find(id);
            category.Status = !category.Status;
            db.SaveChanges();
            return category.Status;
        }
    }
}
