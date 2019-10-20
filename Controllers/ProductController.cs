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
        private readonly SupplierRepository _supplierRepository;
      

        public ProductController(ProductRepository productRepository, SupplierRepository supplierRepository)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        

        public async Task<IActionResult> Create()
        {
            var suppliers = await _supplierRepository.GetAll()
                                              .ConfigureAwait(true);
            var product = new ProductViewModel();

            product.Items = BuildDropdown.DropdownItems(suppliers);

            return View(product);
        }

        

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {

            if (!ModelState.IsValid) return View(model);

            int supplierId = 0;
            int.TryParse(model?.SupplierId, out supplierId);

            var product = new Product { 
                                        ProductName = model?.ProductName, 
                                        UnitPrice = model.UnitPrice, 
                                        Package = model.Package, 
                                        IsDiscontinued = model.IsDiscontinued ,
                                        SupplierId = supplierId
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