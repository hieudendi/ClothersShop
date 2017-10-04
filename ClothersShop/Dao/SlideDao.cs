using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothersShop.Models;


namespace Model.Dao
{
    public class SlideDao
    {
        ApplicationDbContext db = null;
        public SlideDao()
        {
            db = new ApplicationDbContext();
        }
        public List<Slide> ListAll()
        {
            return db.Slides.Where(x => x.Status == true).OrderBy(y => y.DisplayOrder).ToList();
        }
        public long Insert(Slide entity)
        {
            db.Slides.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public Slide GetByID(long id)
        {
            return db.Slides.Find(id);
        }
        public Slide ViewDetail(long id)
        {
            return db.Slides.Find(id);
        }
        public bool Update(Slide entity)
        {
            try
            {
                var slide = db.Slides.Find(entity.ID);
                slide.Image = entity.Image;
                slide.DisplayOrder = entity.DisplayOrder;
                slide.ModifiledDate = DateTime.Now;
                slide.Status = entity.Status;
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
                var slide = db.Slides.Find(id);
                db.Slides.Remove(slide);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //public IEnumerable<Slide> ListAllPaging(string searchString, int page, int pageSize)
        //{
        //    IQueryable<Slide> model = db.Slides;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.Image.Contains(searchString));
        //    }

        //    return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        //}
        public bool ChangeStatus(long id)
        {
            var slide = db.Slides.Find(id);
            slide.Status = !slide.Status;
            db.SaveChanges();
            return slide.Status;
        }
    }
}
