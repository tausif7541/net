using ProductOrderApi.Models;
using System.Collections.Generic;

namespace ProductOrderApi.Repositories
{
    public interface IOrderRepository
    {
        Order GetById(int id);
        IEnumerable<Order> GetAll();
        Order Create(Order order);
        void Update(Order order);
        void Delete(int id);
    }
}
