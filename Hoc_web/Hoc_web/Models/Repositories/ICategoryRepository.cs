namespace Hoc_web.Models.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetById(int id);
        void Create(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}
