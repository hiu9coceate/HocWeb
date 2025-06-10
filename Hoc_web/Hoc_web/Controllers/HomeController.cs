using System.Diagnostics;
using Hoc_web.Models;
using Hoc_web.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hoc_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository; // Dependency đã có từ code của bạn

        // Constructor đã được cập nhật từ code của bạn
        public HomeController(IProductRepository productRepository, ILogger<HomeController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Kiểm tra xem người dùng đã được xác thực và có vai trò là "Admin" hay không
            // Thay "Admin" bằng tên vai trò quản trị viên thực tế trong hệ thống của bạn
            // Thêm kiểm tra User.Identity != null để tránh lỗi nếu chưa xác thực
            if (User.Identity != null && User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                // Nếu là Admin, chuyển hướng đến Action Index của ProductController
                // Bạn có thể thay đổi "Product" và "Index" nếu controller/action của Admin khác
                return RedirectToAction("Index", "Product");
            }

            // Nếu không phải Admin hoặc chưa đăng nhập (và trang Index không yêu cầu [Authorize]),
            // hiển thị danh sách sản phẩm như logic cũ của bạn
            var productList = _productRepository.GetAll();
            return View(productList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
