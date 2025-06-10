namespace Hoc_web.Models.Repositories
{
    public interface IOderRepository
    {
        IEnumerable<Order> GetAll();
        Order GetById(int id);
        Task Add(Order order, List<OrderDetail> orderDetails);
    }
}
