using Microsoft.EntityFrameworkCore;
using ProductOrderApi.Data;
using ProductOrderApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductOrderApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDbContext _context;

        public OrderRepository(StoreDbContext context)
        {
            _context = context;
        }

        public Order GetById(int id)
        {
            return _context.Order
                           .Include(o => o.OrderDetails)
                           .ThenInclude(od => od.Product)
                           .FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Order
                           .Include(o => o.OrderDetails)
                           .ThenInclude(od => od.Product)
                           .ToList();
        }

        public Order Create(Order order)
        {
            _context.Order.Add(order);
            _context.SaveChanges();
            return order;
        }

        public void Update(Order order)
        {
            _context.Order.Update(order);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = _context.Order.Find(id);
            if (order != null)
            {
                _context.Order.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}
