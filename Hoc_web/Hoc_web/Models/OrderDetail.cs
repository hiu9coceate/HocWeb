using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace Hoc_web.Models
{
    [PrimaryKey("OrderId", "ProductId")]
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [ValidateNever]
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ValidateNever]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
