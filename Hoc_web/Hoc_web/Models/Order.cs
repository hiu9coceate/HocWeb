using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Hoc_web.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateOnly Ngay_LapHD { get; set; }
        public decimal TongTien { get; set; }
        public string? UserID { get; set; }
        [ValidateNever]
        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }
        public List<OrderDetail> details { get; set; } = new List<OrderDetail>();

    }
}
