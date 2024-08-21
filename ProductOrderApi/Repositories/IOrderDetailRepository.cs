using ProductOrderApi.Models;
using System.Collections.Generic;

namespace ProductOrderApi.Repositories
{
    public interface IOrderDetailRepository
    {
        OrderDetail GetById(int id);
        IEnumerable<OrderDetail> GetAll();
        OrderDetail Create(OrderDetail orderDetail);
        void Update(OrderDetail orderDetail);
        void Delete(int id);
    }
}
