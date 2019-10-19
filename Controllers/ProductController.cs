using Identity.Data.Repositories;
using Identity.Entities;
using Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var product = new Product { ProductName = model?.ProductName };

            product = await _repository.Create(product).ConfigureAwait(false);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            if (id <= 0) return RedirectToAction("Index", "Home");
            var product = await _repository.GetSingle(id).ConfigureAwait(false);

            if(product == null) return RedirectToAction("Index", "Home");
            return View(product);
        }
    }
}