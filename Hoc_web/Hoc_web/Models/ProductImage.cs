namespace Hoc_web.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        //ket noi bang Product
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
