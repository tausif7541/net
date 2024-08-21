using ProductOrderApi.Data;
using ProductOrderApi.Models;

namespace ProductOrderApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }

        public Product GetById(int id)
        {
            return _context.Product.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Product.ToList();
        }

        public Product Create(Product product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();
            return product;
        }

        public void Update(Product product)
        {
            _context.Product.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Product.Find(id);
            if (product != null)
            {
                _context.Product.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
