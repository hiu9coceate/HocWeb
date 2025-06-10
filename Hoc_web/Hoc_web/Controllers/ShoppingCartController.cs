using Hoc_web.Models;
using Hoc_web.Models.Extensisons;
using Hoc_web.Models.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace Hoc_web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductRepository _productRepository;
        public ShoppingCartController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("cart") ?? new ShoppingCart();
            return View(cart);
        }
        public async Task<IActionResult> AddItemToCart(int productId, int quantity)
        {
            var _product = _productRepository.GetById(productId);
            if (_product != null)
            {
                var cartItem = new CartItem
                {
                    ProductId = productId,
                    ProductName = _product.Name,
                    Price = _product.Price,
                    QuantiTy = quantity
                };
                var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("cart") ?? new ShoppingCart();
                cart.AddItemToCart(cartItem);
                HttpContext.Session.SaveObjectAsjson("cart", cart);
            }
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> CheckOut()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("cart");
            if (cart != null)
            {
                ViewBag.ShoppingCart = cart;
            }
            Order _order = new Order();
            return View(_order);
        }
        private async Task<Product> GetProductFromDatabase(int productId)
        {
            // Truy vấn cơ sở dữ liệu để lấy thông tin sản phẩm 
            var product = await Task.Run(() => _productRepository.GetById(productId));
            return product;
        }

        public IActionResult OrderCompleted()
        {
            return View();
        }
        public Task<IActionResult> OderCompleted()
        {

            return Task.FromResult<IActionResult>(View("OrderCompleted"));
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");

            if (cart is not null)
            {
                cart.RemoveItem(productId);

                // Lưu lại giỏ hàng vào Session sau khi đã xóa mục 
                HttpContext.Session.SaveObjectAsjson("Cart", cart);
            }

            return RedirectToAction("Index");
        }

    }
}
