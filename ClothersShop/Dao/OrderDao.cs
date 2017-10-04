using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothersShop.Models;


namespace Model.Dao
{
    public class OrderDao
    {
        ApplicationDbContext db = null;
        public OrderDao()
        {
            db = new ApplicationDbContext();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
        //public IEnumerable<Order> ListAllPaging(string searchString, int page, int pageSize)
        //{
        //    IQueryable<Order> model = db.Orders;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.ShipName.Contains(searchString));
        //    }
        //    return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        //}
        //public bool Delete(int id)
        //{
        //    try
        //    {
        //        var order = db.Orders.Find(id);
        //        db.Orders.Remove(order);
        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        public bool ChangeStatus(long id)
        {
            var order = db.Orders.Find(id);
            order.Status = !order.Status;
            db.SaveChanges();
            return order.Status;
        }
    }
}
