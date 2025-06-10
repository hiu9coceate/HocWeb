using Microsoft.EntityFrameworkCore;

namespace Hoc_web.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products.Include( n => n.Category).ToList();
        }
        public Product GetById(int id)
        {
            return _context.Products.FirstOrDefault(n => n.Id == id);
        }
        public void Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var _product = _context.Products.FirstOrDefault(n => n.Id == id);
            if (_product != null)
            {
                _context.Remove(_product);
                _context.SaveChanges();
            }
        }
    }
}
