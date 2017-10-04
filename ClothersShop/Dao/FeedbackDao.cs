using ClothersShop.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FeedbackDao
    {
        ApplicationDbContext db = null;
        public FeedbackDao()
        {
            db = new ApplicationDbContext();
        }

        //public IEnumerable<Feedback> ListAllPaging(string searchString, int page, int pageSize)
        //{
        //    IQueryable<Feedback> model = db.Feedbacks;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.Name.Contains(searchString));
        //    }
        //    return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        //}
        public bool Delete(int id)
        {
            try
            {
                var feedback = db.Feedbacks.Find(id);
                db.Feedbacks.Remove(feedback);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ChangeStatus(long id)
        {
            var feedback = db.Feedbacks.Find(id);
            feedback.Statuts = !feedback.Statuts;
            db.SaveChanges();
            return feedback.Statuts;
        }
    }
}
