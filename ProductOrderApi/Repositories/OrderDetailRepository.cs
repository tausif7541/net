using Microsoft.EntityFrameworkCore;
using ProductOrderApi.Data;
using ProductOrderApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductOrderApi.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly StoreDbContext _context;

        public OrderDetailRepository(StoreDbContext context)
        {
            _context = context;
        }

        public OrderDetail GetById(int id)
        {
            return _context.OrderDetail
                           .Include(od => od.Product)
                           .Include(od => od.Order)
                           .FirstOrDefault(od => od.Id == id);
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _context.OrderDetail
                           .Include(od => od.Product)
                           .Include(od => od.Order)
                           .ToList();
        }

        public OrderDetail Create(OrderDetail orderDetail)
        {
            _context.OrderDetail.Add(orderDetail);
            _context.SaveChanges();
            return orderDetail;
        }

        public void Update(OrderDetail orderDetail)
        {
            _context.OrderDetail.Update(orderDetail);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var orderDetail = _context.OrderDetail.Find(id);
            if (orderDetail != null)
            {
                _context.OrderDetail.Remove(orderDetail);
                _context.SaveChanges();
            }
        }
    }
}
