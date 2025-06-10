using Microsoft.EntityFrameworkCore;

namespace Hoc_web.Models.Repositories
{
    public class OderRepository : IOderRepository
    {
        private ApplicationDbContext _context;
        public OderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.Include(n => n.details);
        }
       public Order GetById(int id)
        {
            return _context.Orders
                .Include(n => n.details)
                .FirstOrDefault(n => n.Id == id);
        }
        public async Task Add(Order order, List<OrderDetail> orderDetails)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.Orders.AddAsync(order);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
