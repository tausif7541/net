using ProductOrderApi.Models;

namespace ProductOrderApi.Repositories
{
    public interface IProductRepository
    {
        Product GetById(int id);
        IEnumerable<Product> GetAll();
        Product Create(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
