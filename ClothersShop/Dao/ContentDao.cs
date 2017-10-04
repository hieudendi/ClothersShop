using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothersShop.Models;


namespace Model.Dao
{
    public class ContentDao
    {
        ApplicationDbContext db = null;
        public ContentDao()
        {
            db = new ApplicationDbContext();
        }
        public Content GetById(long id)
        {
            return db.Contents.Find(id);
        }
        public long Insert(Content entity)
        {
            db.Contents.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Content entity)
        {
            try
            {
                var content = db.Contents.Find(entity.ID);
                content.Name = entity.Name;
                content.MetaTitle = entity.MetaTitle;
                content.Image = entity.Image;
                content.MetaDescriptions = entity.MetaDescriptions;
                content.ModifiledDate = DateTime.Now;
                content.CategoryID = entity.CategoryID;
                content.Status = entity.Status;
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
                var content = db.Contents.Find(id);
                db.Contents.Remove(content);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //public IEnumerable<Content> ListAllPaging(string searchString, int page, int pageSize)
        //{
        //    IQueryable<Content> model = db.Contents;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.Name.Contains(searchString) && x.CreatedBy.Contains(searchString));
        //    }

        //    return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        //}
        public bool ChangeStatus(long id)
        {
            var content = db.Contents.Find(id);
            content.Status = !content.Status;
            db.SaveChanges();
            return content.Status;
        }
        public Content ViewDetail(long id)
        {
            return db.Contents.Find(id);
        }
    }
}
