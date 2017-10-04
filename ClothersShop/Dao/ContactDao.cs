using ClothersShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContactDao
    {
        ApplicationDbContext db = null;
        public ContactDao()
        {
            db = new ApplicationDbContext();
        }

        public Contact GetActiveContact()
        {
            return db.Contacts.Single(x => x.Statuts == true);
        }

        public int InsertFeedBack(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
    }
}
