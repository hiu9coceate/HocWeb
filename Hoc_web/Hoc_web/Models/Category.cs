namespace Hoc_web.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //ket noi Product
        public List<Product>? Products { get; set; }

    }
}
