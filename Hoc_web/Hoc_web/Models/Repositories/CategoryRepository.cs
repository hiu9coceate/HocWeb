namespace Hoc_web.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
        public Category GetById(int id)
        {
            return _context.Categories.FirstOrDefault(n => n.Id == id);
        }
        public void Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var _category = _context.Categories.FirstOrDefault(n => n.Id == id);
            if (_category != null)
            {
                _context.Remove(_category);
                _context.SaveChanges();
            }
        }
    }
}
