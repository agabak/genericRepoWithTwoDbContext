using Identity.Data.Repositories;
using Identity.Entities;
using Identity.Services;
using Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly IBuildDropdown _buildDropdown;
        protected ProductViewModel Product { get; set; }

        public ProductController(ProductRepository productRepository, IBuildDropdown buildDropdown)
        {
            _productRepository = productRepository;
            _buildDropdown = buildDropdown;
            Product = new ProductViewModel();
        }

        public IActionResult Index()
        { 
            return View();
        }
        public async Task<IActionResult> Create()
        {
            Product.Items = await _buildDropdown
                                 .DropdownItems()
                                 .ConfigureAwait(true);
            return View(Product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {

            if (!ModelState.IsValid) return View(model);

            var prod = int.TryParse(model?.SupplierId,  out int supplierId);

            var product = new Product { 
                                        ProductName = model?.ProductName, 
                                        UnitPrice = model.UnitPrice, 
                                        Package = model.Package, 
                                        IsDiscontinued = model.IsDiscontinued ,
                                        SupplierId = prod ? supplierId : 0
                                  };

            product = await _productRepository.Create(product).ConfigureAwait(true);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            if (id <= 0) return RedirectToAction("Index", "Home");
            var product = await _productRepository.GetSingle(id).ConfigureAwait(true);

            if(product == null) return RedirectToAction("Index", "Home");
            return View(product);
        }
    }
}