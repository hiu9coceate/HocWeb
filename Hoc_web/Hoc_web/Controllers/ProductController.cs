using Hoc_web.Models;
using Hoc_web.Models.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hoc_web.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Hoc_web.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        private ICategoryRepository _CategoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            
            _productRepository = productRepository;
            _CategoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            var _categories = _CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(_categories, "Id", "Name");

            var list_product = _productRepository.GetAll();
            return View(list_product);
        }
        [AllowAnonymous]
        public IActionResult View(int id)
        {
            var _product = _productRepository.GetById(id);
            return View(_product);
        }

        public IActionResult Create()
        {
            var _categories = _CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(_categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile? ImageUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImageUrl != null)
                {
                     if (!Directory.Exists("wwwroot/images"))
                    {
                        Directory.CreateDirectory("wwwroot/images");
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUrl.FileName);
                    var path = Path.Combine("wwwroot/images", fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        await ImageUrl.CopyToAsync(fs);
                        product.ImageUrl = "images/" + fileName;
                    }
                }
                _productRepository.Create(product);
                return RedirectToAction("Index", "Product");
            }
            else
            {
                var _categories = _CategoryRepository.GetAll();
                ViewBag.Categories = new SelectList(_categories, "Id", "Name");
                return View(product);
            }
        }

        public IActionResult Update(int id)
        {
            var _product = _productRepository.GetById(id);
            var _categories = _CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(_categories, "Id", "Name");
            ViewBag.ImageUrl = _product.ImageUrl;
            return View(_product);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Product product, IFormFile? ImageUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImageUrl != null)
                {
                    if (!Directory.Exists("wwwroot/images"))
                    {
                        Directory.CreateDirectory("wwwroot/images");
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUrl.FileName);
                    var path = Path.Combine("wwwroot/images", fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        await ImageUrl.CopyToAsync(fs);
                        product.ImageUrl = "images/" + fileName;
                    }
                }
                else
                {
                    if(product.ImageUrl == null)
                    {
                        product.ImageUrl = "default_img.jpg";

                    }
                }

                    _productRepository.Update(product);
                return RedirectToAction("Index", "Product");
            }
            else
            {
                var _categories = _CategoryRepository.GetAll();
                ViewBag.Categories = new SelectList(_categories, "Id", "Name");
                return View(product);
            }
        }
        public IActionResult Delete(int id)
        {
            var _product = _productRepository.GetById(id);
            return View(_product);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var _product = _productRepository.GetById(id.Value);
            if (_product == null)
            {
                return NotFound();
            }
            _productRepository.Delete(id.Value);
            return RedirectToAction("Index", "Product");

        }

        public IActionResult Search(string? name, decimal? minPrice, decimal? maxPrice, int? categoryId)
        {
            // Lấy tất cả sản phẩm (nên dùng IQueryable nếu có thể để tối ưu)
            var _products = _productRepository.GetAll();

            // 1. Lọc theo Tên và/hoặc Ghi chú
            if (!string.IsNullOrEmpty(name))
            {
                _products = _products.Where(p =>
        // Gọi hàm mở rộng trực tiếp!
        p.Name.ContainsIgnoreCaseAndDiacritics(name) ||
                    // Hoặc kiểm tra ghi chú (nếu có) chứa chuỗi tìm kiếm
                    (p.Description != null && p.Description.ContainsIgnoreCaseAndDiacritics(name))
                ).ToList();
            }

            // 2. Lọc theo Giá thấp nhất (Min Price)
            if (minPrice.HasValue)
            {
                _products = _products.Where(p => p.Price >= minPrice.Value).ToList();
            }

            // 3. Lọc theo Giá cao nhất (Max Price)
            if (maxPrice.HasValue)
            {
                _products = _products.Where(p => p.Price <= maxPrice.Value).ToList();
            }

            // 4. Lọc theo Danh mục
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                _products = _products.Where(p => p.CategoryId == categoryId.Value).ToList();
            }

            // Lấy danh sách danh mục cho dropdown
            var categories = _CategoryRepository.GetAll();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", categoryId);

            // Gửi lại các giá trị tìm kiếm về View để hiển thị lại trên form
            ViewData["CurrentName"] = name;
            ViewData["CurrentMinPrice"] = minPrice;
            ViewData["CurrentMaxPrice"] = maxPrice;

            // Trả về View (Search.cshtml hoặc Index.cshtml tùy bạn cấu hình)
            return View( "Index" ,_products);
        }





    }
}
