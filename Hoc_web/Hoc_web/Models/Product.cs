using System.ComponentModel.DataAnnotations;

namespace Hoc_web.Models
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(100,ErrorMessage ="Ten San Pham Khong Duoc Dai Qua 100 ky tu")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; } //chắc chắn nhập dữ liệu thì không cần ? còn có hay không cũng được thì để ?

        public string? ImageUrl { get; set; }

        //kết nối bảng Category
        //khóa chinh nên khai báo 2 dòng
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        //kết nối bảng Product image
        //khóa phu nên khai báo 1 dòng
        public List<ProductImage>? List_Image { get; set; }
    }
}
