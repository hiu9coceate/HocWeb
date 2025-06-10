using System.Security.Cryptography.X509Certificates;

namespace Hoc_web.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public void AddItemToCart(CartItem item)
        {
            var cartItem = Items.FirstOrDefault(n => n.ProductId == item.ProductId);
            if (cartItem != null)
            {
                cartItem.QuantiTy += item.QuantiTy; // Assuming QuantiTy is a string that can be concatenated
            }
            else
            {
                Items.Add(item);
            }
        }
        public void RemoveItem(int productId)
        {
            Items.RemoveAll(n => n.ProductId == productId);
        }
        public void RemoveItem(CartItem item)
        {
            Items.RemoveAll(n => n.ProductId == item.ProductId);
        }
    }
}
